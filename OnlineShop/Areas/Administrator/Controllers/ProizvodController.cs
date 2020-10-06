using ClassLibrary.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OnlineShop.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using ReflectionIT.Mvc.Paging;

namespace OnlineShop.Areas.Administrator.Controllers
{
    [Area("Administrator")]
    public class ProizvodController : Controller
    {
        private readonly Context _context;
        private readonly IWebHostEnvironment _hostingEnvironment;
        public ProizvodController(Context _temp, IWebHostEnvironment hostingEnvironment)
        {
            _context = _temp;
            _hostingEnvironment = hostingEnvironment;
        }

        //[Authorize(Roles = "Administrator")]
        //public async Task<ActionResult> Index()
        //{
        //    return View();
        //}
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> GetProizvode(int page = 1, string search = "none", int sortBy = 1)
        {
            IOrderedQueryable<Proizvod> query;
            int pageSize = 8;

            if (search == "none" || search == null)
            {
                if(sortBy == 2)
                {
                    query = _context.Proizvod
                        .Include(p => p.Proizvodjac)
                        .Include(p => p.uvoznik)
                        .Include(p => p.kategorija)
                    .AsNoTracking().OrderBy(p => p.Cijena);
                }
                else if (sortBy == 3)
                {
                    query = _context.Proizvod
                        .Include(p => p.Proizvodjac)
                        .Include(p => p.uvoznik)
                        .Include(p => p.kategorija)
                    .AsNoTracking().OrderByDescending(p => p.Cijena);
                }
                else
                {
                    query = _context.Proizvod
                        .Include(p => p.Proizvodjac)
                        .Include(p => p.uvoznik)
                        .Include(p => p.kategorija)
                    .AsNoTracking().OrderBy(p => p.ProizvodID);
                }
            }
            else
            {
                if (sortBy == 2)
                {
                    query = _context.Proizvod
                        .Include(p => p.Proizvodjac)
                        .Include(p => p.uvoznik)
                        .Include(p => p.kategorija)
                        .Where(p => p.NazivProizvoda.Contains(search) || p.kategorija.NazivKategorije.Contains(search)
                        || p.Proizvodjac.NazivProizvodjaca.Contains(search)
                        || p.uvoznik.NazivUvoznika.Contains(search))
                        .AsNoTracking().OrderBy(p => p.Cijena);
                }
                else if (sortBy == 3)
                {
                    query = _context.Proizvod
                        .Include(p => p.Proizvodjac)
                        .Include(p => p.uvoznik)
                        .Include(p => p.kategorija)
                        .Where(p => p.NazivProizvoda.Contains(search) || p.kategorija.NazivKategorije.Contains(search)
                        || p.Proizvodjac.NazivProizvodjaca.Contains(search)
                        || p.uvoznik.NazivUvoznika.Contains(search))
                        .AsNoTracking().OrderByDescending(p => p.Cijena);
                }
                else
                {
                    query = _context.Proizvod
                        .Include(p => p.Proizvodjac)
                        .Include(p => p.uvoznik)
                        .Include(p => p.kategorija)
                        .Where(p => p.NazivProizvoda.Contains(search) || p.kategorija.NazivKategorije.Contains(search)
                        || p.Proizvodjac.NazivProizvodjaca.Contains(search)
                        || p.uvoznik.NazivUvoznika.Contains(search))
                        .AsNoTracking().OrderBy(p => p.ProizvodID);
                }
                if (query.Count() >= 5)
                    pageSize = query.Count();
            }

            var model = await PagingList.CreateAsync(query, pageSize, page);
            if (model.Count() == 0)
                return Content("<p class=\" title text text-center \">Ne postoji nijedan zapis sa ključnom riječi '" + search + "'. </p>");
            return PartialView("ProizvodiPartial", model);
        }

        [Authorize(Roles = "Administrator")]
        public ActionResult AddProizvod()
        {
            ProizvodViewModel model = new ProizvodViewModel()
            {
                uvoznici = _context.Uvoznik.ToList(),
                kategorije = _context.Kategorija.ToList(),
                proizvodjaci = _context.Proizvodjac.ToList()
            };
            return View(model);
        }

        [HttpPost]
        [Authorize(Roles = "Administrator")]
        public async Task<ActionResult> DodajNoviProizvodv2(Proizvod proizvod, IFormFile slikaProizvoda)
        {

            _context.Proizvod.Add(proizvod);
            _context.SaveChanges();

            if (slikaProizvoda != null)
            {
                if (slikaProizvoda.Length > 0)
                {
                    var path = _hostingEnvironment.WebRootPath + "\\pictures\\proizvodi";
                    var filename = "Proizvod-" + _context.Proizvod
                        .Where(x => x.ProizvodID == proizvod.ProizvodID)
                        .FirstOrDefault().ProizvodID.ToString() + 
                        Path.GetExtension(slikaProizvoda.FileName);

                    proizvod.imageLocation = "/pictures/proizvodi/" + filename;

                    using (var stream = new FileStream(Path.Combine(path, filename), FileMode.Create))
                    {
                        await slikaProizvoda.CopyToAsync(stream);
                    }
                }
            }
            else
            {
                proizvod.imageLocation = "/pictures/no_image.svg";
            }

            _context.SaveChanges();

            return RedirectToAction("Index");
        }

