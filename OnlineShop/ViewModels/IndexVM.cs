using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ClassLibrary.Models;

namespace OnlineShop.ViewModels
{
    public class IndexVM
    {
        public List<Proizvod> proizvodi { get; set; }
        public List<Oglas> oglasi { get; set; }
        public List<Post> postovi { get; set; }
       
    }
}
