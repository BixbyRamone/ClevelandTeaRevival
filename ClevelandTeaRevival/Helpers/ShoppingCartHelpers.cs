using System;
using System.Web;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ClevelandTeaRevival.Data;
using ClevelandTeaRevival.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;

namespace ClevelandTeaRevival.Helpers
{
    public class ShoppingCartHelpers
    {
        private readonly ApplicationDbContext _context;

        public ShoppingCartHelpers(ApplicationDbContext context)
        {
            _context = context;
        }

        public Transaction CreateNewTransaction(Customer customer)
        {
            Transaction newTransaction = new Transaction();

            newTransaction.Customer = customer;
            
            return (newTransaction);
        }

        public Tea GetTea(string strId)
        {

            int intId;
            Int32.TryParse(strId, out intId);



            var tea = _context.Teas
               .Where(t => t.ID == intId)
               .FirstOrDefault();

            tea.Ozs = 1;

            Tea teaList = new Tea();
              teaList = (tea);

            return (teaList);
        }

        public Transaction TransactionTotal(Transaction currentTransaction, List<TransactionTab> transTabArray)
        {
            decimal transTotal = 0;

            currentTransaction.Teas = new List<Tea>();

            foreach (TransactionTab t in transTabArray)
            {
                decimal ozTotal = t.Tea.PricePerOz * t.Ozs;
                decimal lbsTotal = t.Tea.PricePerLb * t.Lbs;

                transTotal = transTotal + ozTotal + lbsTotal;

                if ( currentTransaction.Teas.IndexOf(t.Tea) == -1)
                {
                    currentTransaction.Teas.Add(t.Tea);
                }
               
                
            }

            currentTransaction.Total = transTotal;           
            

            return (currentTransaction);
        }

        public  List<TransactionTab> GetTransactionTabs(Transaction transaction, List<Tea> allTeas)
        {
            var transactionTabs =  _context.TransactionTabs
                                        .Where(tt => tt.TransId == transaction.ID)
                                        .ToList();

            foreach(TransactionTab tt in transactionTabs)
            {
                tt.Tea = allTeas.Find(t => t.ID == tt.TeaId);
            }

            return (transactionTabs);
        }

        public List<TransactionTab> NewTransactionTab(Tea tea, Transaction transaction)
        {
            TransactionTab transactionTab = new TransactionTab {Tea = tea,
                                                                TransId = transaction.ID,
                                                                TeaId = tea.ID,
                                                                Ozs = 1};
            List<TransactionTab> newTransactionAsList = new List<TransactionTab>();
            newTransactionAsList.Add(transactionTab);
            return (newTransactionAsList);
        }

        public List<Tea> GetAllTeas()
        {
            var allteas = _context.Teas.ToList();

            return (allteas);
        }

        public List<TransactionTab> ManageTransactionTab(List<TransactionTab> transactionTabs, TransactionTab currentTab)
        {
            
            var isPresent = transactionTabs.IndexOf(currentTab);
            if (isPresent == -1)
            {
                transactionTabs.Add(currentTab);
            }
            else
            {
                transactionTabs[isPresent].Ozs = transactionTabs[isPresent].Ozs + 1;
            }

            return (transactionTabs);
        }
    }
}
