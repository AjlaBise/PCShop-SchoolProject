using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ClassLibrary.Models;

namespace OnlineShop.ViewModels
{
    public class StatistikaNarudzbeVM
    {

        public List<Narudzba> ListaNarudzbi { get; set; }
        public List<KeyValuePair<int, int>> NarudzbePoGodini { get; set; } = new List<KeyValuePair<int, int>>();

        public void SetNarudzbePoGodini(int? godina)
        {
            var narudzbe = ListaNarudzbi
                .Where(a => a.DatumKreiranjaNarudzbe.Year == godina)
                .GroupBy(x => x.DatumKreiranjaNarudzbe.Month)
                .Select(n => new { BrojNaruzbi = n.Count(), Mjesec = n.Key })
                .OrderBy(p => p.Mjesec).ToList();

            for (int i = 1; i <= 12; i++)
            {
                if (narudzbe.Find(x => x.Mjesec == i) == null)
                {
                    NarudzbePoGodini.Add(new KeyValuePair<int, int>(i, 0));
                }
                else
                {
                    NarudzbePoGodini.Add(new KeyValuePair<int, int>(i, narudzbe.Find(n => n.Mjesec == i).BrojNaruzbi));
                }
            }
        }
    }
}
