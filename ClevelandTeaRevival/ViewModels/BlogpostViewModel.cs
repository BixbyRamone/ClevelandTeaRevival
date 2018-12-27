using ClevelandTeaRevival.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClevelandTeaRevival.ViewModels
{
    public class BlogPostViewModel
    {
        public BlogPost BlogPost { get; set; }
        public List<BlogPost> BlogPosts { get; set; }
    }
}
