using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using flightapi.Models;
using flightapi.Service;
// using firstapi.Service;

namespace flightapi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserServ<PragatiFlightUser> _userserv;

        public UsersController(IUserServ<PragatiFlightUser> userserv)
        {
            _userserv = userserv;
        }

        // GET: api/Users
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PragatiFlightUser>>> GetPragatiFlightUsers()
        {
            return await _userserv.GetAllUsers();
        }

        // GET: api/Users/5
        [HttpGet("{id}")]
        public async Task<ActionResult<PragatiFlightUser>> GetPragatiFlightUser(string id)
        {
            var pragatiFlightUser = await _userserv.GetUserByUsername(id);

            if (pragatiFlightUser == null)
            {
                return NotFound();
            }

            return pragatiFlightUser;
        }

        // PUT: api/Users/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPragatiFlightUser(string id, PragatiFlightUser pragatiFlightUser)
        {
            if (id != pragatiFlightUser.Username)
            {
                return BadRequest();
            }

            // _context.Entry(pragatiFlightUser).State = EntityState.Modified;
            try{
                _userserv.UpdateUser(id, pragatiFlightUser);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PragatiFlightUserExists(id))
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

        // POST: api/Users
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<PragatiFlightUser>> PostPragatiFlightUser(PragatiFlightUser pragatiFlightUser)
        {
            try
            {
                _userserv.AddUser(pragatiFlightUser);
            }
            catch (DbUpdateException)
            {
                if (PragatiFlightUserExists(pragatiFlightUser.Username))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetPragatiFlightUser", new { id = pragatiFlightUser.Username }, pragatiFlightUser);
        }

        // DELETE: api/Users/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePragatiFlightUser(string id)
        {
            var pragatiFlightUser = _userserv.GetUserByUsername(id);
            if (pragatiFlightUser == null)
            {
                return NotFound();
            }

            _userserv.DeleteUser(id);

            return NoContent();
        }

        private bool PragatiFlightUserExists(string id)
        {
            PragatiFlightUser u = _userserv.GetUserByUsername(id).Result;
            if(u!=null)
            return true;
            else return false;
        }
















        // private readonly Ace52024Context _context;

        // public UsersController(Ace52024Context context)
        // {
        //     _context = context;
        // }

        // // GET: api/Users
        // [HttpGet]
        // public async Task<ActionResult<IEnumerable<PragatiFlightUser>>> GetPragatiFlightUsers()
        // {
        //     return await _context.PragatiFlightUsers.ToListAsync();
        // }

        // // GET: api/Users/5
        // [HttpGet("{id}")]
        // public async Task<ActionResult<PragatiFlightUser>> GetPragatiFlightUser(string id)
        // {
        //     var pragatiFlightUser = await _context.PragatiFlightUsers.FindAsync(id);

        //     if (pragatiFlightUser == null)
        //     {
        //         return NotFound();
        //     }

        //     return pragatiFlightUser;
        // }

        // // PUT: api/Users/5
        // // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        // [HttpPut("{id}")]
        // public async Task<IActionResult> PutPragatiFlightUser(string id, PragatiFlightUser pragatiFlightUser)
        // {
        //     if (id != pragatiFlightUser.Username)
        //     {
        //         return BadRequest();
        //     }

        //     _context.Entry(pragatiFlightUser).State = EntityState.Modified;

        //     try
        //     {
        //         await _context.SaveChangesAsync();
        //     }
        //     catch (DbUpdateConcurrencyException)
        //     {
        //         if (!PragatiFlightUserExists(id))
        //         {
        //             return NotFound();
        //         }
        //         else
        //         {
        //             throw;
        //         }
        //     }

        //     return NoContent();
        // }

        // // POST: api/Users
        // // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        // [HttpPost]
        // public async Task<ActionResult<PragatiFlightUser>> PostPragatiFlightUser(PragatiFlightUser pragatiFlightUser)
        // {
        //     _context.PragatiFlightUsers.Add(pragatiFlightUser);
        //     try
        //     {
        //         await _context.SaveChangesAsync();
        //     }
        //     catch (DbUpdateException)
        //     {
        //         if (PragatiFlightUserExists(pragatiFlightUser.Username))
        //         {
        //             return Conflict();
        //         }
        //         else
        //         {
        //             throw;
        //         }
        //     }

        //     return CreatedAtAction("GetPragatiFlightUser", new { id = pragatiFlightUser.Username }, pragatiFlightUser);
        // }

        // // DELETE: api/Users/5
        // [HttpDelete("{id}")]
        // public async Task<IActionResult> DeletePragatiFlightUser(string id)
        // {
        //     var pragatiFlightUser = await _context.PragatiFlightUsers.FindAsync(id);
        //     if (pragatiFlightUser == null)
        //     {
        //         return NotFound();
        //     }

        //     _context.PragatiFlightUsers.Remove(pragatiFlightUser);
        //     await _context.SaveChangesAsync();

        //     return NoContent();
        // }

        // private bool PragatiFlightUserExists(string id)
        // {
        //     return _context.PragatiFlightUsers.Any(e => e.Username == id);
        // }
    }
}
