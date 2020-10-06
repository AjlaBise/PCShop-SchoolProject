using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ClassLibrary.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OnlineShop.ViewModels;
using Org.BouncyCastle.Math.EC.Rfc7748;

namespace OnlineShop.Areas.Korisnik.Controllers
{
    public class RecenzijaController : Controller
    {
        private Context _db;

        public RecenzijaController(Context context)
        {
            _db = context;
        }

        [Area("Korisnik")]
        [Authorize(Roles = "Korisnik")]

        public IActionResult Index(int id)
        {
            
            var model = new RecenzijeVM
            {
                id=id,
                procjecnaOcjena =(float)_db.Recenzija.Where(x => x.ProizvodId == id).Select(x => x.Ocjena).Average(),
                rows =_db.Recenzija.Where(x=>x.ProizvodId == id).Select(x=> new RecenzijeVM.Row
                {
                    ProizvodID=x.ProizvodId,
                    NazivProizvoda=x.Proizvod.NazivProizvoda,
                    Komentar=x.Komentar,
                    Ocjena=x.Ocjena,
                   
                   
                }).ToList(),     

            };

            return View(model);
        }
        [Area("Korisnik")]
        [Authorize(Roles = "Korisnik")]
        public IActionResult Dodaj(int id)
        {
            var model = new DodajRecenzijuVM
            {
                ProizvodID=id,
                Ocjena=_db.Recenzija.Select(x=>x.Ocjena).FirstOrDefault(),
                Komentar=_db.Recenzija.Select(x=>x.Komentar).FirstOrDefault()
            };
            return View(model);
        }
        [Area("Korisnik")]
        [Authorize(Roles = "Korisnik")]
        public IActionResult Snimi(DodajRecenzijuVM rec)
        {
            var m = new Recenzija
            {
                ProizvodId = rec.ProizvodID,
                Komentar = rec.Komentar,
                Ocjena = rec.Ocjena
            };
            _db.Recenzija.Add(m);
            _db.SaveChanges();


            return RedirectToAction("Index", new {id=rec.ProizvodID });
        }

    }
}
