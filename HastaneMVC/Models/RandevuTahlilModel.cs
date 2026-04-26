using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HastaneMVC.Models
{
    public class RandevuTahlilModel : BaseEntity
    {
        
        public int RandevuId { get; set; }
        [ForeignKey("RandevuId")]
        public RandevuModel Randevu { get; set; }

        public int TahlilTuruId { get; set; }
        [ForeignKey("TahlilTuruId")]

        public TahlilTurModel TahlilTuru { get; set; }

        public string? Sonuc { get; set; }

    }
}
