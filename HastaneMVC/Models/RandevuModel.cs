using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema; // ForeignKey için

namespace HastaneMVC.Models
{
    public class RandevuModel :BaseEntity
    {

        public DateTime TarihSaat { get; set; }

        public int HastaId { get; set; }
        [ForeignKey("HastaId")]
        public HastaModel Hasta { get; set; }

        public int DoktorId { get; set; }

        [ForeignKey("DoktorId")]

        public DoktorModel Doktor { get; set; }

        [StringLength(500)]
        public string? DoktorNotu { get; set; }

        public ICollection<RandevuTahlilModel> RandevuTahlilleri { get; set; }
    }
}
