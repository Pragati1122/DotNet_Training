using firstmvcproj.Controllers;
using firstmvcproj.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace firstmvproj.Controllers{
    public class AccountController : Controller{
        private readonly Ace52024Context db;

        public AccountController(Ace52024Context _db){
            db=_db;
        }

        public ActionResult ShowAllAccounts(){
            return View(db.PragatiSbaccounts);
        }

        // DATA FETCHING FROM FOREIGN KEY TABLE : EAGERLY LOADING
        public ActionResult Details(int accNo){             // fetching data from related foreign key table
            var temp = db.PragatiSbaccounts.Where(x=>x.AccountNumber == accNo).Include(x=>x.PragatiSbtransactions).SingleOrDefault();//acc to the situation this should contain only one record of pragatiSBAccount
            List<PragatiSbtransaction> relatedTransactions = new List<PragatiSbtransaction>();
            foreach(var item in temp.PragatiSbtransactions){
                relatedTransactions.Add(item);
            }
            return View(relatedTransactions);     // the view for this action expects a list of pragatiSBTransactions
        }

    }
}