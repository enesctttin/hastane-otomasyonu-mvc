using HastaneMVC.DTOs;
using HastaneMVC.Models;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.Collections.Generic;

namespace HastaneMVC.ViewModels
{
    public class RandevuEkleVM
    {
        // 1. Veritabanına kaydedilecek olan asıl Randevu bilgisi 
        public RandevuModelDTO YeniRandevu { get; set; }


        [ValidateNever]
        public IEnumerable<DoktorModelDTO> DoktorListesi { get; set; }

        [ValidateNever]
        public IEnumerable<HastaModelDTO> HastaListesi { get; set; }


        [ValidateNever]
        public IEnumerable<BransModelDTO> BransListesi { get; set; }

    }
}