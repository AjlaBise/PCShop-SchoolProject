using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ClassLibrary.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MimeKit;
using MailKit;
using MailKit.Net.Smtp;
using Microsoft.VisualBasic;
using OnlineShop.Models;
using OnlineShop.ViewModels;
using Org.BouncyCastle.Ocsp;

namespace OnlineShop.Controllers
{
    public class AccountController : Controller
    {
        private UserManager<AppUser> userMgr { get; }
        private SignInManager<AppUser> signInMgr { get; }
        private RoleManager<AppRole> roleMgr { get; }
        private Context _context { get; }
        public AccountController(Context context, UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, RoleManager<AppRole> roleManager)
        {
            userMgr = userManager;
            signInMgr = signInManager;
            roleMgr = roleManager;
            _context = context;
        }
        public IActionResult Register()
        {
            var drzave = _context.Drzava.ToList();
            ViewData["drzave"] = drzave;
            return View();
        }

        [HttpGet]
        public IActionResult Login(string returnUrl = "", string msg = "")
        {
            var model = new LoginViewModel { ReturnUrl = returnUrl };
            ViewBag.EmailConfirmated = msg;
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Authorize(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var pw = await userMgr.FindByNameAsync(model.Username);
                if (pw != null)
                {
                    if (!(await userMgr.CheckPasswordAsync(pw, model.Password)))
                    {
                        pw.AccessFailedCount++;
                        await userMgr.UpdateAsync(pw);

                        ViewBag.Info = "Prijava neuspješna";
                        ViewBag.ErrorMsg = "Vaša lozinka nije ispravna";
                        ViewBag.Id = pw.Id.ToString();
                        return PartialView("BadLogin");
                    }
                }

                var result = await signInMgr.PasswordSignInAsync(model.Username,
                   model.Password, model.RememberMe, false);

                if (result.IsNotAllowed)
                {
                    ViewBag.Info = "Prijava neuspješna";
                    ViewBag.ErrorMsg = "Vaš račun nije verifikovan. Provjerite vaš e-mail inbox";
                    var x = await userMgr.FindByNameAsync(model.Username);
                    if(x != null)
                        ViewBag.Id = x.Id.ToString();
                    return PartialView("BadLogin");
                }

                if (!result.Succeeded)
                {
                    ViewBag.Info = "Prijava neuspješna";
                    ViewBag.ErrorMsg = "Vaši podaci nisu ispravni";
                    var x = await userMgr.FindByNameAsync(model.Username);
                    if(x != null)
                        ViewBag.Id = x.Id.ToString();
                    return PartialView("BadLogin");
                }

                if (result.Succeeded)
                {
                    AppUser user = await userMgr.FindByNameAsync(model.Username);

                    if (!string.IsNullOrEmpty(model.ReturnUrl) && Url.IsLocalUrl(model.ReturnUrl))
                    {
                        return Redirect(model.ReturnUrl);
                    }
                    else
                    {
                        user.PosljednjiLoginDate = DateTime.Now;
                        _context.SaveChanges();
                        return RedirectToAction("Index", "Home");
                    }
                }
            }

            ModelState.AddModelError("", "Invalid login attempt");
            ViewBag.Info = "Prijava neuspješna";
            ViewBag.ErrorMsg = "Vaši podaci nisu ispravni";
            return PartialView("BadLogin");
        }

        public async Task<IActionResult> ConfirmEmail(string userID, string token)
        {
            if (userID == null || token == null)
            {
                return RedirectToAction("Index", "Home");
            }

            var user = await userMgr.FindByIdAsync(userID);
            if (user == null)
            {
                ViewBag.Info = "Korisnički ID nije validan.";
                return PartialView("BadLogin");
            }

            var result = await userMgr.ConfirmEmailAsync(user, token);
            if (result.Succeeded)
            {
                ViewBag.EmailConfirmated = "Uspješno ste aktivirali svoj račun. Sada se možete prijaviti.";
                var model = new LoginViewModel { ReturnUrl = "" };
                return View("Login", model);
            }

            ViewBag.Info = "Dogodila se greška";
            ViewBag.ViewBag.ErrorMsg = "Ne možemo potvrditi vašu e-mail adresu.";
            return PartialView("BadLogin");
        }

