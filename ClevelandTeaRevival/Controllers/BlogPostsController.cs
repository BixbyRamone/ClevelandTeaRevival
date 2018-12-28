using ClevelandTeaRevival.Data;
using Microsoft.AspNetCore.Mvc;
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

        public ActionResult Index()
        {
            var blogPosts = _context.BlogPosts.OrderByDescending(b => b.CreationDate).ToList();

            return View(blogPosts);
        }
    }
}
