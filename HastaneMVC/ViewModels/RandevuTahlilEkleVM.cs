using HastaneMVC.DTOs;
using HastaneMVC.Models;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.Collections.Generic;
namespace HastaneMVC.ViewModels
{
    public class RandevuTahlilEkleVM
    {

        // Veritabanına kaydedilecek asıl eşleşme nesnesi
        [ValidateNever]
        public RandevuTahlilModel YeniRandevuTahlil { get; set; }



        // Açılır listede gösterilecek liste olarak at
        [ValidateNever]
        public IEnumerable<RandevuModelDTO> RandevuListesi { get; set; }

        [ValidateNever]
        public IEnumerable<TahlilTurModelDTO> TahlilTurListesi { get; set; }




    }
}
