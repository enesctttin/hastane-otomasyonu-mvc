using System.ComponentModel.DataAnnotations;

namespace HastaneMVC.DTOs
{
    public class RandevuModelDTO
    {
        public int Id { get; set; }

        public DateTime TarihSaat { get; set; }
        public int HastaId { get; set; }
        public int DoktorId { get; set; }
        public string? DoktorNotu { get; set; }


        //  LİSTELEME İÇİN BİZİM EKLİYCEĞİMİZ ALANLAR

        public string? HastaAdSoyad { get; set; }

        public string? DoktorAdSoyad { get; set; }

        public string? BransAdi { get; set; }

        public int BransId { get; set; }  

        public Boolean? IsDeleted { get; set; }


    }
}




