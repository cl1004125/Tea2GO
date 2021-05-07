using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using DrinkOptions.Models;
using Microsoft.Extensions.Logging;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace DrinkOptions.Pages.Drinks
{
    public class DetailsModel : PageModel
    {
        private readonly ILogger<DetailsModel> _logger;
        private readonly DrinkOptions.Models.Context _context;

        public DetailsModel(DrinkOptions.Models.Context context, ILogger<DetailsModel> logger)
        {
            _logger = logger;
            _context = context;
        }

        public Drink Drink { get; set; }

        [BindProperty]
        public int OptionIDToDelete {get; set;}

        [BindProperty]
        [Display(Name = "Option")]
        public int OptionIDToAdd {get; set;}
        public List<Option> AllOptions {get; set;}
        public SelectList OptionsDropDown {get; set;}

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            // Bring in related data with .Include and .ThenInclude
            Drink = await _context.Drink.Include(d => d.DrinkOptions).ThenInclude(dc => dc.Option).FirstOrDefaultAsync(m => m.OptionID == id);
            AllCourses = await _context.Course.ToListAsync();
            OptionsDropDown = new SelectList(AllOptions, "OptionID", "Description");

            if (Drink == null)
            {
                return NotFound();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostDeleteCourseAsync(int? id)
        {
            _logger.LogWarning($"OnPost: StudentId {id}, DROP course {OptionIDToDelete}");
            if (id == null)
            {
                return NotFound();
            }

            Drink = await _context.Drink.Include(s => d.DrinkOptions).ThenInclude(dc=> dc.Option).FirstOrDefaultAsync(d => d.DrinkID == id);
           
            
            if (Drink == null)
            {
                return NotFound();
            }

            DrinkOption OptionToDrop = _context.DrinkOption.Find(OptionIDToDelete, id.Value);

            if (optionToDrop != null)
            {
                _context.Remove(optionToDrop);
                _context.SaveChanges();
            }
            else
            {
                _logger.LogWarning("Drink not available");
            }

            return RedirectToPage(new {id = id});
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            _logger.LogWarning($"OnPost: DrinkID {id}, ADD option {OptionIDToAdd}");
            if (OptionIDToAdd == 0)
            {
                ModelState.AddModelError("OptionIDToAdd", "This field is a required field.");
                return Page();
            }
            if (id == null)
            {
                return NotFound();
            }

            Drink = await _context.Drink.Include(d => d.DrinkOption).ThenInclude(mc => mc.Option).FirstOrDefaultAsync(d => d.DrinkID == id);            
            AllOptions = await _context.Option.ToListAsync();
            OptionsDropDown = new SelectList(AllOptions, "OptionID", "Description");

            if (Drink == null)
            {
                return NotFound();
            }

            if (!_context.DrinkOption.Any(dc => dc.OptionID == OptionIdToAdd && sc.DrinkID == id.Value))
            {
                DrinkOptions optionToAdd = new DrinkOption { DrinkID = id.Value, OptionID = OptionIdToAdd};
                _context.Add(courseToAdd);
                _context.SaveChanges();
            }
            else
            {
                _logger.LogWarning("Drink is not available");
            }

            // Best practice is that OnPost should redirect. This clears the form data.
            // FIXME: Can we just populate the routeValues from what is already there?
            return RedirectToPage(new {id = id});
        }
    }
}