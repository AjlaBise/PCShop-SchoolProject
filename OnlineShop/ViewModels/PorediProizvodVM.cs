using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineShop.ViewModels
{
    public class PorediProizvodVM
    {

        public List<StavkeZaPorediti> StavkePorediti { get; set; }

        public class StavkeZaPorediti
        {
            public int ProizvodID { get; set; }
            public string NazivProizvoda { get; set; }
            public double Cijena { get; set; }
            public string Proizvodjac { get; set; }
            public string Slika { get; set; }
        }
    
    }
}