        public async Task<IActionResult> Logout()
        {
            TempData["signout"] = "Uspješno ste odjavljeni!";
            await signInMgr.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

        public void PosaljiMail(AppUser user, string subject, string text)
        {
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress("PC Shop", "fitnotify@gmail.com"));
            message.To.Add(new MailboxAddress(user.Ime, user.Email));
            message.Subject = subject;
            message.Body = new TextPart("html")
            {
                Text = text
            };
            using (var client = new SmtpClient())
            {
                client.Connect("smtp.gmail.com", 587, false);
                client.Authenticate("fitnotify@gmail.com", "student.notify.123");
                client.Send(message);
                client.Disconnect(true);
            }
        }

        public async Task<IActionResult> AddUser(AppUser noviKorisnik)
        {
            try
            {
                TempData["sucess"] = "Uspješno ste registrovani!";

                // Pretraga ako korisnik vec postoji
                AppUser user = await userMgr.FindByNameAsync(noviKorisnik.UserName);

                // Ako korisnik ne postoji
                if (user == null)
                {
                    noviKorisnik.EmailConfirmed = false;
                    noviKorisnik.PhoneNumberConfirmed = false;
                    noviKorisnik.TwoFactorEnabled = false;
                    noviKorisnik.LockoutEnabled = false;
                    noviKorisnik.AccessFailedCount = 1;
                    noviKorisnik.DatumRegistracije = DateTime.Now;
                    noviKorisnik.PosljednjiLoginDate = null;

                    // Password mora sadrzati mala, velika slova, brojeve i najmanje jedan specijalni znak.
                    IdentityResult result = await userMgr.CreateAsync(noviKorisnik, noviKorisnik.PasswordHash);
                    TempData["sucess"] = "Uspješno ste registrovani!";
                    ViewBag.ErrorMsg =
                        "Prije nego se prijavite, molimo vas da potvrdite vaš e-mail tako što ćete kliknuti na link koji smo vam poslali na vašu e-mail adresu.";

                    if (result.Succeeded)
                    {
                        var result1 = await userMgr.AddToRoleAsync(noviKorisnik, "Korisnik");
                        var token = await userMgr.GenerateEmailConfirmationTokenAsync(noviKorisnik);
                        var confirmationLink = Url.Action("ConfirmEmail", "Account",
                            new {userID = noviKorisnik.Id, token = token}, Request.Scheme);

                        PosaljiMail(noviKorisnik, "Potvrda korisničkog računa",
                            "<h3 style=\"color:green; font-weight:bold\"> Uspješno ste se registrovali, ostao je još samo jedan korak! </h3>" +
                            "Molimo vas da potvrdite vaš račun klikom na link ispod. <br/><br/>" +
                            "<a href=\"" + confirmationLink + "\"> Potvrdi moj račun </a><br/>");
                    }

                    return PartialView("RegistrationMessage");
                }
                else
                {
                    TempData["sucess"] = "Nažalost, dogodila se greška pri registraciji. Korisnik već postoji.";
                }
            }
            
            catch (Exception e)
            {
                TempData["sucess"] = "Greška: " + e.Message;
            }
            return PartialView("RegistrationMessage");
        }

