using ClassLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineShop.ViewModels
{
    public class NarudzbaVM
    {
        public DateTime DatumKreiranjaNarudzbe { get; set; }
        public Dostavljac Dostavljac { get; set; }
        public int DostavljacId { get; set; }
        public AppUser Narucioc { get; set; }
        public int NaruciocId { get; set; }
    }
     
    }
