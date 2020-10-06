using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ClassLibrary.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ReflectionIT.Mvc.Paging;

namespace OnlineShop.Controllers
{
    public class PostController : Controller
    {
        private readonly Context _context;
        public PostController(Context temp)
        {
            _context = temp;
        }
        public async Task<IActionResult> Index(int page = 1)
        {

            var query = _context.Post.Include(p => p.Autor).AsNoTracking().OrderBy(p => p.Id);
            var model = await PagingList.CreateAsync(query, 5, page);

            return View(model);
        }

        public async Task<IActionResult> Pregled(int postId)
        {
            Post model = await _context.Post.FindAsync(postId);
            return View(model);
        }

    }
}