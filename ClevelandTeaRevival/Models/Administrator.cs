using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClevelandTeaRevival.Models
{
    public class Administrator
    {
        public int ID { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }

        public ICollection<BlogPost> BlogPosts { get; set; }
        public ICollection<Event> CreatedEvents { get; set; }
    }
}
