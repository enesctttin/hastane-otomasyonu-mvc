using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema; // ForeignKey için

namespace HastaneMVC.Models
{
    public class DoktorModel :BaseEntity
    {
        
        [StringLength(150)]
        [Required]
        public string AdSoyad { get; set; }

        public int BransId { get; set; }


        [ForeignKey("BransId")]
        public BransModel Brans { get; set; }

        // Bire-Çok İlişki: Bir doktorun birden fazla randevusu olabilir.
        public ICollection<RandevuModel> Randevular { get; set; }

    }
}
