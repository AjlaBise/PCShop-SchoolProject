using ClassLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ReflectionIT.Mvc.Paging;

namespace OnlineShop.ViewModels
{
    public class KorisnikIndexVM
    {
        public PagingList<AppUser> korisnici { get; set; }
    }
}
