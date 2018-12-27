using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ClevelandTeaRevival.Models
{
    public class BlogPost
    {
        public int ID { get; set; }
        public string Title { get; set; }
        public string Text { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public string CreationDate { get; set; }

        public BlogPost()
        {
            CreationDate = DateTime.Now.ToString();
        }
    }
}
