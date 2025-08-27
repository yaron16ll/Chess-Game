
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Server.Data;
using Server.Models;

namespace Server.api
{
    [Route("api/[controller]")]
    [ApiController]
    public class GamesTableController : ControllerBase
    {
        private readonly ServerContext _context;

        public GamesTableController(ServerContext context)
        {
            _context = context;
        }

        // GET: api/GamesTables
        [HttpGet]
        public async Task<ActionResult<IEnumerable<GamesTable>>> GetGamesTable()
        {
            return await _context.GamesTable.ToListAsync();
        }

        // GET: api/GamesTables/LastId
        [HttpGet("LastId")]
        public async Task<ActionResult<int>> GetLastGameId()
        {
            var lastGame = await _context.GamesTable.OrderByDescending(g => g.ID).FirstOrDefaultAsync();

            if (lastGame == null)
            {
                return NotFound("No games found.");
            }

            return lastGame.ID;
        }


        // GET: api/GamesTables/5
        [HttpGet("{id}")]
        public async Task<ActionResult<GamesTable>> GetGamesTable(int id)
        {
            var gamesTable = await _context.GamesTable.FindAsync(id);

            if (gamesTable == null)
            {
                return NotFound();
            }

            return gamesTable;
        }

        // PUT: api/GamesTables/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutGamesTable(int id, GamesTable gamesTable)
        {
            if (id != gamesTable.ID)
            {
                return BadRequest();
            }

            _context.Entry(gamesTable).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!GamesTableExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/GamesTables
        [HttpPost]
        public async Task<ActionResult<GamesTable>> PostGamesTable(GamesTable gamesTable)
        {
      
         
                // Add the entity to the context
                _context.GamesTable.Add(gamesTable);
                await _context.SaveChangesAsync();

                // Return the created entity with a 201 Created response
                return CreatedAtAction("GetGamesTable", new { id = gamesTable.ID }, gamesTable);
           
         
        }

        // DELETE: api/GamesTables/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteGamesTable(int id)
        {
            var gamesTable = await _context.GamesTable.FindAsync(id);
            if (gamesTable == null)
            {
                return NotFound();
            }

            _context.GamesTable.Remove(gamesTable);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool GamesTableExists(int id)
        {
            return _context.GamesTable.Any(e => e.ID == id);
        }
    }
}
