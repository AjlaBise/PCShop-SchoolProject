using System;
using System.Collections.Generic;
using System.Text;

namespace ClassLibrary.Models
{
    public class Dostavljac
    {
        public int Id { get; set; }
        public string NazivDostave { get; set; }
        public string Adresa { get; set; }
        public string KontaktTel { get; set; }
    }
}
