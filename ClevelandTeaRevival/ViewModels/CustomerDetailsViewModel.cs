using ClevelandTeaRevival.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClevelandTeaRevival.ViewModels
{
    public class CustomerDetailsViewModel
    {
        public Customer Customer { get; set; }
        public IdentityUser IdentityUser { get; set; }
        public List<Transaction> Transactions { get; set; }
        public List<TransactionTab> TransactionTabs { get; set; }
    }
}
