using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Numerics;
using Server.Models;

namespace Server.Pages.Players
{
    public class IndexModel : PageModel
    {
        private readonly Server.Data.ServerContext _context;

        public IndexModel(Server.Data.ServerContext context)
        {
            _context = context;
        }

        [BindProperty(SupportsGet = true)]
        public string SelectedPlayerName { get; set; } = default!;


        public IEnumerable<string> PlayerNames { get; set; } = default!;

        // Store query results as a list of dynamic objects
        public List<dynamic> QueryResults { get; set; } = default!;

        public bool isDeletePlayer = false;

        public bool isDeleteGame = false;

        public List<(string Handler, string Label)> actions =
    [
        ("AllPlayers", "Show All Players"),
        ("AllPlayersByName", "All Players By Name"),
        ("AllPlayersByNameAndDate", "All Players By Name and Date"),
        ("AllGames", "All Games"),
        ("FirstPlayerEachCountry", "All First Players of Each Country"),
        ("EachPlayerWithGameCount", "All Players with Game Count"),
        ("AllPlayerByGameCount", "All Players By Game Count"),
        ("AllPlayerByCountry", "All Players By Country")
    ];




        public async Task OnGetAsync()
        {

            await getPlayersNames();


        }

        private async Task getPlayersNames()
        {
            PlayerNames = await _context.PlayersTable
              .Where(p => !string.IsNullOrEmpty(p.FirstName)) // Filter out null or empty names
              .GroupBy(p => (p.FirstName ?? "").ToLower()) // Group by lowercase version to treat different cases as the same
              .Select(g => g.First().FirstName!) // Select the original name while ensuring it's non-null with null-forgiving operator
              .OrderBy(name => name) // Sort names alphabetically
              .ToListAsync();

        }

        public async Task OnPostAllPlayersAsync()
        {
            if (_context.PlayersTable != null)
            {
                // Fetch all players and store them as dynamic objects
                var result = await _context.PlayersTable
                    .Select(p => new
                    {
                        p.FirstName,
                        LastName = p.LastName ?? "Not Exist", // Provide "Not Exist" if LastName is null
                        p.PhoneNumber,
                        p.Country,
                        p.ID
                    })
                    .ToListAsync();

                QueryResults = result.Cast<dynamic>().ToList(); // Cast to dynamic
                isDeletePlayer = true;
                isDeleteGame = false;
            }
            await getPlayersNames();
        }

        public async Task OnPostAllPlayersByNameAsync()
        {
            if (_context.PlayersTable != null)
            {
                // Fetch and order players by name
                var result = await _context.PlayersTable
                .OrderBy(p => p.FirstName)
                .Select(p => new
                {
                    p.FirstName,
                    LastName = p.LastName ?? "Not Exist", // Provide "Not Exist" if LastName is null
                    p.PhoneNumber,
                    p.Country
                })
                .ToListAsync(); // Use ToListAsync without dynamic

                QueryResults = result.Cast<dynamic>().ToList(); // Cast to dynamic
                isDeletePlayer = false;
                isDeleteGame = false;
            }
            await getPlayersNames();

        }

        public async Task OnPostAllPlayersByNameAndDateAsync()
        {
            if (_context.PlayersTable != null && _context.GamesTable != null)
            {
                // Fetch the joined data from the database
                var rawResult = await _context.PlayersTable
                    .Join(
                        _context.GamesTable,
                        player => player.ID,
                        game => game.PlayerID,
                        (player, game) => new { player.FirstName, game.StartDate }
                    )
                    .ToListAsync();

                var result = rawResult
                    .GroupBy(p => p.FirstName, StringComparer.Ordinal) // Group by Player Name (case-sensitive)
                    .Select(g => new
                    {
                        PlayerName = g.Key,
                        LastGameDate = g.Max(x => x.StartDate)?.ToString("dd-MM-yyyy")

                    })
                    .OrderByDescending(p => p.PlayerName, StringComparer.Ordinal) // Case-sensitive sort
                    .ToList();

                QueryResults = result.Cast<dynamic>().ToList();

                isDeletePlayer = false;
                isDeleteGame = false;
            }
            await getPlayersNames();
        }

        public async Task OnPostAllGamesAsync()
        {
            if (_context.PlayersTable != null && _context.GamesTable != null)
            {
                var result = await _context.PlayersTable
                    .Join(
                        _context.GamesTable,
                        player => player.ID,
                        game => game.PlayerID,
                        (player, game) => new
                        {
                            GameID = game.ID,               // Explicitly include game.ID as GameID
                            GameLength = game.Length,
                            GameStartDate = game.StartDate,
                            PlayerFirstName = player.FirstName
                        }
                    )
                    .Select(item => new
                    {
                        ID = item.GameID,                 // Project GameID as ID
                        Length = item.GameLength,
                        StartDate = item.GameStartDate.HasValue
                            ? item.GameStartDate.Value.ToString("dd-MM-yyyy")
                            : "No Date Available",
                        FirstName = item.PlayerFirstName
                    })
                    .ToListAsync();

                QueryResults = result.Cast<dynamic>().ToList(); // Cast result to dynamic if required

                isDeletePlayer = false;
                isDeleteGame = true;
            }

            await getPlayersNames();

        }

