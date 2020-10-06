using System;
using System.Collections.Generic;
using System.Text;

namespace ClassLibrary.Models
{
    public class BankovniRacun
    {
        public int Id { get; set; }
        public AppUser Korisnik { get; set; }
        public int KorisnikId { get; set; }
        public Banka Banka { get; set; }
        public int BankaId { get; set; }
        public string BrojRacuna { get; set; }
        public DateTime DatumOtvaranjaRacuna { get; set; }
        public double StanjeNaRacunu { get; set; }
    }
}
