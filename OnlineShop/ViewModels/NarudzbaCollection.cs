using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ClassLibrary.Models;

namespace OnlineShop.ViewModels
{
    public class NarudzbaCollection
    {
        public Narudzba narudzba { get; set; }
        public List<Proizvod> proizvodi { get; set; }
        public ProizvodContainer _container { get; set; } = new ProizvodContainer();
        public double UkupnaCijena { get; set; }

        public void GenerisiCijenu()
        {
            double temp = 0;
            foreach (var proizvod in proizvodi)
                temp += proizvod.Cijena;

            UkupnaCijena = Math.Round(temp, 2);
        }

        public void PopuniContainer()
        {
            foreach(var p in proizvodi)
            {
                _container.Dodaj(p);
            }
        }

    }
}

public class ProizvodContainer
{
    public List<Proizvod> Proizvodi { get; set; } = new List<Proizvod>();
    public List<int> Kolicine { get; set; } = new List<int>();
    public List<double> Sume { get; set; } = new List<double>();

    public bool Dodaj(Proizvod p)
    {
        if (Proizvodi.Contains(p))
        {
            int lokacija = Proizvodi.IndexOf(p);
            Kolicine[lokacija] += 1;
            Sume[lokacija] += p.Cijena;
            return false;
        }
        Proizvodi.Add(p);
        Kolicine.Add(1);
        Sume.Add(p.Cijena);
        return true;
    }

}