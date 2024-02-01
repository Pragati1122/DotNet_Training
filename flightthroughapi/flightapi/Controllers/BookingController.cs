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
    public class BookingController : ControllerBase
    {
        private readonly Ace52024Context _context;

        public BookingController(Ace52024Context context)
        {
            _context = context;
        }

        // GET: api/Booking
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PragatiBooking>>> GetPragatiBookings()
        {
            return await _context.PragatiBookings.ToListAsync();
        }

        // GET: api/Booking/5
        [HttpGet("{id}")]
        public async Task<ActionResult<PragatiBooking>> GetPragatiBooking(int id)
        {
            var pragatiBooking = await _context.PragatiBookings.FindAsync(id);

            if (pragatiBooking == null)
            {
                return NotFound();
            }

            return pragatiBooking;
        }

        // PUT: api/Booking/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPragatiBooking(int id, PragatiBooking pragatiBooking)
        {
            if (id != pragatiBooking.Bookingid)
            {
                return BadRequest();
            }

            _context.Entry(pragatiBooking).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PragatiBookingExists(id))
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

        // POST: api/Booking
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<PragatiBooking>> PostPragatiBooking(PragatiBooking pragatiBooking)
        {
            _context.PragatiBookings.Add(pragatiBooking);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPragatiBooking", new { id = pragatiBooking.Bookingid }, pragatiBooking);
        }

        // DELETE: api/Booking/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePragatiBooking(int id)
        {
            var pragatiBooking = await _context.PragatiBookings.FindAsync(id);
            if (pragatiBooking == null)
            {
                return NotFound();
            }

            _context.PragatiBookings.Remove(pragatiBooking);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PragatiBookingExists(int id)
        {
            return _context.PragatiBookings.Any(e => e.Bookingid == id);
        }
    }
}
