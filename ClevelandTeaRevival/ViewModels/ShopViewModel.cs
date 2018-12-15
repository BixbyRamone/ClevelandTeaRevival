using ClevelandTeaRevival.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClevelandTeaRevival.ViewModels
{
    public class ShopViewModel
    {
        public List<Tea> Teas { get; set; }
        public TransactionTab TransactionTab { get; set; }
    }
}
