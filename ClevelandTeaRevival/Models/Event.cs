using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ClevelandTeaRevival.Models
{
    public class Event
    {
        public int ID { get; set; }
        public string EventName { get; set; }
        public string Description { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime EventDate { get; set; }
    }
}
