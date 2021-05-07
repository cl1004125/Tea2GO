using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using DrinkOptions.Models;

namespace DrinkOptions.Pages.Drinks
{
    public class IndexModel : PageModel
    {
        private readonly DrinkOptions.Models.Context _context;

        public IndexModel(DrinkOptions.Models.Context context)
        {
            _context = context;
        }

        public IList<Drink> Drink { get;set; }

        public async Task OnGetAsync()
        {
            // Bring in related data. This is Many-to-Many so Include=>StudentCourses ThenInclude=>Course
            Drink = await _context.Drink.Include(d => d.DrinkOptions).ThenInclude(dc => dc.Options).ToListAsync();
        }
    }
}