using System.ComponentModel.DataAnnotations;

namespace HastaneMVC.DTOs
{
    public class BransModelDTO
    {

        public int Id { get; set; }

        [Required(ErrorMessage = "Branş adı zorunludur.")]
        public string BransAdi { get; set; }
    }
}
