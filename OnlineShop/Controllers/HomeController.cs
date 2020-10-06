using ClassLibrary.Models;
using Microsoft.AspNetCore.Mvc;
using OnlineShop.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace OnlineShop.Controllers
{
    public class HomeController : Controller
    {
        private readonly Context context;
        public HomeController(Context temp)
        {
            context = temp;
        }
        public IActionResult Index()
        {
            IndexVM model = new IndexVM()
            {
                proizvodi = context.Proizvod.OrderBy(r => Guid.NewGuid()).Take(9).ToList(),
                postovi = context.Post.Include(p => p.Autor).OrderByDescending(p => p.DatumObjave).Take(3).ToList(),
                oglasi = context.Oglas.OrderBy(o => o.DatumObjave).Take(3).ToList()
            };
            return View(model);
        }
        public IActionResult FillDatabase()
        {
            if (DataFill.IsEmptyDB(context))
            {
                if (DataFill.LoadData(context) && DataFill.LoadOglasi(context))
                    return Content("Baza uspjesno popunjena.");
                else
                    return Content("Nemoguce popuniti tabele. Potrebno je obrisati čitavu bazu i ponovo uraditi migracije i update da bi mogli ponovo generisati podatke.");
            }
            return Content("Baza je vec puna. Potrebno je obrisati citavu bazu pa ponovo generisati podatke.");
        }
        public IActionResult EmptyDatabase()
        {
            DataFill.EmptyDatabase(context);
            return Content("Podaci su obrisani. Ako želite ponovo generisati podatke, morate obrisati bazu i ponovo uraditi migracije i update.");

        }
        public IActionResult Poruka()
        {
            ViewData["Message"] = "Vaš artikal je dodan u korpu";
            return View("Poruka");
        }
    }
}