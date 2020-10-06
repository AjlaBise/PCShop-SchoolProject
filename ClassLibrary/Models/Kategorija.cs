using ServiceStack.DataAnnotations;

namespace ClassLibrary.Models
{
    public class Kategorija
    {
        [PrimaryKey]
        public int KategorijaID { get; set; }
        public string NazivKategorije { get; set; }
    }
}