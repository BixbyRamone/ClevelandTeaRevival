using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ClevelandTeaRevival.Models
{
    public class TransactionTab //: IEnumerable<TransactionTab>
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        public string TransId { get; set; }
        public int TeaId { get; set; }
        public Tea Tea { get; set; }
        public int Ozs { get; set; }
        public int Lbs { get; set; }

       /* public IEnumerator<TransactionTab> GetEnumerator()
        {
           // throw new NotImplementedException();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
           // throw new NotImplementedException();
        }*/
    }
}
