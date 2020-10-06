using System;
using System.Collections.Generic;
using System.Text;

namespace ClassLibrary.Models
{
    public class Narudzba
    {
        public int Id { get; set; }
        public DateTime DatumKreiranjaNarudzbe { get; set; }
        public bool Aktivna { get; set; }
        public Dostavljac Dostavljac { get; set; }
        public int DostavljacId { get; set; }
        public AppUser Narucioc { get; set; }
        public int NaruciocId { get; set; }
        public bool Potvrdjena { get; set; }
        public ICollection<NarudzbaStavka> NarudzbaStavke { get; set; } 
    }
}
