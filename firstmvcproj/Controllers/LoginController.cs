using firstmvcproj.Models;
using Microsoft.AspNetCore.Mvc;

namespace firstmvcproj.Controllers{
    public class LoginController : Controller{
        private readonly Ace52024Context db;          // private readonly also works
        //Constructor Injection
        public LoginController(Ace52024Context _db){
            db=_db;
        }

        public IActionResult Login(){
            return View();
        }

        [HttpPost]
        public IActionResult Login(PragatiUsertbl u){
            var result = (from i in db.PragatiUsertbls
                             where i.Email==u.Email && i.Password==u.Password
                             select i).SingleOrDefault();
                    if(result!=null){
                        HttpContext.Session.SetString("uname",result.Username);        //setting the string for session
                        return RedirectToAction("GetAllEmployeesFromDB","Employee");           //takes 2 args --> action method & the controller under which it is
                    }
                    else{
                        return  View();
                    }
        }

        public IActionResult Logout(){
            HttpContext.Session.Clear();          // clears all the set setring for this session
            return RedirectToAction("Login");
        }
    }
}