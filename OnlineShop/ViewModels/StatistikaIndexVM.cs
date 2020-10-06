using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using ClassLibrary.Models;

namespace OnlineShop.ViewModels
{
    public class StatistikaIndexVM
    {
        public List<NarudzbaStavka> ListaStavki { get; set; }
        public List<Narudzba> ListaNarudzbi { get; set; }
        public List<Proizvod> ListaProizvoda { get; set; }
        public List<AppUser> ListaKorisnika { get; set; }
        public List<int> Godine { get; set; }
        


        public KeyValuePair<Proizvod, int> NajprodavanijIProizvod { get; set; }
        public KeyValuePair<Proizvod, int> NajmanjeprodavanijIProizvod { get; set; }
        public List<KeyValuePair<AppUser, int>> TopKupci { get; set; } = new List<KeyValuePair<AppUser, int>>();
        public int BrojNaruzbi { get; set; }
        public Proizvod NajvecaKolicina { get; set; }
        public Proizvod NajmanjaKolicina { get; set; }

        public KeyValuePair<Proizvod, int> GetNajprodavanijiProizvod(bool max)
        {
            if (max)
            {
                var najprodavaniji = ListaStavki
                    .GroupBy(k => k.ProizvodId)
                    .Select(k => new { id = k.Key, count = k.Count() })
                    .OrderByDescending(k => k.count).ToList()[0];

                return new KeyValuePair<Proizvod, int>(ListaProizvoda.Find(x => x.ProizvodID == najprodavaniji.id), najprodavaniji.count);
            }
            else
            {
                var not_najprodavaniji = ListaStavki
                    .GroupBy(k => k.ProizvodId)
                    .Select(k => new {id = k.Key, count = k.Count()})
                    .OrderBy(k => k.count).ToList()[0];

                return new KeyValuePair<Proizvod, int>(ListaProizvoda.Find(x => x.ProizvodID == not_najprodavaniji.id),
                    not_najprodavaniji.count);
            }
        }

        public void SetTopKupce()
        {
            var sortiranaLista = ListaNarudzbi
                .GroupBy(k => k.NaruciocId)
                .Select(k => new {id = k.Key, count = k.Count()})
                .OrderByDescending(k => k.count).ToList();

            for(int i = 0; i < 3; i++)
                TopKupci.Add(new KeyValuePair<AppUser, int>
                    (ListaKorisnika.Find(x => x.Id == sortiranaLista[i].id),
                        sortiranaLista[i].count)); 
        }

    }
}
