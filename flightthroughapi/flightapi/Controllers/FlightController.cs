using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using flightapi.Models;

namespace flightapi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FlightController : ControllerBase
    {
        private readonly Ace52024Context _context;

        public FlightController(Ace52024Context context)
        {
            _context = context;
        }

        // GET: api/Flight
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PragatiFlight>>> GetPragatiFlights()
        {
            return await _context.PragatiFlights.ToListAsync();
        }

        // GET: api/Flight/5
        [HttpGet("{id}")]
        public async Task<ActionResult<PragatiFlight>> GetPragatiFlight(int id)
        {
            var pragatiFlight = await _context.PragatiFlights.FindAsync(id);

            if (pragatiFlight == null)
            {
                return NotFound();
            }

            return pragatiFlight;
        }

        // PUT: api/Flight/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPragatiFlight(int id, PragatiFlight pragatiFlight)
        {
            if (id != pragatiFlight.Flightid)
            {
                return BadRequest();
            }

            _context.Entry(pragatiFlight).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PragatiFlightExists(id))
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

        // POST: api/Flight
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<PragatiFlight>> PostPragatiFlight(PragatiFlight pragatiFlight)
        {
            _context.PragatiFlights.Add(pragatiFlight);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPragatiFlight", new { id = pragatiFlight.Flightid }, pragatiFlight);
        }

        // DELETE: api/Flight/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePragatiFlight(int id)
        {
            Console.WriteLine(id);
            var pragatiFlight = await _context.PragatiFlights.FindAsync(id);
            if (pragatiFlight == null)
            {
                return NotFound();
            }

            _context.PragatiFlights.Remove(pragatiFlight);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PragatiFlightExists(int id)
        {
            return _context.PragatiFlights.Any(e => e.Flightid == id);
        }
    }
}
