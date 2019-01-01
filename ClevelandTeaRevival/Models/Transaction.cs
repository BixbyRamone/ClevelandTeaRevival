using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ClevelandTeaRevival.Models
{
    public class Transaction
    {
        public string ID { get; set; }

        public int CustomerId { get; set; }
        public Customer Customer { get; set; }

        public ICollection<Tea> Teas { get; set; }

        [DataType(DataType.Currency)]
        public decimal Total { get; set; }

        public bool Completed { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public string CompletionDate { get; set; }
       

        public IQueryable<TransactionTab> TransactionTabs { get; set; }



        public Transaction()
        {
            Completed = false;
            ID = Guid.NewGuid().ToString();
        }

    }
}
