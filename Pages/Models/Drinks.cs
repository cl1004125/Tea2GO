using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;

namespace Tea2GO.Models
{
    public class Drink
    {
        public int DrinkID{get; set;}
        [Display(Drink = "Tea")]
        [Required]
        

        public List<DrinkOption> DrinkOptions {get; set;}//navagation property
        
       
    }
}