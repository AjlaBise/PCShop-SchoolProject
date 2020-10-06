using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ClassLibrary.Models;
using Korzh.EasyQuery.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OnlineShop.ViewModels;

namespace OnlineShop.Controllers
{

    public class ProizvodiController : Controller
    {
        private readonly Context _context;

        public ProizvodiController(Context _db)
        {
            _context = _db;
        }
        public IActionResult PregledProizvoda(string naziv)
        {
            var proizvodiP = _context.Proizvod.Include(x => x.kategorija).Where(x => x.kategorija.NazivKategorije == naziv).ToList();

            return View(proizvodiP);
        }
        public IActionResult PregledSvih()
        {
            var pregledProizvoda = _context.Proizvod.Where(x => x.Kolicina > 0).Include(x => x.kategorija).ToList();
            return View(pregledProizvoda);
        }
      
        public async Task<IActionResult> Index (String searchTXT)
        {
          
            var proizvod = from m in _context.Proizvod select m;
            
            if(!string.IsNullOrEmpty(searchTXT))
            {
                
                proizvod = proizvod.Include(s=>s.kategorija).Where(s => s.NazivProizvoda.Contains(searchTXT));
            }
            return View(await proizvod.ToListAsync());
        }


    }
}