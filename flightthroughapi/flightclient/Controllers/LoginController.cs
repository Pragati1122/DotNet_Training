using Microsoft.AspNetCore.Mvc;
using flightapi.Models;
using System.Net.Http.Headers;
using Newtonsoft.Json;
using System.Text;

namespace flightclientapp
{
    public class LoginController : Controller{

        private readonly ISession session;
        //Constructor Injection
        public LoginController(IHttpContextAccessor httpContextAccessor){
            session = httpContextAccessor.HttpContext.Session;
        }

        public ActionResult RegisterUser(){
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> RegisterUser(PragatiFlightUser u){
            PragatiFlightUser obj = new PragatiFlightUser();
            using (var httpClient = new HttpClient())
            {
                StringContent content = new StringContent(JsonConvert.SerializeObject(u), 
              Encoding.UTF8, "application/json");

                using (var response = await httpClient.PostAsync("http://localhost:5064/api/Users", content))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    obj = JsonConvert.DeserializeObject<PragatiFlightUser>(apiResponse);
                }
            }
            return RedirectToAction("UserRegistered");
        }

        [HttpGet]
        public ActionResult UserRegistered(){
            return View();
        }


        [HttpGet]
        public ActionResult Login(){
            return View();
        }

        [HttpPost]
         public async Task<IActionResult> Login(PragatiFlightUser u){
            PragatiFlightUser obj = new PragatiFlightUser();
            using (var httpClient = new HttpClient())
            {
                StringContent content = new StringContent(JsonConvert.SerializeObject(u), 
              Encoding.UTF8, "application/json");

                using (var response = await httpClient.PostAsync("http://localhost:5064/api/Login", content))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    obj = JsonConvert.DeserializeObject<PragatiFlightUser>(apiResponse);
                }
            }
            // Console.WriteLine("value of obj: "+obj.Username+" "+obj.Email);
            if(obj!=null){
                    HttpContext.Session.SetString("uname",obj.Username);        //setting the string for session
                    if(u.Email.Contains("@fareportal.com")){
                        return RedirectToAction("ShowAllFlights","Admin");
                    }
                    else
                        return RedirectToAction("GetFlightRequirements","User");           //takes 2 args --> action method & the controller under which it is
            }
            else{
                return  View();
            }
        }

        public IActionResult Logout(){
            HttpContext.Session.Clear();          
            return RedirectToAction("Login");
        }
        
        }
}
