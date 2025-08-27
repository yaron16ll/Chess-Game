using System.Globalization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Server.Data;

namespace Server.Models
{
    public class EditModel : PageModel
    {
        private readonly Server.Data.ServerContext _context;

        public EditModel(Server.Data.ServerContext context)
        {
            _context = context;
        }

        [BindProperty]
        public PlayersTable PlayersTable { get; set; } = default!;

        public List<string> Countries { get; set; } = new List<string>();

        // OnGetAsync to load player data and country list
        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var playerstable = await _context.PlayersTable.FirstOrDefaultAsync(m => m.ID == id);
            if (playerstable == null)
            {
                return NotFound();
            }

            PlayersTable = playerstable;

            LoadCountries(); // Load country options for the dropdown
            return Page();
        }

        // OnPostAsync to update player data
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                LoadCountries(); // Load countries again if the model state is invalid
                return Page();
            }

            _context.Attach(PlayersTable).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PlayersTableExists(PlayersTable.ID))
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

        // LoadCountries to populate dropdown list
        private void LoadCountries()
        {
            Countries = CultureInfo.GetCultures(CultureTypes.SpecificCultures)
                .Select(c => new RegionInfo(c.Name).EnglishName)
                .Distinct()
                .OrderBy(name => name)
                .ToList();
        }

        // Helper method to check if the player exists in the database
        private bool PlayersTableExists(int id)
        {
            return _context.PlayersTable.Any(e => e.ID == id);
        }
    }
}
