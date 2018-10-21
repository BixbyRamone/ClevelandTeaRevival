using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ClevelandTeaRevival.Models
{
    public class Transaction
    {
        public int ID { get; set; }
        public int CustomerID { get; set; }

        public ICollection<Tea> Teas { get; set; }

        [DataType(DataType.Currency)]
        public decimal Total { get; set; }

        public Customer Customer { get; set; }
    }
}
