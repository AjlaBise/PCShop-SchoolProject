using ClassLibrary.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OnlineShop.Constants;
using OnlineShop.Extensions;
using OnlineShop.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineShop.Areas.Korisnik.Controllers
{

    [Area("Korisnik")]
    [Authorize(Roles = "Korisnik")]
    public class PorediProizvod : Controller
    {
        private readonly Context _context;

        private KorpaProizvodVM KorpaSessionModel
        {

            get => HttpContext.Session.GetObject<KorpaProizvodVM>(SessionKeys.Poredi);
            set => HttpContext.Session.SetObject(SessionKeys.Poredi, value);
        }

        public PorediProizvod(Context _db)
        {
            _context = _db;
        }

        public IActionResult Index(int? proizvodId = null)
        {

            var porediSessija = KorpaSessionModel ?? new KorpaProizvodVM
            {
                StavkeKorpe = new List<KorpaProizvodVM.StavkaKorpe>()
            };
           
            if (proizvodId != null)
            {
                var proizvod = _context.Proizvod.Include(p=>p.uvoznik).Include(p=>p.Proizvodjac).FirstOrDefault(p => p.ProizvodID == proizvodId.GetValueOrDefault());

                if (porediSessija.StavkeKorpe.Any(sk => sk.ProizvodId == proizvod.ProizvodID))
                {
                    var index = porediSessija.StavkeKorpe.FindIndex(sk => sk.ProizvodId == proizvod.ProizvodID);
                    porediSessija.StavkeKorpe[index].Kolicina += 1;
                    porediSessija.StavkeKorpe[index].Cijena = proizvod.Cijena;
                }
                else
                {
                    porediSessija.StavkeKorpe.Add(new KorpaProizvodVM.StavkaKorpe
                    {
                        ProizvodId = proizvod.ProizvodID,
                        Kolicina = 1,
                        Cijena = proizvod.Cijena,
                        NazivProizvoda = proizvod.NazivProizvoda,
                        Snizen = proizvod.snizen,
                        UrlSlike = proizvod.imageLocation,
                        Opis = proizvod.OpisProizvoda,
                        Uvoznik=proizvod.uvoznik.NazivUvoznika,
                        Proizvodjac=proizvod.Proizvodjac.NazivProizvodjaca
                       
                    });
                }

            }

            KorpaSessionModel = porediSessija;


            return View(porediSessija);
        }
        [HttpPost]
        public IActionResult UkloniStavku(int proizvodId)
        {
            var porediSessija = KorpaSessionModel;
            var index = porediSessija.StavkeKorpe.FindIndex(sk => sk.ProizvodId == proizvodId);
            if (index != -1)
                porediSessija.StavkeKorpe.RemoveAt(index);

            KorpaSessionModel = porediSessija;

            return Ok();
        }

    }
}
