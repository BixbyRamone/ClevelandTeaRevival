using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ClevelandTeaRevival.Data;
using ClevelandTeaRevival.Models;
using Microsoft.AspNetCore.Identity;
using ClevelandTeaRevival.ViewModels;

namespace ClevelandTeaRevival.Controllers
{
    public class ShoppingCartController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _identityUser;

        public ShoppingCartController(ApplicationDbContext context, UserManager<IdentityUser> identityUser)
        {
            _context = context;
            _identityUser = identityUser;
        }

        public  ActionResult Index()
        {
            Transaction currentTrans;
            try
            {
                var currentUser = _identityUser.GetUserAsync(User);

                var currentCustomer = _context.Customers.SingleOrDefault(c => c.AspNetUserId == currentUser.Id.ToString());

                currentTrans = _context.Transactions.SingleOrDefault(t => t.CustomerId == currentCustomer.ID);
            }
            catch(Exception err)
            {
                return Content(err.ToString());
            }


            var transactionTabs = _context.TransactionTabs.Where(t => t.TransId == currentTrans.ID).ToList();

            var viewModel = new ShoppingCartViewModel
            {
                TransactionTabs = transactionTabs,
                Transaction = currentTrans
            };

            
            return View(viewModel);
        }
    }
}
