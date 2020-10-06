
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.InteropServices;
using ServiceStack.DataAnnotations;

namespace ClassLibrary.Models
{
    public class Recenzija
    {
        [PrimaryKey]
        public int RecenzijaID { get; set; }
        public int Ocjena { get; set; }
        public string Komentar { get; set; }
        public Proizvod Proizvod { get; set; }
        public int ProizvodId { get; set; }
    }
}
