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
    public class DeleteModel : PageModel
    {
        private readonly DrinkOptions.Models.Context _context;

        public DeleteModel(DrinkOptions.Models.Context context)
        {
            _context = context;
        }

        [BindProperty]
        public Drink Drink { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Drink = await _context.Student.FirstOrDefaultAsync(m => m.DrinkID == id);

            if (Drink == null)
            {
                return NotFound();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Drink = await _context.Drink.FindAsync(id);

            if (Drink != null)
            {
                _context.Drink.Remove(Drink);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}