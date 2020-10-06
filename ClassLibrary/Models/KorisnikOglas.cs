using System;
using System.Collections.Generic;
using System.Text;

namespace ClassLibrary.Models
{
    public class KorisnikOglas
    {
        public int Id { get; set; }
        public AppUser Korisnik { get; set; }
        public int KorisnikId { get; set; }              

        public Oglas Oglas { get; set; }
        public int OglasId { get; set; }

        public DateTime DatumPrijave { get; set; }
        public string PathCV { get; set; }

    }

}
