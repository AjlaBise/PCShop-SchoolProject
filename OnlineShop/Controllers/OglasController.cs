using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ClassLibrary.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OnlineShop.ViewModels;

namespace OnlineShop.Areas.Korisnik.Controllers
{
    public class OglasController : Controller
    {
        private readonly Context _context;

        public OglasController(Context _db)
        {
            _context = _db;
        }


        public IActionResult Prikazi()
        {
            var oglasi = _context.Oglas.Where(x => x.Aktivan == true).ToList();
            return View(oglasi);
        }

       public IActionResult DetaljiOglasa(int id)
        {
            var detaljiO = _context.Oglas.Where(x => x.Id == id).SingleOrDefault();
            return View(detaljiO); 
        }

      public IActionResult Prijava(int id)
        {
            return View("Prijava");
        }
    }
}