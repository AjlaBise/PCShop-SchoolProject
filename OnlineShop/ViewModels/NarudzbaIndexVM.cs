using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ClassLibrary.Models;

namespace OnlineShop.ViewModels
{
    public class NarudzbaIndexVM
    {
        public List<Narudzba> narudzbe { get; set; }
        public List<NarudzbaStavka> narudzbeStavke { get; set; }
        public List<Proizvod> proizvodi { get; set; }
    }
}