        public async Task<IActionResult> NisamDobioMail(string UserID)
        {
            var user = await userMgr.FindByIdAsync(UserID);

            var token = await userMgr.GenerateEmailConfirmationTokenAsync(user);
            var confirmationLink = Url.Action("ConfirmEmail", "Account",
                new { userID = user.Id, token = token }, Request.Scheme);

            PosaljiMail(user, "Potvrda korisničkog računa",
                "<h3 style=\"color:green; font-weight:bold\"> Uspješno ste se registrovali, ostao je još samo jedan korak! </h3>" +
                "Molimo vas da potvrdite vaš račun klikom na link ispod. <br/><br/>" +
                "<a href=\"" + confirmationLink + "\"> Potvrdi moj račun </a><br/>");

            @TempData["sucess"] = "E-mail je ponovo poslan";
            @ViewBag.ErrorMsg = "Provjerite vaš e-mail inbox.";

            return PartialView("RegistrationMessage");
        }

        // Ovu metodu treba obrisati nakon završetka projekta
        public async Task<IActionResult> CreateRolesOnce()
        {

            bool x = await roleMgr.RoleExistsAsync("Administrator");
            if (!x)
            {
                // Kreiramo rolu za Administratora
                var role = new AppRole
                {
                    Name = "Administrator"
                };
                await roleMgr.CreateAsync(role);

                // Kreiramo rolu za Korisnika
                var roleKorisnik = new AppRole
                {
                    Name = "Korisnik"
                };
                await roleMgr.CreateAsync(roleKorisnik);

                Drzava drzava = new Drzava()
                {
                    Naziv = "Bosna i Hercegovina"
                };

                _context.Drzava.Add(drzava);
                _context.SaveChanges();

                // Kreira se nalog za Administratora
                var user = new AppUser();

                user.UserName = "Neurouz";
                user.Email = "neurouzmedia@gmail.com";
                user.EmailConfirmed = true;
                user.DatumRegistracije = DateTime.Now;
                user.Ime = "Ajdin";
                user.Prezime = "Hukara";
                user.PhoneNumber = "061550134";
                user.Spol = 'M';
                user.SjedisteId = _context.Drzava.Find(drzava.Id).Id;
                user.PosljednjiLoginDate = null;

                var korisnik1 = new AppUser();

                korisnik1.UserName = "Korisnik-1";
                korisnik1.Email = "k1_mail@gmail.com";
                korisnik1.EmailConfirmed = false;
                korisnik1.DatumRegistracije = DateTime.Now;
                korisnik1.Ime = "Korisnik 1";
                korisnik1.Prezime = "Prezime 1";
                korisnik1.PhoneNumber = "061666584";
                korisnik1.Spol = 'Z';
                korisnik1.SjedisteId = _context.Drzava.Find(drzava.Id).Id;
                korisnik1.PosljednjiLoginDate = null;

                var korisnik2 = new AppUser();

                korisnik2.UserName = "Korisnik-2";
                korisnik2.Email = "k2_mail@gmail.com";
                korisnik2.EmailConfirmed = false;
                korisnik2.DatumRegistracije = DateTime.Now;
                korisnik2.Ime = "Korisnik 2";
                korisnik2.Prezime = "Prezime 2";
                korisnik2.PhoneNumber = "066547855";
                korisnik2.Spol = 'M';
                korisnik2.PosljednjiLoginDate = null;
                korisnik2.SjedisteId = _context.Drzava.Find(drzava.Id).Id;

                var korisnik3 = new AppUser();

                korisnik3.UserName = "Korisnik-3";
                korisnik3.Email = "k3_mail@gmail.com";
                korisnik3.EmailConfirmed = false;
                korisnik3.DatumRegistracije = DateTime.Now;
                korisnik3.Ime = "Korisnik 3";
                korisnik3.Prezime = "Prezime 3";
                korisnik3.PhoneNumber = "062114412";
                korisnik3.Spol = 'Z';
                korisnik3.PosljednjiLoginDate = null;
                korisnik3.SjedisteId = _context.Drzava.Find(drzava.Id).Id;

                IdentityResult chkUser = await userMgr.CreateAsync(user, "$123abCdE!");
                IdentityResult chkUser1 = await userMgr.CreateAsync(korisnik1, "Pa$$w0rD=");
                IdentityResult chkUser2 = await userMgr.CreateAsync(korisnik2, "Ta$$t4turA=");
                IdentityResult chkUser3 = await userMgr.CreateAsync(korisnik3, "T3$tuS3r=");

                // Dodaje se rola "Administrator" na prethodno dodani nalog
                if (chkUser.Succeeded)
                {
                    var result1 = await userMgr.AddToRoleAsync(user, "Administrator");
                    var result2 = await userMgr.AddToRoleAsync(korisnik1, "Korisnik");
                    var result3 = await userMgr.AddToRoleAsync(korisnik2, "Korisnik");
                    var result4 = await userMgr.AddToRoleAsync(korisnik3, "Korisnik");
                }
            }

            return Content("Administrator dodan. Korisnička rola dodana. Ne pozivati ovu funkciju ponovo ako baza postoji.");
        }