        public async Task OnPostFirstPlayerEachCountryAsync()
        {
            if (_context.PlayersTable != null && _context.GamesTable != null)
            {
                // Fetch the grouped data from the database
                var groupedData = await _context.PlayersTable
                    .Join(
                        _context.GamesTable,
                        player => player.ID,
                        game => game.PlayerID,
                        (player, game) => new
                        {
                            player.Country,
                            player.FirstName,
                            LastName = player.LastName ?? "Not Exist", // Provide "Not Exist" if LastName is null
                            player.PhoneNumber,
                            game.StartDate
                        }
                    )
                    .GroupBy(p => p.Country) // Group by Country
                    .ToListAsync();

                var result = groupedData
                    .Select(g => g.OrderBy(p => p.StartDate).First()) // Order and pick first in-memory
                    .Select(p => new
                    {
                        p.FirstName,
                        LastName = p.LastName ?? "Not Exist", // Provide "Not Exist" if LastName is null
                        p.PhoneNumber,
                        p.Country
                    })
                    .ToList();

                QueryResults = result.Cast<dynamic>().ToList();

                isDeletePlayer = false;
                isDeleteGame = false;
            }
            await getPlayersNames();

        }

        public async Task OnPostEachPlayerWithGameCountAsync()
        {
            if (_context.PlayersTable != null && _context.GamesTable != null)
            {
                var result = await _context.GamesTable
                    .GroupBy(game => game.PlayerID) // Group by PlayerID
                    .Select(g => new
                    {
                        PlayerID = g.Key,
                        GameCount = g.Count(), // Count games for each player
                    })
                    .Join(
                        _context.PlayersTable,
                        gameGroup => gameGroup.PlayerID,
                        player => player.ID,
                        (gameGroup, player) => new
                        {
                            player.FirstName, // Retrieve the player's name
                            gameGroup.GameCount
                        }
                    )
                    .OrderByDescending(p => p.GameCount) // Optional: order by game count
                    .ToListAsync();

                QueryResults = result.Cast<dynamic>().ToList();

                isDeletePlayer = false;
                isDeleteGame = false;
            }
            await getPlayersNames();


        }

        public async Task OnPostAllPlayerByGameCountAsync()
        {
            if (_context.PlayersTable != null && _context.GamesTable != null)
            {
                var result = await
            _context.PlayersTable.GroupJoin(
            _context.GamesTable,
            player => player.ID,
            game => game.PlayerID,
            (player, games) => new
            {
                player.FirstName,
                player.LastName,
                player.PhoneNumber,
                player.Country,
                GameCount = games.Count() // Count of games for each player
            })
         .Select(g => new
         {
             g.FirstName,
             LastName = g.LastName ?? "Not Exist", // Provide "Not Exist" if LastName is null
             g.PhoneNumber,
             g.Country,
             g.GameCount

         })
        .OrderByDescending(g => g.GameCount) // Order by GameCount descending
             .ToListAsync();

                QueryResults = result.Cast<dynamic>().ToList();
                isDeletePlayer = false;
                isDeleteGame = false;

            }
            await getPlayersNames();

        }

        public async Task OnPostAllPlayerByCountryAsync()
        {
            if (_context.PlayersTable != null)
            {
                // Fetch and order players by country
                var result = await _context.PlayersTable
                    .OrderBy(p => p.Country)
                    .Select(p => new
                    {
                        p.FirstName,
                        LastName = p.LastName ?? "Not Exist", // Provide "Not Exist" if LastName is null
                        p.PhoneNumber,
                        p.Country
                    })
                    .ToListAsync();

                QueryResults = result.Cast<dynamic>().ToList(); // Cast to dynamic
                isDeletePlayer = false;
                isDeleteGame = false;
            }
            await getPlayersNames();
        }


        public async Task OnPostDeletePlayerAsync(int? id)
        {
            if (id != null && _context.PlayersTable != null && _context.GamesTable != null)
            {

                // Find all games related to the player by PlayerID
                var relatedGames = await _context.GamesTable
                    .Where(g => g.PlayerID == id)
                    .ToListAsync();

                // Remove all related games first
                if (relatedGames.Any())
                {
                    _context.GamesTable.RemoveRange(relatedGames);
                }

                // Find the player by ID
                var player = await _context.PlayersTable.FindAsync(id);

                if (player != null)
                {
                    // Remove the player
                    _context.PlayersTable.Remove(player);

                    // Save changes to the database
                    await _context.SaveChangesAsync();

                    // Optionally, refresh the player list
                    await OnPostAllPlayersAsync();
                }
            }
            await getPlayersNames();

        }
        public async Task OnPostDeleteGameAsync(int? id)
        {
            if (id != null && _context.GamesTable != null)
            {

                // Find the player by ID
                var games = await _context.GamesTable.FindAsync(id);

                if (games != null)
                {
                    // Remove the player
                    _context.GamesTable.Remove(games);

                    // Save changes to the database
                    await _context.SaveChangesAsync();

                    // Optionally, refresh the player list
                    await OnPostAllGamesAsync();
                }
            }
            await getPlayersNames();

        }

        public async Task OnPostPlayerSelectedAsync()
        {
            if (_context.PlayersTable != null && _context.GamesTable != null)
            {

                var selectedPlayer = await _context.PlayersTable
                    .Where(p => (p.FirstName ?? "").ToLower() == SelectedPlayerName.ToLower())
                    .FirstOrDefaultAsync();

                if (selectedPlayer != null)
                {
                    var games = await _context.GamesTable
                    .Where(game => game.PlayerID == selectedPlayer.ID)
                    .OrderBy(game => game.StartDate)
                    .Select(game => new
                    {
                        game.Length,
                        StartDate = game.StartDate.HasValue ? game.StartDate.Value.ToString("dd-MM-yyyy") : "No Date Available",
                        PlayerName = selectedPlayer.FirstName
                    })
                    .ToListAsync();

                    QueryResults = games.Cast<dynamic>().ToList();
                }

                await getPlayersNames();
            }
        }
    }
}