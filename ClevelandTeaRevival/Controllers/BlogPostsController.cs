using ClevelandTeaRevival.Data;
using ClevelandTeaRevival.Helpers;
using ClevelandTeaRevival.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace ClevelandTeaRevival.Controllers
{
    public class BlogPostsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public BlogPostsController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index(int? page)
        {

            var blogPosts = from b in _context.BlogPosts select b;


                blogPosts = blogPosts.OrderByDescending(b => b.CreationDate);

            int postCount = blogPosts.Count();

            int pageSize = 10;
           

            return View(await PaginatedList<BlogPost>.CreateAsync(blogPosts.AsNoTracking(), page ?? 1, pageSize));
        }
    }
}
