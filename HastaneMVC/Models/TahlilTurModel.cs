using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace HastaneMVC.Models
{
    public class TahlilTurModel :BaseEntity
    {
     
        [Required]
        [StringLength(150)]
        public string Ad { get; set; }


        // Çoka-Çok İlişkinin ayağı: Bu tahlil türü hangi randevularda istendi?
        // Navigation Property" (Gezintsi Özelliği)
        //SQL'deki o JOIN işleminin C# dünyasındaki köprüsüdür.
        // Fiziksel bir kolon DEĞİLDİR: update-Database yapıp SQL'e baktığında RandevuTahlilleri diye bir kolon göremezsin.
        //Include ile bu köprüden (gezinti yolundan) geçip tahlilleri de toplayıp getireceğim.
        public ICollection<RandevuTahlilModel> RandevuTahlilleri { get; set; }


    }
}
