using ServiceStack.DataAnnotations;
using System.Collections.Generic;
using System.Linq;

namespace ClassLibrary.Models
{
    public class Proizvod
    {
        [PrimaryKey]
        public int ProizvodID { get; set; }
        public string NazivProizvoda { get; set; }
        public double Cijena { get; set; }
        public int Kolicina { get; set; }
        public string OpisProizvoda { get; set; }
        public Kategorija kategorija { get; set; }
        public int kategorijaId { get; set; }
        public Uvoznik uvoznik { get; set; }
        public int uvoznikId { get; set; }
        public string imageLocation { get; set; }
        public bool snizen { get; set; } = false;
        public Proizvodjac Proizvodjac { get; set; }
        public int ProizvodjacId { get; set; }
        public ICollection<NarudzbaStavka> NarudzbaStavke { get; set; }

        public List<Recenzija> getRecenzije()
        {
            Context conn = new Context();
            List<Recenzija> recs = conn.Recenzija.Where(rec => rec.ProizvodId == this.ProizvodID).ToList();
            conn.Dispose();
            return recs;
        }
    }
}