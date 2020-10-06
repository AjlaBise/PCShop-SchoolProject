using ClassLibrary.Models;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;

namespace OnlineShop.ViewModels
{
    public class EditViewModel
    {
        public List<Kategorija> kategorije { get; set; }
        public List<Uvoznik> uvoznici { get; set; }
        public List<Proizvodjac> proizvodjaci { get; set; }
        public Proizvod proizvodEdit { get; set; } = new Proizvod();
        public IFormFile slikaProizvoda { get; set; }
        public double staraCijena { get; set; }
    }
}