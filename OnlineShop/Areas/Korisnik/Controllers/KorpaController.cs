using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Threading.Tasks;
using ClassLibrary.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.FileSystemGlobbing.Internal;
using Newtonsoft.Json;
using OnlineShop.Constants;
using OnlineShop.Extensions;
using OnlineShop.ViewModels;
using SQLitePCL;

namespace OnlineShop.Areas.Korisnik.Controllers
{
    [Area("Korisnik")]
    [Authorize(Roles = "Korisnik")]
    public class KorpaController : Controller
    {
        private readonly Context _context;

        private KorpaProizvodVM KorpaSessionModel {

            get => HttpContext.Session.GetObject<KorpaProizvodVM>(SessionKeys.Korpa);
            set => HttpContext.Session.SetObject(SessionKeys.Korpa, value);
        }

        public KorpaController(Context _db)
        {
            _context = _db;
        }

        public IActionResult Index(int? proizvodId = null) {

            var korpaSessija = KorpaSessionModel ?? new KorpaProizvodVM
            {
                StavkeKorpe = new List<KorpaProizvodVM.StavkaKorpe>()
            };

            if (proizvodId != null) {
                var proizvod = _context.Proizvod.FirstOrDefault(p => p.ProizvodID == proizvodId.GetValueOrDefault());

                if (korpaSessija.StavkeKorpe.Any(sk => sk.ProizvodId == proizvod.ProizvodID))
                {
                    var index = korpaSessija.StavkeKorpe.FindIndex(sk => sk.ProizvodId == proizvod.ProizvodID);
                    korpaSessija.StavkeKorpe[index].Kolicina += 1;
                    korpaSessija.StavkeKorpe[index].Cijena = proizvod.Cijena;
                }
                else
                {
                    korpaSessija.StavkeKorpe.Add(new KorpaProizvodVM.StavkaKorpe
                    {
                        ProizvodId = proizvod.ProizvodID,
                        Kolicina = 1,
                        Cijena = proizvod.Cijena,
                        NazivProizvoda = proizvod.NazivProizvoda,
                        Snizen = proizvod.snizen,
                        UrlSlike = proizvod.imageLocation,
                        Opis = proizvod.OpisProizvoda
                    });
                }

            }

            KorpaSessionModel = korpaSessija;


            return View(korpaSessija);
        }

        [HttpPost]
        public IActionResult AzurirajKolicinuStavke(int proizvodId,int kolicina) {
            
            var korpaSessija = KorpaSessionModel;

            var index = korpaSessija.StavkeKorpe.FindIndex(sk => sk.ProizvodId == proizvodId);
            if(index != -1)
                korpaSessija.StavkeKorpe[index].Kolicina = kolicina;

            KorpaSessionModel = korpaSessija;

            return Ok();
        }

        [HttpPost]
        public IActionResult UkloniStavku(int proizvodId)
        {
            var korpaSessija = KorpaSessionModel;
            var index = korpaSessija.StavkeKorpe.FindIndex(sk => sk.ProizvodId == proizvodId);
            if(index != -1)
                korpaSessija.StavkeKorpe.RemoveAt(index);

            KorpaSessionModel = korpaSessija;

            return Ok();
        }

        [HttpPost]
        public IActionResult Checkout()
        {
            var korpaSessija = KorpaSessionModel;

            if (korpaSessija is null)
            {
                return RedirectToAction("/Account/Login/");
            }

            if (korpaSessija.StavkeKorpe.Count == 0)
            {
                ModelState.AddModelError("cart_empty", "Prazna korpa");
                return View("Index", korpaSessija);
            }

            try
            {
                var narudzba = new Narudzba
                {
                    Potvrdjena = false,
                    Aktivna = true,
                    DatumKreiranjaNarudzbe = DateTime.Now,
                    NaruciocId = 23995,
                    DostavljacId = 2,
                };

                if (korpaSessija.StavkeKorpe?.Any() ?? false)
                {
                    narudzba.NarudzbaStavke = korpaSessija.StavkeKorpe.Select(sk => new NarudzbaStavka
                    {
                        Cijena = sk.Cijena,
                        Kolicina = sk.Kolicina,
                        ProizvodId = sk.ProizvodId
                    }).ToList();
                }

                _context.Narudzba.Add(narudzba);
                _context.SaveChanges();

                KorpaSessionModel = null;

            }
            catch (Exception e)
            {
                // redirekcija na view sa greskom
            }

            return View("Checkout"); //redirekcija na view sa prikazom narudzbe (detalji, stavke) nesto kao racun
        }

    }
       

}