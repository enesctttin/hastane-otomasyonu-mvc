using System.ComponentModel.DataAnnotations;

namespace HastaneMVC.DTOs
{
    public class RandevuTahlilModelDTO
    {


        public int Id { get; set; }
        public int RandevuId { get; set; }
        public int TahlilTuruId { get; set; }
        public string? Sonuc { get; set; }


        // Ekranda göstermek için 


        public string HastaAdSoyad { get; set; }

        public string DoktorAdSoyad { get; set; }

        public string TahlilAd { get; set; }



    }
}
