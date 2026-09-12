using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace HastaneMVC.Models
{
    public class BransModel :BaseEntity
    {
      
        [Required(ErrorMessage ="Zorunlu alan")]
        [StringLength(150)]
        public string BransAdi { get; set; }

        // Bire-Çok İlişki: Bir branşın birden fazla doktoru olabilir. bu sql tablosunda yazmaz  
        public ICollection<DoktorModel> Doktorlar { get; set; }

    }
}