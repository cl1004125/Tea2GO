using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;

namespace Tea2GO.Models
{
    public class Drink
    {
        public int DrinkID{get; set;}
        [Display(Name = "Tea")]
        [Required]
        public string STea {get; set;}
        [Display(Name = "sweet tea")]
        [Required]
        public string UTea {get; set;}
        [Display(Name = "unsweet tea")]
        [Required]

        public List<DrinkOption> DrinkOptions {get; set;}//navagation property
        
       
    }
}