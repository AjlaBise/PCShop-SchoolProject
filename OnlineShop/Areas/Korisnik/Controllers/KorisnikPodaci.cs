using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using ClassLibrary.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using OnlineShop.ViewModels;

namespace OnlineShop.Areas.Korisnik.Controllers
{
    [Area("Korisnik")]
    public class KorisnikPodaci : Controller
    {
       
            private UserManager<AppUser> userMgr { get; }
            private Context _context { get; }
            private IHttpContextAccessor _httpContextAccessor { get; }
            public KorisnikPodaci(UserManager<AppUser> mgr, Context context, IHttpContextAccessor accessor)
            {
                userMgr = mgr;
                _context = context;
                _httpContextAccessor = accessor;
            }

            [Authorize(Roles = "Korisnik")]
            public IActionResult Index(string msg = "")
            {
                return View();
            }

            [Authorize(Roles = "Korisnik")]
            public IActionResult PosaljiMailSvima()
            {
                return PartialView("PosaljiMailSvima");
            }

            [Authorize(Roles = "Korisnik")]
            public IActionResult PromijeniUsername()
            {
                return PartialView("PromijeniUsername");
            }
            [Authorize(Roles = "Korisnik")]
            public IActionResult PromijeniLozinku()
            {
                return PartialView("PromijeniLozinku");
            }
        [Authorize(Roles = "Korisnik")]
        public IActionResult PromijeniEmail()
        {
            return PartialView("PromijeniEmail");
        }
        [Authorize(Roles = "Korisnik")]
            public IActionResult PregledajProfil()
            {
                var model = _context.Users.Include(u => u.Sjediste)
                    .Single(u => u.Id == int.Parse(_httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value));
                return PartialView("PregledProfila", model);
            }

            [Authorize(Roles = "Korisnik")]
            public async Task<IActionResult> SpasiUsername(string userId, string noviUsername)
            {
                if (noviUsername == null)
                {
                    TempData["msg"] = "Unesite username";
                    return RedirectToAction(nameof(Index));
                }

                var user = await userMgr.FindByIdAsync(userId);
                user.UserName = noviUsername;

                await userMgr.UpdateAsync(user);

                TempData["msg"] = "Username uspješno promijenjen";
                return RedirectToAction(nameof(Index));
            }
        [Authorize(Roles = "Korisnik")]
        public async Task<IActionResult> SpasiEmail(string userId, string noviEmail)
        {
            if (noviEmail == null)
            {
                TempData["msg"] = "Unesite email";
                return RedirectToAction(nameof(Index));
            }

            var user = await userMgr.FindByIdAsync(userId);
            user.Email = noviEmail;

            await userMgr.UpdateAsync(user);

            TempData["msg"] = "Email uspješno promijenjen";
            return RedirectToAction(nameof(Index));
        }
        [Authorize(Roles = "Korisnik")]
            public async Task<IActionResult> SpasiLozinku(string userId, string staraLozinka, string novaLozinka)
            {
                var user = await userMgr.FindByIdAsync(userId);
                var x = await userMgr.ChangePasswordAsync(user, staraLozinka, novaLozinka);

                if (!x.Succeeded)
                {
                    TempData["msg"] = "Niste ispravno unijeli vašu staru lozinku";
                    return RedirectToAction(nameof(Index));
                }

                await userMgr.UpdateAsync(user);

                TempData["msg"] = "Lozinka uspješno promijenjena";
                return RedirectToAction(nameof(Index));
            }

        }

    
}