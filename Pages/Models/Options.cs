using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;
using System.Collections.Generic;

namespace Tea2GO.Models
{
    public class Option
    {
        public int OptionID{get; set;} //pk
        [Required]
        public string Size{get; set;}
        public List<DrinkOption> DrinkOptions{get; set;}//navagation property
    }

    public class DrinkOption
    {
        public int DrinkID{get; set;} //composite primary key, FK
        public int OptionID{get; set;} //composite primary key, FK
        public Drink Drink{get; set;} //composite primary key, FK
        public Option Option{get; set;} //composite primary key, FK
    }
}