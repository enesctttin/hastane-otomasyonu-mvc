using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace HastaneMVC.Models
{
    public class HastaModel :BaseEntity
    {
     
        [Required(ErrorMessage = "Zorunlu alan")]
        [StringLength(11)]
        public string TcNo { get; set; } = null!;

        [Required(ErrorMessage = "Zorunlu alan")]
        [StringLength(150)]
        public string AdSoyad { get; set; }

        // Bire-Çok İlişki: Bir hastanın birden fazla randevusu olabilir.
        public ICollection<RandevuModel> Randevular { get; set; }


    }
}
