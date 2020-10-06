using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClassLibrary.Models
{
    public class AppUser : IdentityUser<int>
    {
        public string Ime { get; set; }
        public string Prezime { get; set; }
        public char Spol { get; set; }
        public DateTime DatumRegistracije { get; set; }
        public Drzava Sjediste { get; set; }
        public int SjedisteId { get; set; }
        public DateTime? PosljednjiLoginDate { get; set; }
        public bool Pretplacen { get; set; }
        public List<Narudzba> GetNarudzbe()
        {
            Context connection = new Context();
            var narudzbe = connection.Narudzba.Where(x => x.NaruciocId == this.Id).ToList();
            connection.Dispose();
            return narudzbe;
        }
    }
}
