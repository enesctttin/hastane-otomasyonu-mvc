using HastaneMVC.DTOs;
using HastaneMVC.Models;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.Collections.Generic;

namespace HastaneMVC.ViewModels
{
    public class DoktorEkleVM
    {
        // açılır listeyi (Dropdown) dolduracak 

        [ValidateNever]  
        public IEnumerable<BransModelDTO> BransListesi { get; set; }

        public DoktorModelDTO YeniDoktor { get; set; }
    }
}