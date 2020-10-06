using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineShop.ViewModels
{
    public class AdminIndexVM
    {
        public int BrojRegistrovanihKorisnika { get; set; }
        public int BrojAktivnihKorisnika { get; set; }
        public int BrojProizvoda { get; set; }
        public int BrojPotvrdjenihNarudzbi { get; set; }

        public int BrojObjavljenihPostova { get; set; }
        public double ProsjekRecenzija { get; set; }
        public int BrojPrijavaNaOglas { get; set; }

    }
}
