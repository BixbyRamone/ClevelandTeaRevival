using ClevelandTeaRevival.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClevelandTeaRevival.ViewModels
{
    public class ShoppingCartViewModel
    {
        public List<TransactionTab> TransactionTabs { get; set; }
        public Transaction Transaction { get; set; }
    }
}
