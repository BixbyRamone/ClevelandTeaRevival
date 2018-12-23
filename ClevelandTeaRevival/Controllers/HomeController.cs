using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ClevelandTeaRevival.Models;
using ClevelandTeaRevival.Helpers;
using Stripe;
using Microsoft.AspNetCore.Identity;
using ClevelandTeaRevival.Data;
using Microsoft.EntityFrameworkCore;

namespace ClevelandTeaRevival.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _identityUser;


        public HomeController(ApplicationDbContext context, UserManager<IdentityUser> identityUser)
        {
            _context = context;
            _identityUser = identityUser;
        }

        public async Task<IActionResult> Index()
        {
            UserRegisterHelpers userRegisterHelpers = new UserRegisterHelpers(_context);

            var currentUser = await _identityUser.GetUserAsync(User);

            if (currentUser != null)
            {
                await userRegisterHelpers.RegisterCustomer(currentUser);
            }

            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult Charge(string StripeEmail, string StripeToken)
        {
            var customers = new StripeCustomerService();
            var charges = new StripeChargeService();

            var customer = customers.Create(new StripeCustomerCreateOptions
            {
                Email = StripeEmail,
                SourceToken = StripeToken
            });

            var charge = charges.Create(new StripeChargeCreateOptions
            {
                Amount = 500,
                Description = "Sample Charge",
                Currency = "usd",
                CustomerId = customer.Id
            });

            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