        [Authorize(Roles = "Administrator")]
        public ActionResult UkloniProizvod(int id)
        {
            Proizvod p = _context.Proizvod.Where(x => x.ProizvodID == id).SingleOrDefault();

            var extension_delete = Path
                .GetExtension(p.imageLocation);

            var path_delete = _hostingEnvironment.WebRootPath
                + "\\pictures\\proizvodi\\" + "Proizvod-" + id + extension_delete;

            if (System.IO.File.Exists(path_delete))
            {
                System.IO.File.Delete(path_delete);
            }

            List<Recenzija> rec = _context.Recenzija
                .Where(x => x.ProizvodId == id).ToList();
            List<NarudzbaStavka> nas = _context.NarudzbaStavka
                .Where(x => x.ProizvodId == id).ToList();

            _context.Recenzija.RemoveRange(rec);
            _context.NarudzbaStavka.RemoveRange(nas);

            _context.Proizvod.Attach(p);
            _context.Proizvod.Remove(p);
            _context.SaveChanges();

            return Redirect("~/Administrator/Proizvod");
        }

        [Authorize(Roles = "Administrator")]
        public ActionResult EditRedirect(int id)
        {

            EditViewModel model = new EditViewModel()
            {
                uvoznici = _context.Uvoznik.ToList(),
                kategorije = _context.Kategorija.ToList(),
                proizvodEdit = _context.Proizvod.Find(id),
                proizvodjaci = _context.Proizvodjac.ToList(),
                staraCijena = _context.Proizvod.Find(id).Cijena
            };
            
            return View("EditProizvod", model);
        }

        [Authorize(Roles = "Administrator")]
        public async Task<ActionResult> EditujProizvod(EditViewModel mdl)
        {
            Proizvod p = _context.Proizvod.Find(mdl.proizvodEdit.ProizvodID);

            p.NazivProizvoda = mdl.proizvodEdit.NazivProizvoda;
            p.Cijena = mdl.proizvodEdit.Cijena;
            p.Kolicina = mdl.proizvodEdit.Kolicina;
            p.kategorijaId = mdl.proizvodEdit.kategorijaId;
            p.uvoznikId = mdl.proizvodEdit.uvoznikId;
            p.OpisProizvoda = mdl.proizvodEdit.OpisProizvoda;
            p.ProizvodjacId = mdl.proizvodEdit.ProizvodjacId;

            p.snizen = mdl.proizvodEdit.Cijena < mdl.staraCijena ? true : false;

            if (mdl.slikaProizvoda != null)
            {
                if (mdl.slikaProizvoda.Length > 0)
                {
                    var extension_delete = Path.GetExtension(mdl.proizvodEdit.imageLocation);
                    var path_delete = _hostingEnvironment.WebRootPath 
                        + "\\pictures\\proizvodi\\" + mdl.proizvodEdit.ProizvodID + extension_delete;

                    if (System.IO.File.Exists(path_delete))
                    {
                        System.IO.File.Delete(path_delete);
                    }

                    var path = _hostingEnvironment.WebRootPath + "\\pictures\\proizvodi";

                    var filename = "Proizvod-" + _context.Proizvod
                        .Where(x => x.ProizvodID == mdl.proizvodEdit.ProizvodID)
                        .FirstOrDefault().ProizvodID.ToString() +
                        Path.GetExtension(mdl.slikaProizvoda.FileName);

                    p.imageLocation = "/pictures/proizvodi/" + filename;

                    using (var stream = new FileStream(Path.Combine(path, filename), FileMode.Create))
                    {
                        await mdl.slikaProizvoda.CopyToAsync(stream);
                    }
                }
            }

            _context.SaveChanges();
            return Redirect("~/Administrator/Proizvod");
        }

        [Authorize(Roles = "Administrator")]
        public ActionResult Detaljno(int id)
        {
            var product = _context.Proizvod.Find(id);

            if (product == null)
                return View("Error");

            var model = _context.Proizvod
                .Include(p => p.kategorija)
                .Include(p => p.uvoznik).ToList().Find(p => p.ProizvodID.Equals(product.ProizvodID));

            return View(model);
            
        }

        [Authorize(Roles = "Administrator")]
        public async Task<bool> DodajKategoriju(string kategorija)
        {
            if (kategorija != null)
            {
                _context.Kategorija.Add(new Kategorija() { NazivKategorije = kategorija });
                await _context.SaveChangesAsync();
                return true;
            }
            else
                return false;
        }

        [Authorize(Roles = "Administrator")]
        public async Task<bool> DodajDobavljaca(string naziv, string adresa, string kontakt)
        {
            if (naziv != null && adresa != null && kontakt != null)
            {
                _context.Dostavljac.Add(new Dostavljac()
                {
                    Adresa = adresa,
                    KontaktTel = kontakt,
                    NazivDostave = naziv
                });
                await _context.SaveChangesAsync();
                return true;
            }

            return false;
        }

        [Authorize(Roles = "Administrator")]
        public IActionResult DodajUvoznika()
        {
            UvoznikVM model = new UvoznikVM()
            {
                sjedista = _context.Drzava.ToList()
            };
            return View(model);
        }

        [Authorize(Roles = "Administrator")]
        public async Task<bool> SnimiUvoznika(Uvoznik uvoznik)
        {
            _context.Uvoznik.Add(uvoznik);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}