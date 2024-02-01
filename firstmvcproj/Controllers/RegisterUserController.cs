using firstmvcproj.Models;
using Microsoft.AspNetCore.Mvc;

namespace firstmvcproj.Controllers{
    public class RegisterUserController : Controller{
        private readonly Ace52024Context db;          // private readonly also works
        //Constructor Injection
        public RegisterUserController(Ace52024Context _db){
            db=_db;
        }

        public IActionResult RegisterUser(){
            return View();
        }

        [HttpPost]
        public IActionResult RegisterUser(PragatiUsertbl u){
            db.PragatiUsertbls.Add(u);
            db.SaveChanges();
            // if(db.PragatiUsertbls.Find(u.Email)!=null){

            // }
            return RedirectToAction("Login","Login");                // after clicking on the register button it returns to login page
        }
    }
}