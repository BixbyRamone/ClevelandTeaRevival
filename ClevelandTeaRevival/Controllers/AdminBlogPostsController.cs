using ClevelandTeaRevival.Data;
using ClevelandTeaRevival.Models;
using ClevelandTeaRevival.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClevelandTeaRevival.Controllers
{
    public class AdminBlogPostsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AdminBlogPostsController(ApplicationDbContext context)
        {
            _context = context;
        }

        public ActionResult Index(string sortOrder)
        {
            var blogposts = _context.BlogPosts.OrderByDescending(b => b.CreationDate).ToList();

            var viewModel = new BlogPostViewModel
            {
                BlogPosts = blogposts
            };
            return View(viewModel);
        }

        public ActionResult Create(BlogPost blogPost)
        {
            if (blogPost == null)
            {
                return RedirectToAction("Index", "AdminBlogPosts");
            }

            _context.Add(blogPost);

            _context.SaveChanges();
        
            return RedirectToAction("Index", "AdminBlogPosts");
        }
    }
}
