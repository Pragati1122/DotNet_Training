using System.Data.Common;
using flightproj.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace flightproj.Controllers{
    public class AdminController : Controller{
        private readonly Ace52024Context db;

        // Dependency injection
        public AdminController(Ace52024Context _db)
        {
            db =_db;
        }

        [HttpGet]
        public ActionResult ShowAllFlights(){
            var uname=HttpContext.Session.GetString("uname");
            if(uname!=null){
                return View(db.PragatiFlights);
            }
            else{
                return RedirectToAction("Login","Login");
            } 
        }


        [HttpGet]
        public ActionResult AddNewFlight(){
            var uname=HttpContext.Session.GetString("uname");
            if(uname!=null){
                return View();
            }
            else{
                return RedirectToAction("Login","Login");
            } 
        }
        [HttpPost]
        public ActionResult AddNewFlight(PragatiFlight f){
            db.PragatiFlights.Add(f);
            db.SaveChanges();
            return RedirectToAction("ShowAllFlights");
        }

        [HttpGet]
        public ActionResult Edit(int id){
            var uname=HttpContext.Session.GetString("uname");
            if(uname!=null){
                PragatiFlight f = db.PragatiFlights.Where(x=>x.Flightid==id).SingleOrDefault();
                return View(f);
            }
            else{
                return RedirectToAction("Login","Login");
            } 
        }

        [HttpPost]
        public ActionResult Edit(PragatiFlight f){
            db.PragatiFlights.Update(f);
            db.SaveChanges();
            return RedirectToAction("ShowAllFlights");
        }

        [HttpGet]
        public ActionResult Details(int id){
            var uname=HttpContext.Session.GetString("uname");
            if(uname!=null){
                PragatiFlight f = db.PragatiFlights.Where(x=>x.Flightid==id).SingleOrDefault();
                return View(f);
            }
            else{
                return RedirectToAction("Login","Login");
            } 
        }

        [HttpGet]
        public ActionResult Delete(int id){
            var uname=HttpContext.Session.GetString("uname");
            if(uname!=null){
                PragatiFlight f = db.PragatiFlights.Where(x=>x.Flightid==id).SingleOrDefault();
                return View(f);
            }
            else{
                return RedirectToAction("Login","Login");
            } 
        }

        [HttpPost]
        [ActionName("Delete")]

        public ActionResult DeleteConfirmed(int id){
            PragatiFlight e = db.PragatiFlights.Where(x=>x.Flightid==id).SingleOrDefault();
            db.PragatiFlights.Remove(e);
            db.SaveChanges();
            return RedirectToAction("ShowAllFlights");
        }

        public ActionResult ThisFlightBookingDetails(int flightid){
            var uname=HttpContext.Session.GetString("uname");
            if(uname!=null){
                ViewBag.flightid=flightid;
                var temp = db.PragatiFlights.Where(x=>x.Flightid == flightid).Include(x=>x.PragatiBookings).SingleOrDefault();//acc to the situation this should contain only one record of pragatiFlight
                List<PragatiBooking> relatedBookings = new List<PragatiBooking>();
                foreach(var item in temp.PragatiBookings){
                    relatedBookings.Add(item);
                }
            return View(relatedBookings); 
            }
            else{
                return RedirectToAction("Login","Login");
            } 
        }

        public ActionResult GetPassengersOnThisFlight(int flightid){
            var uname=HttpContext.Session.GetString("uname");
            if(uname!=null){
                ViewBag.flightid=flightid;
                var temp = db.PragatiFlights.Where(x=>x.Flightid == flightid).Include(x=>x.PragatiPassengers).SingleOrDefault();//acc to the situation this should contain only one record of pragatiSBAccount
                List<PragatiPassenger> relatedPassengers = new List<PragatiPassenger>();
                foreach(var item in temp.PragatiPassengers){
                    relatedPassengers.Add(item);
                }
            return View(relatedPassengers); 
            }
            else{
                return RedirectToAction("Login","Login");
            } 
        }

        [HttpGet]
        public ActionResult ShowAllBookings(){
            var uname=HttpContext.Session.GetString("uname");
            if(uname!=null){
                return View(db.PragatiBookings);
            }
            else{
                return RedirectToAction("Login","Login");
            } 
        }

    }
}