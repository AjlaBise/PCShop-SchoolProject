using ClassLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineShop.ViewModels
{
    public class KorpaProizvodVM
    {
        public List<StavkaKorpe> StavkeKorpe { get; set; }
        public double Ukupno => StavkeKorpe?.Sum(sk => sk.Ukupno) ?? 0;

        public class StavkaKorpe {
            public int ProizvodId { get; set; }
            public string NazivProizvoda { get; set; }
            public int Kolicina { get; set; }
            public double Cijena { get; set; }
            public string UrlSlike { get; set; }
            public string Opis { get; set; }
            public bool Snizen { get; set; }
            public string Uvoznik { get; set; }
            public string Proizvodjac { get; set; }
            public double Ukupno => Kolicina * Cijena;
        }
    }
}
