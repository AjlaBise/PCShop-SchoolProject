using System.Linq;
using ClassLibrary.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OnlineShop.ViewModels;

namespace OnlineShop.Areas.Administrator.Controllers
{
    [Area("Administrator")]
    public class HomeController : Controller
    {
        private UserManager<AppUser> userMgr { get; }
        private Context _context { get; }
        public HomeController(UserManager<AppUser> mgr, Context context)
        {
            userMgr = mgr;
            _context = context;
        }
        [Authorize(Roles = "Administrator")]
        public IActionResult Index()
        {
            AdminIndexVM model = new AdminIndexVM()
            {

                BrojAktivnihKorisnika = _context.Users.Where(x => x.EmailConfirmed).ToList().Count(),
                BrojRegistrovanihKorisnika = _context.Users.ToList().Count(),
                BrojPotvrdjenihNarudzbi = _context.Narudzba.Where(n => n.Aktivna).ToList().Count(),
                BrojProizvoda = _context.Proizvod.ToList().Count(),

                BrojObjavljenihPostova = _context.Post.ToList().Count(),
                BrojPrijavaNaOglas = _context.KorisnikOglas.ToList().Count(),
                ProsjekRecenzija = _context.Recenzija.Average(x => x.Ocjena)
            };

            return View(model);
        }
    }
}