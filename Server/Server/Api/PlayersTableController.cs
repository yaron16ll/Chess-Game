using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Server.Data;
using Server.Models;

namespace Server.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlayersTableController : ControllerBase
    {
        private readonly ServerContext _context;
        private static readonly Random _random = new Random();

        public PlayersTableController(ServerContext context)
        {
            _context = context;
        }


        // GET: api/PlayersTable
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PlayersTable>>> GetPlayersTable()
        {
            try
            {
                return await _context.PlayersTable.ToListAsync();
            }
            catch (Exception)
            {
                // Log the exception
                return StatusCode(StatusCodes.Status500InternalServerError, "Error retrieving data from the database.");
            }
        }

        // GET: api/PlayersTable/5
        [HttpGet("{id}")]
        public async Task<ActionResult<PlayersTable>> GetPlayersTable(int id)
        {
            try
            {
                var player = await _context.PlayersTable.FindAsync(id);

                if (player == null)
                {
                    return NotFound();
                }

                return player;
            }
            catch (Exception)
            {
                // Log the exception
                return StatusCode(StatusCodes.Status500InternalServerError, "Error retrieving data from the database.");
            }
        }
        [HttpPost("random-move")]
        public ActionResult<int> PostRandomMove([FromBody] int moveRange)
        {
            try
            {
                if (moveRange <= 0)
                {
                    return BadRequest("Move range must be greater than 0.");
                }

                int randomMove = _random.Next(moveRange); 
                return Ok(randomMove);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error: {ex.Message}");
            }
        }

        // POST: api/PlayersTable
        [HttpPost]
        public async Task<ActionResult<PlayersTable>> PostPlayersTable(PlayersTable playersTable)
        {
            try
            {
                _context.PlayersTable.Add(playersTable);
                await _context.SaveChangesAsync();

                return CreatedAtAction("GetPlayersTable", new { id = playersTable.ID }, playersTable);
            }
            catch (Exception)
            {
                // Log the exception
                return StatusCode(StatusCodes.Status500InternalServerError, "Error creating new player record.");
            }
        }

        // DELETE: api/PlayersTable/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePlayersTable(int id)
        {
            try
            {
                var playersTable = await _context.PlayersTable.FindAsync(id);
                if (playersTable == null)
                {
                    return NotFound();
                }

                _context.PlayersTable.Remove(playersTable);
                await _context.SaveChangesAsync();

                return NoContent();
            }
            catch (Exception)
            {
                // Log the exception
                return StatusCode(StatusCodes.Status500InternalServerError, "Error deleting data.");
            }
        }
    }
}
