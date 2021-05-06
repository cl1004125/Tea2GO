using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;
using System.Collections.Generic;

namespace Tea2GO.Models
{
    public class Option
    {
        public int Option{get; set;} //pk
        [Required]
        public string Size{get; set;}
        public List<DrinkOption> DrinkOptions{get; set;}//navagation property
    }

    public class DrinkOption
    {
        public int DrinkID{get; set;}
        public int OptionID{get; set;}
        public Drink Drink{get; set;}
        public Option Option{get; set;}
    }
}