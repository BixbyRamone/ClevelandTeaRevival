using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClevelandTeaRevival.Models
{
    public class TransactionTab
    {
        public int ID { get; set; }
        public string TransId { get; set; }
        public int TeaId { get; set; }
        public Tea Tea { get; set; }
        public int Ozs { get; set; }
        public int Lbs { get; set; }
    }
}
