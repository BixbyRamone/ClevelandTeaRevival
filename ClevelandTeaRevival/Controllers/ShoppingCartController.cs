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
using ClevelandTeaRevival.Helpers;

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

        public async Task<IActionResult> Index()
        {
            UserRegisterHelpers userRegisterHelpers = new UserRegisterHelpers(_context);
            ShoppingCartHelpers shoppingCartHelpers = new ShoppingCartHelpers(_context);
            //TransactionHelpers transactionHelpers = new TransactionHelpers();

            var currentUser = await _identityUser.GetUserAsync(User);

            var transObj = userRegisterHelpers.GetCustomerTransaction(currentUser.Id);           
           


            var transactionTabs = _context.TransactionTabs.Where(t => t.TransId == transObj.Transaction.ID).ToList();

            transactionTabs = shoppingCartHelpers.AssignTeasToTabs(transactionTabs);

            var viewModel = new ShoppingCartViewModel
            {
                TransactionTabs = transactionTabs,
                Transaction = transObj.Transaction
            };

            viewModel.Transaction = shoppingCartHelpers.TransactionTotal(viewModel.Transaction, viewModel.TransactionTabs);
                        
            return View(viewModel);
        }
    }
}
