using ClevelandTeaRevival.Data;
using ClevelandTeaRevival.Helpers.HelperModels;
using ClevelandTeaRevival.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ClevelandTeaRevival.Helpers;
using ClevelandTeaRevival.ViewModels;

namespace ClevelandTeaRevival.Helpers
{
    public class UserRegisterHelpers
    {
        private readonly ApplicationDbContext _context;

        public UserRegisterHelpers(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> RegisterCustomer(IdentityUser currentUser)
        {
            var customer = await _context.Customers
                                .Where(c => c.AspNetUserId == currentUser.Id)
                                .ToArrayAsync();
                if (customer.Length < 1)
            {
                Customer customerToAdd = new Customer();
                customerToAdd.AspNetUserId = currentUser.Id;

                _context.Add(customerToAdd);

                await _context.SaveChangesAsync();
                
            }
           

            return null;
        }

        public UserTransactionHelper GetCustomerTransaction(string currentUserId)
        {
            Customer currentCustomer = new Customer();

            //get customer associated with AspNetUserId
            if (currentUserId != null)
            {
                currentCustomer = _context.Customers
                                    .Where(c => c.AspNetUserId == currentUserId)
                                    .FirstOrDefault();
            }

            // Get any open transactions
            var currentTransaction = _context.Transactions
                                     .Where(t => t.Customer == currentCustomer && t.Completed == false)
                                     .FirstOrDefault();

            if (currentTransaction == null)
            {
                ShoppingCartHelpers shoppingCartHelpers = new ShoppingCartHelpers(_context);
                var newTransaction = shoppingCartHelpers.CreateNewTransaction(currentCustomer);

                currentTransaction = newTransaction;
            }

            

            UserTransactionHelper userTransactionHelper = new UserTransactionHelper
            {
                Transaction = currentTransaction,
                Customer = currentCustomer
            };

            return (userTransactionHelper);

        }
       
        public DetailsViewModel GetOrCreateTransactionTab(UserTransactionHelper customerTransaction, DetailsViewModel teaAndTransTab)
        {
            //bool isTransactionNew = false;

            teaAndTransTab.TransactionTab.TransId = customerTransaction.Transaction.ID;

            List<TransactionTab> transactionTabs = new List<TransactionTab>();

           /* try
            {
                transactionTabs = _context.TransactionTabs
                                    .Where(tt => tt.TransId == customerTransaction.Transaction.ID)
                                    .ToList();
            }
            catch
            {

            }

            

            /*ShoppingCartHelpers shoppingCartHelpers = new ShoppingCartHelpers(_context);

            if (customerTransaction.Transaction == null)
            {
                //create a new transaction 
                var transaction = shoppingCartHelpers.CreateNewTransaction(customerTransaction.Customer);
                customerTransaction.Transaction = transaction;

                isTransactionNew = true;
            }
            else //get past transaction tabs
            {
               transactionTabs.Add(teaAndTransTab.TransactionTab);
            }*/

            return (teaAndTransTab);

        }
    }
}
