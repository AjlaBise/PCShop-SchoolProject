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
    public class ProizvodDetaljiController : Controller
    {
        private readonly Context _context;
        public ProizvodDetaljiController (Context _db)
        {
            _context = _db;
        }

        [Area("Korisnik")]
        [Authorize(Roles = "Korisnik")]
        public IActionResult PrikazDetalja(int id)
        {

            var proizvodiC = _context.Proizvod.Where(x => x.ProizvodID == id).Include(x => x.Proizvodjac).Include(x=> x.kategorija).SingleOrDefault();
            
     
            return View(proizvodiC);
        }
    }
}