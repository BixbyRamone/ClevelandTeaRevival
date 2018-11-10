using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ClevelandTeaRevival.Models
{
    [NotMapped]
    public class Customer
    {
        public int ID { get; set; }
        public string AspNetUserId { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string MailingAddress { get; set; }

        public ICollection<Transaction> Transactions { get; set; }
    }
}
