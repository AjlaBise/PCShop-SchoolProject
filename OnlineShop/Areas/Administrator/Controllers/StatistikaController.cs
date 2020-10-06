using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ClassLibrary.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;
using OnlineShop.ViewModels;

namespace OnlineShop.Areas.Administrator.Controllers
{
    [Area("Administrator")]
    public class StatistikaController : Controller
    {

        private UserManager<AppUser> userMgr { get; }
        private Context _context { get; }
        private int now = DateTime.Today.Year;
        public StatistikaController(UserManager<AppUser> mgr, Context context)
        {
            userMgr = mgr;
            _context = context;
        }

        [Authorize(Roles = "Administrator")]
        public IActionResult Index(int? godina = null)
        {
            StatistikaIndexVM model = new StatistikaIndexVM()
            {
                BrojNaruzbi = _context.Narudzba.Count(),
                ListaKorisnika = userMgr.Users.ToList(),
                ListaStavki = _context.NarudzbaStavka.Include(x => x.Proizvod).ToList(),
                ListaProizvoda = _context.Proizvod.ToList(),
                ListaNarudzbi = _context.Narudzba.Include(x => x.Narucioc).ToList(),
                NajvecaKolicina = _context.Proizvod.OrderByDescending(x => x.Kolicina).ToList()[0],
                NajmanjaKolicina = _context.Proizvod.OrderBy(x => x.Kolicina).ToList()[0],
                Godine = _context.Narudzba.Select(x => x.DatumKreiranjaNarudzbe.Year).Distinct().ToList()
            };

            model.NajprodavanijIProizvod = model.GetNajprodavanijiProizvod(true);
            model.NajmanjeprodavanijIProizvod = model.GetNajprodavanijiProizvod(false);
            model.SetTopKupce();

            if (godina == null)
                ViewBag.year = 2020;
            else
                ViewBag.year = godina;


            return View(model);
        }

        public IActionResult NarudzbaPartial(int? godina)
        {
            StatistikaNarudzbeVM model = new StatistikaNarudzbeVM()
            {
                ListaNarudzbi = _context.Narudzba.Include(x => x.Narucioc).ToList()
            };
            if (godina == null)
            {
                ViewBag.year = 2020;
                model.SetNarudzbePoGodini(DateTime.Today.Year);
            }
            else
            {
                ViewBag.year = godina;
                model.SetNarudzbePoGodini(godina);
            }

            return PartialView("NarudzbeGraph", model);
        }

    }
}