using System;
using System.Collections.Generic;
using System.Text;

namespace ClassLibrary.Models
{
    public class Post
    {
        public int Id { get; set; }
        public string Naslov { get; set; }
        public string Sadrzaj { get; set; }
        public AppUser Autor { get; set; }
        public int AutorId { get; set; }
        public string ImageLocation { get; set; }
        public DateTime DatumObjave { get; set; }

        public string GetDatumAsText()
        {
            var trenutnoVrijeme = DateTime.Now;
            var razlika = (trenutnoVrijeme - DatumObjave).Days;

            if (razlika == 0) return "Danas";
            if (razlika == 1) return "Jučer";

            if (razlika > 365)
            {
                var godine = razlika / 365;
                return "Prije " + godine + " godina";
            }

            if (razlika > 31)
            {
                var mjeseci = razlika / 31;
                return "Prije " + mjeseci + " mjeseci";
            }

            if (razlika < 31)
                return "Prije " + razlika + " dana";

            return "";
        }
    }
}
