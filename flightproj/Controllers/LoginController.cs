using flightproj.Models;
using Microsoft.AspNetCore.Mvc;

namespace flightproj.Controllers{
    public class LoginController : Controller{

        private readonly ISession session;
        private readonly Ace52024Context db;          // private readonly also works
        //Constructor Injection
        public LoginController(Ace52024Context _db, IHttpContextAccessor httpContextAccessor){
            db=_db;
            session = httpContextAccessor.HttpContext.Session;
        }

        public IActionResult Login(){
            return View();
        }

        [HttpPost]
        public IActionResult Login(PragatiFlightUser u){
            var result = (from i in db.PragatiFlightUsers
                             where i.Email==u.Email && i.Password==u.Password
                             select i).SingleOrDefault();
                    if(result!=null){
                        HttpContext.Session.SetString("uname",result.Username);        //setting the string for session
                        if(u.Email.Contains("@fareportal.com")){
                            return RedirectToAction("ShowAllFlights","Admin");
                        }
                        else
                        return RedirectToAction("GetFlightRequirements","Flight");           //takes 2 args --> action method & the controller under which it is
                    }
                    else{
                        return  View();
                    }
        }

        public IActionResult Logout(){
            HttpContext.Session.Clear();          // clears all the set string for this session
            return RedirectToAction("Login");
        }

        public IActionResult RegisterUser(){
            return View();
        }

        [HttpPost]
        public IActionResult RegisterUser(PragatiFlightUser u){
            var temp=db.PragatiFlightUsers.Where(x=>x.Email==u.Email || x.Username==u.Username).SingleOrDefault();
            if(temp==null){
                db.PragatiFlightUsers.Add(u);
                db.SaveChanges();
                return RedirectToAction("Login","Login");                // after clicking on the register button it returns to login page
            }
            else{
                return RedirectToAction("AccountExists");
            }
            
        }

        public ActionResult AccountExists(){
            return View();
        }
    }
}