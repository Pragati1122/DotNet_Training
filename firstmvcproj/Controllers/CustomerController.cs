using firstmvcproj.Controllers;
using firstmvcproj.Models;
using Microsoft.AspNetCore.Mvc;

namespace firstmvcproj.Controllers{
    public class CustomerController : Controller{

        private readonly Ace52024Context db;

        public CustomerController(Ace52024Context _db){
            db = _db;
        }

        public ActionResult ShowCustomers(){
            return View(db.PragatiCustomers);
        }
        
        public ActionResult AddCustomer(){
            return View();
        }

        [HttpPost]
        public ActionResult AddCustomer(PragatiCustomer c){
            db.PragatiCustomers.Add(c);
            db.SaveChanges();
            return RedirectToAction("ShowCustomers");
        }

    }
}