using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClevelandTeaRevival.Models
{
    public class Customer
    {
        public int ID { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string MailingAddress { get; set; }

        public ICollection<Transaction> Transactions { get; set; }
    }
}