        public IActionResult ForgotPassword()
        {
            ForgotPasswordVM model = new ForgotPasswordVM();
            return View();
        }

        public async Task<IActionResult> ResetujLozinku(ForgotPasswordVM model)
        {
            if (ModelState.IsValid)
            {
                var user = await userMgr.FindByEmailAsync(model.Email);
                if (user != null && await userMgr.IsEmailConfirmedAsync(user))
                {
                    var token = await userMgr.GeneratePasswordResetTokenAsync(user);
                    var passwordResetLink = Url.Action("ResetPassword", "Account",
                        new {email = model.Email, token = token}, Request.Scheme);

                    PosaljiMail(user, "Resetuj lozinku",
                        "<h3 style=\"color:green; font-weight:bold\"> Resetuj lozinku </h3>" +
                        "Kliknite na link ispod kako biste unijeli vašu novu lozinku. <br/><br/>" +
                        "<a href=\"" + passwordResetLink + "\"> Resetuj lozinku </a><br/>");

                    @TempData["sucess"] = "E-mail je poslan na vašu adresu";
                    @ViewBag.ErrorMsg = "Provjerite e-mail inbox i potvrdite promjenu vaše lozinke";

                    return PartialView("RegistrationMessage");
                }
                @TempData["sucess"] = "Dogodila se greška";
                @ViewBag.ErrorMsg = "Vaš račun nije aktivan. Prvo morate potvrditi vaš račun!";
                return PartialView("RegistrationMessage");
            }

            return View("ForgotPassword", model);
        }

        public IActionResult ResetPassword(string email, string token)
        {
            ResetPasswordVM model = new ResetPasswordVM()
            {
                Email = email,
                Token = token
            };
            return View(model);
        }

        public async Task<IActionResult> PromijeniLozinku(ResetPasswordVM model)
        {
            if (ModelState.IsValid)
            {
                var user = await userMgr.FindByEmailAsync(model.Email);
                if (user != null)
                {
                    var result = await userMgr.ResetPasswordAsync(user, model.Token, model.Password);
                    if (result.Succeeded)
                    {
                        return RedirectToAction("Login", new {msg = "Vaša lozinka je promijenjena"});
                    }
                    @TempData["sucess"] = "Dogodila se greška";
                    @ViewBag.ErrorMsg = "Molimo kontaktirajte administratora";
                    return PartialView("RegistrationMessage");
                }
            }
            @TempData["sucess"] = "Dogodila se greška";
            @ViewBag.ErrorMsg = "Molimo kontaktirajte administratora";
            return PartialView("RegistrationMessage");
        }

        public async Task<string> CheckUserName(string Username)
        {
            if (Username == null)
                return "2";
            var user = await userMgr.FindByNameAsync(Username);
            if (user != null)
                return "1";
            return "0";
        }

        public async Task<int> CheckEmail(string Email)
        {
            if (Email == null)
                return 2;
            var user = await userMgr.FindByEmailAsync(Email);
            if (user != null)
                return 1;
            return 0;
        }

    }
}