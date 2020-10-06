using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using ServiceStack.Caching;
using System;
using System.Collections.Generic;
using System.Text;

namespace ClassLibrary.Models
{
    public class NarudzbaStavka
    {

        public int Id { get; set; }
        public Proizvod Proizvod { get; set; }
        public int ProizvodId { get; set; }
        public Narudzba Narudzba { get; set; }
        public int NarudzbaId { get; set; }
        public int Kolicina { get; set; }
        public double Cijena { get; set; }



    }
}
