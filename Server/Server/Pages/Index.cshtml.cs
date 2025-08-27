using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Globalization;
using Server.Models;

namespace Server.Pages
{
    public class IndexModel : PageModel
    {
        private readonly Server.Data.ServerContext _context;

        public IndexModel(Server.Data.ServerContext context)
        {
            _context = context;
        }


        [BindProperty]
        public PlayersTable PlayersTable { get; set; } = default!;

        public List<string> Countries { get; set; } = new List<string>();

        public  void OnGetAsync()
        {
            // Load all country names
            Countries =  CultureInfo.GetCultures(CultureTypes.SpecificCultures)
                .Select(c => new RegionInfo(c.Name).EnglishName)
                .Distinct()
                .OrderBy(name => name)
                .ToList();
        }

        public async Task<IActionResult> OnPostCreatePlayerAsync()
        { 
            try
            {
                // Check if the player ID already exists to avoid duplicate IDs
                var existingPlayer = await _context.PlayersTable.FindAsync(PlayersTable.ID);

                if (existingPlayer != null) {
                    throw new Exception("Player already exists. Please choose a different ID.");

                }

                // Create a new player record
                var newPlayer = new PlayersTable
                {
                    ID = PlayersTable.ID,
                    FirstName = PlayersTable.FirstName,
                    LastName = PlayersTable.LastName,
                    Password = PlayersTable.Password,
                    Country = PlayersTable.Country,
                    PhoneNumber = PlayersTable.PhoneNumber
                };

                // Add and save the new player record
                 _context.PlayersTable.Add(newPlayer);
                await _context.SaveChangesAsync();

                TempData["SuccessMessage"] = $"Player {PlayersTable.FirstName} has been registered successfully!";
                return RedirectToPage(); // Redirect to clear the form and show the modal
            }
            catch (Exception ex)
            {
                // Log the exception 
                TempData["ErrorMessage"] = $"{ex.Message}";
                OnGetAsync();
                return Page(); // Stay on the same page to show the error
            }
        }
    }
}
