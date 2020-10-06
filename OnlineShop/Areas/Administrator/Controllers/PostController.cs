using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using AspNetCore;
using ClassLibrary.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OnlineShop.ViewModels;
using ReflectionIT.Mvc.Paging;

namespace OnlineShop.Areas.Administrator.Controllers
{
    [Area("Administrator")]
    public class PostController : Controller
    {

        private readonly Context _context;
        private readonly IWebHostEnvironment _hostingEnvironment;

        public PostController(Context context, IWebHostEnvironment henv)
        {
            _context = context;
            _hostingEnvironment = henv;
        }

        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> Index(int page = 1)
        {
            var query = _context.Post.Include(x => x.Autor).AsNoTracking().OrderByDescending(p => p.DatumObjave);
            var model = await PagingList.CreateAsync(query, 3, page);

            return View(model);
        }
        [Authorize(Roles = "Administrator")]
        public IActionResult DodajPost()
        {
            Post model = new Post();
            return View(model);
        }
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> SnimiPost(Post post, IFormFile slika)
        {

            post.DatumObjave = DateTime.Now;
            _context.Post.Add(post);
            _context.SaveChanges();

            if (slika != null)
            {
                if (slika.Length > 0)
                {
                    var path = _hostingEnvironment.WebRootPath + "\\pictures\\oglasi";
                    var filename = "Oglas-" + _context.Post
                        .Where(x => x.Id == post.Id)
                        .FirstOrDefault().Id.ToString() +
                        Path.GetExtension(slika.FileName);

                    await using (var stream = new FileStream(Path.Combine(path, filename), FileMode.Create))
                    {
                        await slika.CopyToAsync(stream);
                    }
                    post.ImageLocation = "/pictures/oglasi/" + filename;
                }
            }
            else
            {
                post.ImageLocation = "/pictures/oglasi/no_image_post.png";
            }

            _context.SaveChanges();

            return RedirectToAction("Index", "Post");
        }
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> ObrisiPost(int? id)
        {
            if (id == null)
                return NotFound();
            var post = await _context.Post.FindAsync(id);

            if (post == null)
                return NotFound();

            var extension_delete = Path
                .GetExtension(post.ImageLocation);

            var path_delete = _hostingEnvironment.WebRootPath
                + "\\pictures\\oglasi\\" + "Oglas-" + id + extension_delete;

            if (System.IO.File.Exists(path_delete))
            {
                System.IO.File.Delete(path_delete);
            }

            _context.Post.Remove(post);
            await _context.SaveChangesAsync();

            return RedirectToAction("Index");
        }
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> EditPost(int? id)
        {
            var model = new EditPostVM()
            {
                postEdit = await _context.Post.FindAsync(id)
            };

            return View(model);
        }
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> SpasiPromjene(EditPostVM model, IFormFile slika)
        {
            Post p = await _context.Post.FindAsync(model.postEdit.Id);
            p.AutorId = model.postEdit.AutorId;
            p.DatumObjave = DateTime.Now;
            p.Naslov = model.postEdit.Naslov;
            p.Sadrzaj = model.postEdit.Sadrzaj;

            if (slika != null)
            {
                if (slika.Length > 0)
                {

                    var extension_delete = Path.GetExtension(p.ImageLocation);
                    var path_delete = _hostingEnvironment.WebRootPath
                        + "\\pictures\\oglasi\\" + "Oglas-" + model.postEdit.Id + extension_delete;

                    if (System.IO.File.Exists(path_delete))
                    {
                        System.IO.File.Delete(path_delete);
                    }

                    var path = _hostingEnvironment.WebRootPath + "\\pictures\\oglasi";
                    var filename = "Oglas-" + _context.Post
                        .Where(x => x.Id == model.postEdit.Id)
                        .FirstOrDefault().Id.ToString() +
                        Path.GetExtension(slika.FileName);

                    await using (var stream = new FileStream(Path.Combine(path, filename), FileMode.Create))
                    {
                        await slika.CopyToAsync(stream);
                    }
                    p.ImageLocation = "/pictures/oglasi/" + filename;
                }
            }

            await _context.SaveChangesAsync();
            return RedirectToAction("Index", "Post");
        }

    }
}