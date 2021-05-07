using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DrinkOptions.Models;
using Microsoft.Extensions.Logging;

namespace DrinkOptions.Pages.Drinks
{
    public class EditModel : PageModel
    {
        private readonly DrinkOptions.Models.Context _context;
        private readonly ILogger _logger;

        public EditModel(DrinkOptions.Models.Context context, ILogger<EditModel> logger)
        {
            _context = context;
            _logger = logger;
        }

        [BindProperty]
        public Drink Drink { get; set; } 
        public List<Option> Options {get; set;} //list of your options

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            // Bring in related data using Include and ThenInclude
            Drink = await _context.Student.Include(d => d.DrinkOptions).ThenInclude(dc => dc.Option).FirstOrDefaultAsync(d => d.DrinkID == id);
            
            Options = _context.Option.ToList();

            if (Drink == null)
            {
                return NotFound();
            }
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync(int[] selectedOption)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            //_context.Attach(Student).State = EntityState.Modified;
            
            var drinkToUpdate = await _context.Drink.Include(d => d.DrinkOptions).ThenInclude(dc => dc.Option).FirstOrDefaultAsync(d => d.DrinkID == Drink.DrinkID);
            DrinkToUpdate.STea = Drink.STea;
            DrinkToUpdate.UTea = Drink.UTea;

            // Separate method to update the courses because it can get complex
            UpdateDrinkOptions(selectedOption, drinkToUpdate);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DrinkExists(Drink.DrinkID))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool DrinkExists(int id)
        {
            return _context.Drink.Any(e => e.DrinkID == id);
        }

        private void UpdateDrinkOptions(int[] selectedDrinks, Drink drinkToUpdate)
        {
            if (selectedOptions == null)
            {
                drinkToUpdate.DrinkOptions = new List<DrinkOptions>();
                return;
            }

            List<int> currentOptions = drinkToUpdate.DrinkOptions.Select(d => d.DrinkID).ToList();
            List<int> selectedOptionList = selectedOptions.ToList();

            foreach (var course in _context.Option)
            {
                if (selectedOptionList.Contains(course.OptionID))
                {
                    if (!currentOptions.Contains(course.OptionID))
                    {
                        // Add your option here
                        DrinkToUpdate.DrinkOptions.Add(
                            new DrinkOptions { DrinkID = drinkToUpdate.DrinkID, OptionID = option.OptionID }
                        );
                        _logger.LogWarning($"Drink {DrinkToUpdate.STea} {DrinkToUpdate.UTea} ({DrinkToUpdate.DrinkID}) - ADD {option.OptionID} {option.Description}");
                    }
                }
                else
                {
                    if (currentOptions.Contains(option.OptionID))
                    {
                        // Remove option here
                        DrinkOptions optionToRemove = drinkToUpdate.DrinkOption.SingleOrDefault(d => d.OptionID == option.OptionID);
                        _context.Remove(optionToRemove);
                        _logger.LogWarning($"Drink {drinkToUpdate.STea} {drinkToUpdate.UTea} ({drinkToUpdate.DrinkID}) - DROP {option.OptionID} {option.Description}");
                    }
                }
            }
        }
    }
}