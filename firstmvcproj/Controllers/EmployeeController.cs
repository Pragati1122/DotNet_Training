using firstmvcproj.Models;
using Microsoft.AspNetCore.Mvc;

namespace firstmvcproj.Controllers{
    public class EmployeeController : Controller{                 // C in EmployeeController must be capital and a folder in view for this controller i.e. Employee
        //Action Method
        // Ace52024Context dbase = new Ace52024Context();
        public static Ace52024Context dbase;
        
        //Dependency Injection  in constructor
        public EmployeeController(Ace52024Context _db)
        {
            dbase=_db;
        }
        public ActionResult ShowEmpDetail(){                      // a .cshtml file inside that Employee folder in views
            List<Employee> employees = new List<Employee>();
            Employee e = new Employee();
            e.Eid = 101;
            e.Ename = "Ram";
            e.Salary = 50000;
            Employee e1 = new Employee();
            e1.Eid = 102;
            e1.Ename = "Meet";
            e1.Salary = 70000;
            employees.Add(e);
            employees.Add(e1);
            return View(employees);
        }

        // public ActionResult GetAllEmployees(){                      // a .cshtml file inside that Employee folder in views
            // List<Employee> emps = new List<Employee>();
            // Employee e = new Employee();
            // e.Eid = 101;
            // e.Ename = "Ram";
            // e.Salary = 50000;
            // Employee e1 = new Employee();
            // e1.Eid = 102;
            // e1.Ename = "Meet";
            // e1.Salary = 70000;
            // emps.Add(e);
            // emps.Add(e1);
            // return View(emps);

        // }


        [HttpGet]
        public ActionResult GetAllEmployeesFromDB(){
            ViewBag.Username=HttpContext.Session.GetString("uname");         //checks if the session has started yet or not(that is logged in or not)
            if(ViewBag.Username!=null){                   //ViewBag is used to pass data from controller to view-->check its corresponding view -->not needed to pass it through parameter
                return View(dbase.PragatiEmps);   // similar to select*from pragatiemp
            }                      // a .cshtml file inside that Employee folder in views
            else{
                return RedirectToAction("Login","Login");
            }
        }
        //agar is controller me ViewBag define kr dia gaya h to uske view me directly access kr skte hain 


        [HttpGet]            // optional to write this way beciz by default it is get
        public ActionResult AddEmployee(){
            return View();
        }

        [HttpPost]            //button click logic
        public ActionResult AddEmployee(PragatiEmp e){                
            dbase.PragatiEmps.Add(e);
            dbase.SaveChanges();
            return RedirectToAction("GetAllEmployeesFromDB");           // post method never returns a view rather it redirects to this mentioned action method 
        }

        public ActionResult Details(int id){              // REMEMBER : THE PARAM NAME SHOULD BE SAME AS PASSED FORM GETaLL METHOD
            PragatiEmp e = dbase.PragatiEmps.Where(x=>x.Eid == id).SingleOrDefault();
            return View(e);
        }

        [HttpGet]
        public ActionResult Edit(int id){
            PragatiEmp e = dbase.PragatiEmps.Where(x=>x.Eid==id).SingleOrDefault();
            return View(e);
        }

        [HttpPost]
        public ActionResult Edit(PragatiEmp e){
            dbase.PragatiEmps.Update(e);
            dbase.SaveChanges();
            return RedirectToAction("GetAllEmployeesFromDB");
        }

        public ActionResult Delete(int id){
            PragatiEmp e = dbase.PragatiEmps.Where(x=>x.Eid==id).SingleOrDefault();
            return View(e);
        }

        [HttpPost]
        [ActionName("Delete")]

        public ActionResult DeleteConfirmed(int id){
            PragatiEmp e = dbase.PragatiEmps.Where(x=>x.Eid==id).SingleOrDefault();
            dbase.PragatiEmps.Remove(e);
            dbase.SaveChanges();
            return RedirectToAction("GetAllEmployeesFromDB");
        }

}
}