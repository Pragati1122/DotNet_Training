using Microsoft.AspNetCore.Mvc;
using flightapi.Models;
using System.Net.Http.Headers;
using Newtonsoft.Json;
using System.Text;

namespace flightclientapp
{
    public class AdminController : Controller{

        public static List<PragatiFlight> flights = new List<PragatiFlight>();

        public async Task<IActionResult> ShowAllFlights(){
            HttpClient client = new HttpClient();

            client.DefaultRequestHeaders.Clear();

             //Define request data format
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            //Sending request to find web api REST service resource ShowAllFlights using HttpClient  
            HttpResponseMessage Res = await client.GetAsync("http://localhost:5064/api/Flight");

            //Checking the response is successful or not which is sent using HttpClient  
            if (Res.IsSuccessStatusCode)
                {
                    //Storing the response details recieved from web api   
                    var FlightResponse = Res.Content.ReadAsStringAsync().Result;

                    //Deserializing the response recieved from web api and storing into the Employee list  
                    flights = JsonConvert.DeserializeObject<List<PragatiFlight>>(FlightResponse);

                }
                var uname=HttpContext.Session.GetString("uname");
                if(uname!=null && uname=="admin"){
                    return View(flights);
                }
                else{
                    return RedirectToAction("Login","Login");
                }   
                    
            }
        
        public ActionResult AddFlight(){
            var uname=HttpContext.Session.GetString("uname");
                if(uname!=null && uname=="admin"){
                    return View();
                }
                else{
                    return RedirectToAction("Login","Login");
                } 
            
        }

        [HttpPost]
        public async Task<IActionResult> AddFlight(PragatiFlight c){
            PragatiFlight flightobj = new PragatiFlight();
            using (var httpClient = new HttpClient())
            {
                StringContent content = new StringContent(JsonConvert.SerializeObject(c), 
              Encoding.UTF8, "application/json");

                using (var response = await httpClient.PostAsync("http://localhost:5064/api/Flight", content))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    flightobj = JsonConvert.DeserializeObject<PragatiFlight>(apiResponse);
                }
            }
            return RedirectToAction("ShowAllFlights");
        }

        [HttpGet]
        public async Task<ActionResult> UpdateFlight(int id)
        {
            PragatiFlight flight = new PragatiFlight();
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync("http://localhost:5064/api/Flight/" + id))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    flight = JsonConvert.DeserializeObject<PragatiFlight>(apiResponse);
                }
            }
            var uname=HttpContext.Session.GetString("uname");
                if(uname!=null && uname=="admin"){
                    return View(flight);
                }
                else{
                    return RedirectToAction("Login","Login");
                } 
            
        }

        [HttpPost]
        public async Task<ActionResult> UpdateFlight(PragatiFlight e)
        {
            PragatiFlight receivedflight = new PragatiFlight();

            using (var httpClient = new HttpClient())
            {
                #region
                //var content = new MultipartFormDataContent();
                //content.Add(new StringContent(reservation.Empid.ToString()), "Empid");
                //content.Add(new StringContent(reservation.Name), "Name");
                //content.Add(new StringContent(reservation.Gender), "Gender");
                //content.Add(new StringContent(reservation.Newcity), "Newcity");
                //content.Add(new StringContent(reservation.Deptid.ToString()), "Deptid");
                #endregion
                int id = e.Flightid;
                StringContent content1 = new StringContent(JsonConvert.SerializeObject(e)
         , Encoding.UTF8, "application/json");
                using (var response = await httpClient.PutAsync("http://localhost:5064/api/Flight/" + id, content1))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    ViewBag.Result = "Success";
                    receivedflight = JsonConvert.DeserializeObject<PragatiFlight>(apiResponse);
                }
            }
            return RedirectToAction("ShowAllFlights");
        }

        [HttpGet]
        public async Task<ActionResult> DeleteFlight(int id)
        {
            TempData["flightid"] = id;
            PragatiFlight flight = new PragatiFlight();
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync("http://localhost:5064/api/Flight/" + id))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    flight = JsonConvert.DeserializeObject<PragatiFlight>(apiResponse);
                }
            }
            var uname=HttpContext.Session.GetString("uname");
                if(uname!=null && uname=="admin"){
                    return View(flight);
                }
                else{
                    return RedirectToAction("Login","Login");
                }
        }

        [HttpPost]
    //    [ActionName("DeleteFlight")]
        public async Task<ActionResult> DeleteFlight(PragatiFlight e)
        {
            int flightid = Convert.ToInt32(TempData["flightid"]);
            Console.WriteLine(flightid);
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.DeleteAsync("http://localhost:5064/api/Flight/" + flightid))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                }
            }

            return RedirectToAction("ShowAllFlights");
        }

        [HttpGet]
        public async Task<ActionResult> DetailOfFlight(int id)
        {
            PragatiFlight flight = new PragatiFlight();
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync("http://localhost:5064/api/Flight/" + id))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    flight = JsonConvert.DeserializeObject<PragatiFlight>(apiResponse);
                }
            }
            var uname=HttpContext.Session.GetString("uname");
                if(uname!=null && uname=="admin"){
                    return View(flight);
                }
                else{
                    return RedirectToAction("Login","Login");
                }
        }

        [HttpGet]
        public async Task<IActionResult> ShowAllBookings(){
            List<PragatiBooking> bookings = new List<PragatiBooking>();
            HttpClient client = new HttpClient();

            client.DefaultRequestHeaders.Clear();

            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
 
            HttpResponseMessage Res = await client.GetAsync("http://localhost:5064/api/Booking");
 
            if (Res.IsSuccessStatusCode)
                {
                    //Storing the response details recieved from web api   
                    var BookingResponse = Res.Content.ReadAsStringAsync().Result;

                    //Deserializing the response recieved from web api and storing into the Employee list  
                    bookings = JsonConvert.DeserializeObject<List<PragatiBooking>>(BookingResponse);

                }
                //returning the bookings list to view  
                
                var uname=HttpContext.Session.GetString("uname");
                if(uname!=null && uname=="admin"){
                    return View(bookings);
                }
                else{
                    return RedirectToAction("Login","Login");
                }
            }

        }
}
