using System.Data.Common;
using flightproj.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;

namespace flightproj.Controllers{
    public class FlightController : Controller{
        private readonly Ace52024Context db;

        // Dependency injection
        public FlightController(Ace52024Context _db)
        {
            db =_db;
        }

        [HttpGet]
        public ActionResult GetFlightRequirements(){
            ViewBag.FlightSources=new SelectList(db.PragatiFlights,"Source","Source");
            ViewBag.FlightDestinations=new SelectList(db.PragatiFlights,"Destination","Destination");
            return View();
        }
        [HttpPost]
        public ActionResult GetFlightRequirements(PragatiFlight f){
            TempData["source"] = f.Source;                                //TempData is used to pass data from action to another action
            TempData["destination"] = f.Destination;
            TempData["date"] = f.Date;
            return RedirectToAction("ShowMatchingFlights");
        }

        [HttpGet]
        public ActionResult ShowMatchingFlights(){
            string src = TempData["source"].ToString();         //since this method is dependent on these values from another method sp independently refreshing this page gives error due to null value in these variables
            string dest = TempData["destination"].ToString();
            // DateOnly dateT = DateOnly.FromDateTime(Convert.ToDateTime(TempData["date"].ToString()));
            // DateOnly dateT = TempData["date"].ToString();
            // System.Console.WriteLine("date is "+date);

            var result = (from i in db.PragatiFlights
                             where i.Source== src && i.Destination==dest
                             select i).ToList();
            if(result.Any()){
                return View(result);
            }
            else return RedirectToAction("NoFlightsAvailablePage");
        }

        public ActionResult NoFlightsAvailablePage(){
            return View();
        }

        public ActionResult DetailsOfCurFlight(int id){
            PragatiFlight f = db.PragatiFlights.Where(x=>x.Flightid==id).SingleOrDefault();
            return View(f);
        }

        [HttpGet]
        public ActionResult BookAFlight(int id){
            ViewBag.Username=HttpContext.Session.GetString("uname");
            TempData["flightid"] = id;
            PragatiFlight temp = db.PragatiFlights.Where(x=>x.Flightid==id).SingleOrDefault();
            var temp2 = db.PragatiPassengers.Include(x=>x.Flight);
            ViewBag.flightid=id;
            ViewBag.cost=temp.Price;
            ViewBag.flightName=temp.Flightname;
            if(temp.SeatsAvailable>=1){
                if(ViewBag.Username!=null){
                    return View();
                }
                else{
                    return RedirectToAction("Login","Login");
                }
            }
            else return RedirectToAction("SeatsFull");
            
        }

        [HttpPost]
        public ActionResult BookAFlight(PragatiPassenger p){
            int flightid = Convert.ToInt32(TempData["flightid"].ToString());
            PragatiFlight temp = db.PragatiFlights.Where(x=>x.Flightid==flightid).SingleOrDefault();
            p.Username=HttpContext.Session.GetString("uname");
            p.Flightid=flightid;
            db.PragatiPassengers.Add(p);
            temp.SeatsAvailable = temp.SeatsAvailable - 1;
            db.SaveChanges();
            PragatiBooking booking = new PragatiBooking();
            booking.Flightid = temp.Flightid;
            booking.Passengerid = p.Passengerid;
            booking.Bookingdate = DateTime.Now;
            booking.TotalCost = temp.Price;
            db.PragatiBookings.Add(booking);
            db.SaveChanges();
            return RedirectToAction("BookingSuccess");
        }

        public ActionResult BookingSuccess(){
            var uname=HttpContext.Session.GetString("uname");
            if(uname!=null){
                return View();
            }
            else{
                return RedirectToAction("Login","Login");
            } 
        }

        public ActionResult SeatsFull(){
            return View();
        }

        [HttpGet]

        public ActionResult YourBookings(){
            var uname=HttpContext.Session.GetString("uname");
            if(uname!=null){
                var result = db.PragatiBookings.Include(x=>x.Passenger).Where(p=>p.Passenger.Username==uname);      //IMPORTANT//
                if(result.Any()){
                    return View(result);
                }
                else
                return RedirectToAction("NoBookings");
                }
            else{
                return RedirectToAction("Login","Login");
            } 
            // var uname=HttpContext.Session.GetString("uname");
        }

        public ActionResult NoBookings(){
            return View();
        }

        [HttpGet]
        public ActionResult CancelBooking(){
            var uname=HttpContext.Session.GetString("uname");
            if(uname!=null){
                return View();
            }
            else{
                return RedirectToAction("Login","Login");
            } 
        }

        [HttpPost]
        [ActionName("CancelBooking")]
        public ActionResult CancelBookingConfirmed(int id){
            var tempinbooking = (from i in db.PragatiBookings
                                  where i.Bookingid==id
                                  select i).SingleOrDefault();
            var passengerid = tempinbooking.Passengerid;
            var tempinpassenger = (from i in db.PragatiPassengers
                                  where i.Passengerid==passengerid
                                  select i).SingleOrDefault();
            db.PragatiBookings.Remove(tempinbooking);
            db.PragatiPassengers.Remove(tempinpassenger);
            db.SaveChanges();
            return RedirectToAction("YourBookings");
        }
    }
}