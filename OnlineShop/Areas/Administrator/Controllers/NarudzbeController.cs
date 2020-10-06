using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ClassLibrary.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Razor.Language.CodeGeneration;
using Microsoft.EntityFrameworkCore;
using OnlineShop.ViewModels;
using ReflectionIT.Mvc.Paging;

namespace OnlineShop.Areas.Administrator.Controllers
{
    [Area("Administrator")]
    public class NarudzbeController : Controller
    {
        private UserManager<AppUser> userMgr { get; }
        private Context _context { get; }

        private static int? filter = 0;

        public NarudzbeController(UserManager<AppUser> mgr, Context context)
        {
            userMgr = mgr;
            _context = context;
        }

        [Authorize(Roles = "Administrator")]
        public IQueryable<NarudzbaCollection> GenerisiNarudzbe()
        {
            NarudzbaIndexVM model = new NarudzbaIndexVM()
            {
                narudzbe = _context.Narudzba.Include(n => n.Narucioc)
                    .Include(n => n.Dostavljac).ToList(),
                narudzbeStavke = _context.NarudzbaStavka.ToList(),
                proizvodi = _context.Proizvod.Include(p => p.uvoznik).ToList()
            };

            var kolekcija = new List<NarudzbaCollection>();
            foreach (var narudzba in model.narudzbe)
            {
                kolekcija.Add(new NarudzbaCollection()
                {
                    narudzba = narudzba,
                    proizvodi = new List<Proizvod>(),
                    _container = new ProizvodContainer()
                });

                foreach (var stavka in model.narudzbeStavke)
                {
                    if (narudzba.Id == stavka.NarudzbaId)
                    {
                        kolekcija.Where(x => x.narudzba == narudzba).SingleOrDefault().proizvodi
                            .Add(model.proizvodi.Where(p => p.ProizvodID == stavka.ProizvodId).SingleOrDefault());
                    }
                }
            }

            foreach (var x in kolekcija)
            {
                x.GenerisiCijenu();
                x.PopuniContainer();
            }

            return kolekcija.AsQueryable().AsNoTracking();
        }

        [Authorize(Roles = "Administrator")]
        public IActionResult Index(int? orderBy, int page = 1)
        {

            if (orderBy != null)
                filter = orderBy;

            var narudzbe = GenerisiNarudzbe();
            IQueryable<NarudzbaCollection> query;

            // Prikaz svih narudzbi
            if (filter == 0)
                query = narudzbe
                    .OrderByDescending(k => k.narudzba.DatumKreiranjaNarudzbe);

            // Prikaz aktivnih narudzbi
            else if (filter == 1)
                query = narudzbe.Where(k => k.narudzba.Aktivna == true)
                    .OrderByDescending(k => k.narudzba.DatumKreiranjaNarudzbe);

            // Prikaz neaktivnih narudzbi
            else if (filter == 2)
                query = narudzbe.Where(k => !k.narudzba.Aktivna);

            // Prikaz aktivnih nepotvrđenih narudzbi
            else if (filter == 3)
                query = narudzbe.Where(k => k.narudzba.Aktivna && !k.narudzba.Potvrdjena);

            else
                query = narudzbe.OrderBy(k => k.narudzba.Id);

            var model = PagingList.Create(query, 5, page);

            TempData["feelter"] = filter;
            return View(model);
        }

        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> PotvrdiNarudzbu(int? narudzbaId)
        {
            if (narudzbaId == null)
                return Content("Narudžba ne postoji");

            var narudzba = await _context.Narudzba.FindAsync(narudzbaId);

            if (narudzba.Potvrdjena)
                return Content("Narudžba je već potvrđena");

            narudzba.Potvrdjena = true;
            await _context.SaveChangesAsync();

            return Content("Narudžba potvrđena");
        }

    }
}