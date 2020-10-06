using ClassLibrary.Models;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;

namespace OnlineShop.ViewModels
{
    public class ProizvodViewModel
    {
        public List<Proizvod> proizvodi { get; set; }
        public List<Kategorija> kategorije { get; set; }
        public List<Uvoznik> uvoznici { get; set; }
        public List<Proizvodjac> proizvodjaci { get; set; }
        public Proizvod proizvod { get; set; } = new Proizvod();
        public IFormFile slikaProizvoda { get; set; }
        public string srchTXT { get; set; }
    }
}