using Microsoft.AspNetCore.Mvc;
using flightapi.Models;
using System.Net.Http.Headers;
using Newtonsoft.Json;
using System.Text;
using System.Security.Cryptography.X509Certificates;

namespace flightclientapp
{
    public class UserController : Controller{

        [HttpGet]
        public ActionResult GetFlightRequirements(){
            return View();
        }   

        [HttpPost]
        public ActionResult GetFlightRequirements(PragatiFlight p){
            TempData["src"] = p.Source;
            TempData["dest"] = p.Destination;
            return RedirectToAction("ShowMatchingFlights");
        }

        [HttpGet]
        public async Task<IActionResult> ShowMatchingFlights(){
            
            List<PragatiFlight> flights = new List<PragatiFlight>();
            Place p = new Place();
            p.Source=TempData["src"].ToString();
            p.Destination=TempData["dest"].ToString();

            HttpClient client = new HttpClient();

            client.DefaultRequestHeaders.Clear();

            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
 
            HttpResponseMessage Res = await client.GetAsync("http://localhost:5064/api/Flight");
 
            if (Res.IsSuccessStatusCode)
                { 
                    var FlightResponse = Res.Content.ReadAsStringAsync().Result;
 
                    flights = JsonConvert.DeserializeObject<List<PragatiFlight>>(FlightResponse);

                }
                
                var matchingFlights = flights.Where(x=>x.Source == p.Source && x.Destination == p.Destination).ToList();

                if(matchingFlights.Any()){
                    return View(matchingFlights);
                }
                else return RedirectToAction("NoFlightsAvailablePage");
            }

            public ActionResult NoFlightsAvailablePage(){
                return View();
            }

            [HttpGet]
            public ActionResult BookAFlight(int id){
                ViewBag.flightid=id;
                TempData["flightid"]=id;
                var uname=HttpContext.Session.GetString("uname");
                if(uname!=null){
                    return View();
                }
                else{
                    return RedirectToAction("Login","Login");
                }
                
            }

            [HttpPost]

            public async Task<IActionResult> BookAFlight(PragatiPassenger p){

            PragatiPassenger passengerobj = new PragatiPassenger();
            int flightid = Convert.ToInt32(TempData["flightid"].ToString());
            using (var httpClient = new HttpClient())
            {
                //POST passenger
                StringContent content = new StringContent(JsonConvert.SerializeObject(p), 
              Encoding.UTF8, "application/json");

                using (var response = await httpClient.PostAsync("http://localhost:5064/api/Passenger", content))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    passengerobj = JsonConvert.DeserializeObject<PragatiPassenger>(apiResponse);
                }
                Console.WriteLine(passengerobj.Name+" "+passengerobj.Passengerid);

                //GET related flight
                PragatiFlight flight = new PragatiFlight();
                using (var response = await httpClient.GetAsync("http://localhost:5064/api/Flight/" + flightid))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    flight = JsonConvert.DeserializeObject<PragatiFlight>(apiResponse);
                }

                // POST booking entry
                PragatiBooking x = new PragatiBooking();
                x.Flightid = flightid;
                x.Passengerid = passengerobj.Passengerid;
                x.Bookingdate = DateTime.Now;
                x.TotalCost = flight.Price;
                Console.WriteLine("x value: "+x.Flightid+" "+x.Passengerid+" "+x.Bookingdate+" "+x.TotalCost);
                StringContent content3 = new StringContent(JsonConvert.SerializeObject(x), 
              Encoding.UTF8, "application/json");

                using (var response = await httpClient.PostAsync("http://localhost:5064/api/Booking", content3))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    Console.WriteLine("Respone: "+apiResponse);
                    x = JsonConvert.DeserializeObject<PragatiBooking>(apiResponse);
                }

                //PUT flight
                PragatiFlight updatedFlight = new PragatiFlight();
                updatedFlight.Flightid = flightid;
                updatedFlight.Flightname = flight.Flightname;
                updatedFlight.Source = flight.Source;
                updatedFlight.Destination = flight.Destination;
                updatedFlight.StartTime = flight.StartTime;
                updatedFlight.EndTime = flight.EndTime;
                updatedFlight.Layover = flight.Layover;
                updatedFlight.SeatsAvailable = flight.SeatsAvailable - 1;
                updatedFlight.Price = flight.Price;
                updatedFlight.Date = flight.Date;
                StringContent content2 = new StringContent(JsonConvert.SerializeObject(updatedFlight)
         , Encoding.UTF8, "application/json");
                using (var response = await httpClient.PutAsync("http://localhost:5064/api/Flight/" + flightid, content2))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    ViewBag.Result = "Success";
                    updatedFlight = JsonConvert.DeserializeObject<PragatiFlight>(apiResponse);
                }            
            }

            

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


        }
}
