using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ClevelandTeaRevival.Data;
using ClevelandTeaRevival.Models;
using Microsoft.AspNetCore.Identity;
using ClevelandTeaRevival.Helpers;
using ClevelandTeaRevival.ViewModels;

namespace ClevelandTeaRevival.Controllers
{
    public class TransactionsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _identityUser;

        public TransactionsController(ApplicationDbContext context, UserManager<IdentityUser> identityUser)
        {
            _context = context;
            _identityUser = identityUser;
        }

        //[HttpPost]
        public ActionResult AddToCart(DetailsViewModel viewModel)
        {
            UserRegisterHelpers userRegisterHelpers = new UserRegisterHelpers(_context);

            viewModel.TransactionTab.TeaId = viewModel.Tea.ID;

            viewModel.TransactionTab.PricePerOz = viewModel.Tea.PricePerOz;
            viewModel.TransactionTab.PricePerLb = viewModel.Tea.PricePerLb;
            // viewModel.TransactionTab.Tea = viewModel.Tea;

            //get urrent AspNetUser
            var currentUserId = _identityUser.GetUserId(User);

            var customerTransaction = userRegisterHelpers.GetCustomerTransaction(currentUserId);

            // need to find any open transactions, see if current transactionTab exists on that,
            //then add  transactionTab if it doesn't exist

            viewModel.TransactionTab.TransId = customerTransaction.Transaction.ID;

            TransactionTab newTransTab = new TransactionTab();
            newTransTab = viewModel.TransactionTab;


            _context.Add(newTransTab);

            try
            {
                _context.SaveChanges();
            }
            catch (Exception x)
            {
                return Content(x.ToString());
            }
            
            

            return RedirectToAction("Index", "Shop");
        }   

        // GET: Transactions
        public async Task<IActionResult> Index(string id)
        {
            ShoppingCartHelpers shoppingCartHelpers = new ShoppingCartHelpers(_context);

            //get urrent AspNetUser
            var currentUser = await _identityUser.GetUserAsync(User);

            Customer currentCustomer = new Customer();

            //get customer associated with AspNetUserId
           if (currentUser != null)
            {
                currentCustomer = await _context.Customers
                                    .Where(c => c.AspNetUserId == currentUser.Id)
                                    .FirstOrDefaultAsync();
            }

           // Get any open transactions
            var currentTransaction = await _context.Transactions
                                     .Where(t => t.Customer == currentCustomer && t.Completed == false)
                                     .FirstOrDefaultAsync();

            bool isTransactionNew = false;

            //get Tea that was ordered
            string strId = HttpUtility.HtmlEncode(id);
            var allTeas = shoppingCartHelpers.GetAllTeas();

            var selectedTea = shoppingCartHelpers.GetTea(strId);

            var transactionTabs = await _context.TransactionTabs
                                    .Where(tt => tt.TransId == currentTransaction.ID)
                                    .ToListAsync();
            //List<TransactionTab> transactionTabs = new List<TransactionTab>();

            // if no current open transaction, create new transaction
            if (currentTransaction == null)
            {
                //create a new transaction 
                var transaction = shoppingCartHelpers.CreateNewTransaction(currentCustomer);
                currentTransaction = transaction;

                isTransactionNew = true;
            }
            else //get past transaction tabs
            {
                transactionTabs = shoppingCartHelpers.GetTransactionTabs(currentTransaction, allTeas);
            }

            //create transaction tab and add it to the db
            var newTransactionTab = shoppingCartHelpers.NewTransactionTab(selectedTea, currentTransaction);
            _context.Add(newTransactionTab[0]);


            if(transactionTabs.Count() > 0)
            {
                //transactionTabs = shoppingCartHelpers.ManageTransactionTab(transactionTabs, newTransactionTab[0]);
                transactionTabs.Add(newTransactionTab[0]);
            }
            else
            {
                transactionTabs = newTransactionTab;
            }

            //calculate Total, Add teas to object, etc...
            currentTransaction = shoppingCartHelpers.TransactionTotal(currentTransaction, transactionTabs);

            if(isTransactionNew == true)
            {
                _context.Add(currentTransaction);
            }
            else
            {
                _context.Update(currentTransaction);
            }
           
            _context.SaveChanges();

            var viewModel = new TransactionViewModel
            {
                TransactionTabs = transactionTabs,
                Transaction = currentTransaction
            };

            return View(transactionTabs);
        }

        // GET: Transactions/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var transaction = await _context.Transactions
                .FirstOrDefaultAsync(m => m.ID == id.ToString());
            if (transaction == null)
            {
                return NotFound();
            }

            return View(transaction);
        }

        // GET: Transactions/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Transactions/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,CustomerID,Total")] Transaction transaction)
        {
            if (ModelState.IsValid)
            {
                _context.Add(transaction);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(transaction);
        }

        // GET: Transactions/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var transaction = await _context.Transactions.FindAsync(id);
            if (transaction == null)
            {
                return NotFound();
            }
            return View(transaction);
        }

        // POST: Transactions/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,CustomerID,Total")] Transaction transaction)
        {
            if (id.ToString() != transaction.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(transaction);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TransactionExists(transaction.ID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(transaction);
        }

        // GET: Transactions/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var transaction = await _context.Transactions
                .FirstOrDefaultAsync(m => m.ID == id.ToString());
            if (transaction == null)
            {
                return NotFound();
            }

            return View(transaction);
        }

        // POST: Transactions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var transaction = await _context.Transactions.FindAsync(id);
            _context.Transactions.Remove(transaction);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TransactionExists(string id)
        {
            return _context.Transactions.Any(e => e.ID == id);
        }
    }
}
