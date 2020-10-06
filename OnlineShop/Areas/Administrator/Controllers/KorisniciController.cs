using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ClassLibrary.Models;
using MailKit.Net.Smtp;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MimeKit;
using OnlineShop.ViewModels;
using ReflectionIT.Mvc.Paging;

namespace OnlineShop.Areas.Administrator.Controllers
{
    [Area("Administrator")]
    public class KorisniciController : Controller
    {
        private UserManager<AppUser> userMgr { get; }
        private Context _context { get; }
        public KorisniciController(UserManager<AppUser> mgr, Context context)
        {
            userMgr = mgr;
            _context = context;
        }
        [Authorize(Roles = "Administrator")]
        public IActionResult Index()
        {
            return View();
        }

        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> GetUsersPartial(int page = 1, string search="none")
        {
            IOrderedQueryable<AppUser> query;
            int pageSize = 8;
            if (search == "none" || search == null)
            {
                query = userMgr.Users.AsNoTracking().OrderBy(k => k.Id);
            }
            else
            {
                query = userMgr.Users
                    .Where(u => u.UserName.Contains(search) || u.Email.Contains(search)
                                || u.Ime.Contains(search) || u.Prezime.Contains(search)
                                || (u.Ime + " " + u.Prezime).Contains(search))
                    .AsNoTracking().OrderBy(k => k.Id);

                if (query.Count() >= 5)
                    pageSize = query.Count();
            }

            KorisnikIndexVM model = new KorisnikIndexVM()
            {
                korisnici = await PagingList.CreateAsync(query, pageSize, page)
            };
            if (model.korisnici.Count() == 0)
                return Content("<p class=\" title text text-center \">Ne postoji nijedan zapis sa ključnom riječi '" + search + "'. </p>");

            return PartialView("KorisniciPartial", model);
        }

        [Authorize(Roles = "Administrator")]
        public async Task<string> PosaljiToken(string UserID)
        {
            var user = await userMgr.FindByIdAsync(UserID);
            if (user == null)
                return "error";

            var token = await userMgr.GenerateEmailConfirmationTokenAsync(user);
            var confirmationLink = Url.Action("ConfirmEmail", "Account",
                new { userID = user.Id, token = token }, Request.Scheme);
            var text = "<h3 style=\"color:green; font-weight:bold\"> Aktivacijski kod za prijavu </h3>" +
                       "Molimo vas da potvrdite vaš račun klikom na link ispod. <br/><br/>" +
                       "<a href=\"" + confirmationLink + "\"> Potvrdi moj račun </a><br/>";

            PosaljiPoruku(user, text, "Potvrda korisničkog naloga");
            return user.UserName;
        }
        [Authorize(Roles = "Administrator")]
        public async Task<string> PosaljiMail(string userId, string text, string title="Obavijest")
        {

            if (userId == "All")
            {
                foreach (var korisnik in await userMgr.GetUsersInRoleAsync("Korisnik"))
                {
                    if(korisnik.Id == 1002 || korisnik.Id == 22995)
                        PosaljiPoruku(korisnik, text, title);
                }

                return "Uspješno poslan mail svim korisnicima";
            }

            var user = await userMgr.FindByIdAsync(userId);
            if (user == null)
                return "error";

            PosaljiPoruku(user, text, "PC Shop: Obavijest");
            return user.UserName;
        }
        [Authorize(Roles = "Administrator")]
        public void PosaljiPoruku(AppUser user, string text, string subject)
        {
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress("PC Shop", "ajla_bise@outlook.com"));
            message.To.Add(new MailboxAddress(user.Ime, user.Email));
            message.Subject = subject;
            message.Body = new TextPart("html")
            {
                Text = text
            };
            using (var client = new SmtpClient())
            {
                client.Connect("smtp.gmail.com", 587, false);
                client.Authenticate("ajla_bise@outlook.com", "student.notify.123");
                client.Send(message);
                client.Disconnect(true);
            }
        }
        [Authorize(Roles = "Administrator")]
        public IActionResult EmailPartial(string id, string user)
        {
            Tuple<string, string> model = new Tuple<string, string>(item1: id, item2: user);
            return PartialView("Email", model);
        }
    }
}