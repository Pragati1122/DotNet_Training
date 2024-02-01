using flightapi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// LOGIN THROUGH API

namespace flightapi.Controllers{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : Controller
    {
        private readonly Ace52024Context _db;

        public LoginController(Ace52024Context db)
        {
            _db=db;
        }

        [HttpPost]
        public async Task<ActionResult<PragatiFlightUser>> Login(PragatiFlightUser u)
        {
            var user = (from i in _db.PragatiFlightUsers
                         where i.Email == u.Email && i.Password==u.Password
                         select i).SingleOrDefault();
                         if(user!=null)
                         return user;
                         else
                         return NotFound();
        }
    }
}