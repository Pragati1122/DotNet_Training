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
    public class PassengerController : ControllerBase
    {
        private readonly Ace52024Context _context;

        public PassengerController(Ace52024Context context)
        {
            _context = context;
        }

        // GET: api/Passenger
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PragatiPassenger>>> GetPragatiPassengers()
        {
            return await _context.PragatiPassengers.ToListAsync();
        }

        // GET: api/Passenger/5
        [HttpGet("{id}")]
        public async Task<ActionResult<PragatiPassenger>> GetPragatiPassenger(int id)
        {
            var pragatiPassenger = await _context.PragatiPassengers.FindAsync(id);

            if (pragatiPassenger == null)
            {
                return NotFound();
            }

            return pragatiPassenger;
        }

        // PUT: api/Passenger/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPragatiPassenger(int id, PragatiPassenger pragatiPassenger)
        {
            if (id != pragatiPassenger.Passengerid)
            {
                return BadRequest();
            }

            _context.Entry(pragatiPassenger).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PragatiPassengerExists(id))
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

        // POST: api/Passenger
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<PragatiPassenger>> PostPragatiPassenger(PragatiPassenger pragatiPassenger)
        {
            _context.PragatiPassengers.Add(pragatiPassenger);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPragatiPassenger", new { id = pragatiPassenger.Passengerid }, pragatiPassenger);
        }

        // DELETE: api/Passenger/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePragatiPassenger(int id)
        {
            var pragatiPassenger = await _context.PragatiPassengers.FindAsync(id);
            if (pragatiPassenger == null)
            {
                return NotFound();
            }

            _context.PragatiPassengers.Remove(pragatiPassenger);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PragatiPassengerExists(int id)
        {
            return _context.PragatiPassengers.Any(e => e.Passengerid == id);
        }
    }
}
