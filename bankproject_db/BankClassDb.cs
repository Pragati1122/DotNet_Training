using System.Net;
using bankproject.Models;

namespace bankeg{

    class BankRepository : IBankRepository{
        
        private static Ace52024Context db = new Ace52024Context();

        public void NewAccount(PragatiSbaccount acc){
            if(acc.AccountNumber <0)
            {
                throw new InvalidAccNoException("Please enter a valid account number!");
            }
            if(acc.CurrentBalance < 0)
            {
                throw new InvalidBalException("Please enter a valid balance!");
            }
            else{
                db.PragatiSbaccounts.Add(acc);
                db.SaveChanges();
                System.Console.WriteLine("New Account added successfully.");
            }
        }

        public List<PragatiSbaccount> GetAllAccounts(){
            return db.PragatiSbaccounts.ToList();
        }

        public PragatiSbaccount GetAccountDetails(int accno){
            PragatiSbaccount accountDetails = db.PragatiSbaccounts.Find(accno);
            if(accountDetails == null){
                throw new InvalidAccNoException("Account Not Found!!!");
            }
            return accountDetails;
        }

        public void DepositAmount(int accno, decimal amt){
            PragatiSbaccount account = db.PragatiSbaccounts.Find(accno);
            if(account == null){
                throw new InvalidAccNoException("Please enter a valid Account Number");
            }
            if(amt<0){
                throw new DepositException("Please enter a valid amount!");
            }
            else{
                account.CurrentBalance = account.CurrentBalance + amt;
                db.PragatiSbaccounts.Update(account);
                PragatiSbtransaction newTrans = new PragatiSbtransaction();
                newTrans.TransactionType = "Deposit";
                newTrans.TransactionDate = DateTime.Now;
                newTrans.Amount = amt;
                newTrans.AccountNumber = accno;
                db.PragatiSbtransactions.Add(newTrans);
                db.SaveChanges();
                System.Console.WriteLine(amt+" rupees added into your account.");
            } 
        }

        public void WithdrawAmount(int accno, decimal amt){
            PragatiSbaccount account = db.PragatiSbaccounts.Find(accno);
            if(account == null){
                throw new InvalidAccNoException("Please enter a valid Account Number");
            }
            if(amt<0){
                throw new DepositException("Please enter a valid amount!");
            }
            if(account.CurrentBalance >= amt){
                account.CurrentBalance = account.CurrentBalance - amt;
                db.PragatiSbaccounts.Update(account);
                PragatiSbtransaction newTrans = new PragatiSbtransaction();
                newTrans.TransactionType = "Withdrawal";
                newTrans.TransactionDate = DateTime.Now;
                newTrans.Amount = amt;
                newTrans.AccountNumber = accno;
                db.PragatiSbtransactions.Add(newTrans);
                db.SaveChanges();
                System.Console.WriteLine(amt+" rupees withdrawn from your account.");
            }
            else{
                System.Console.WriteLine("Insufficient Balance!!!");
            }
        }

        public List<PragatiSbtransaction> GetTransactions(int accno){
            PragatiSbaccount account = db.PragatiSbaccounts.Find(accno);
            if(account == null){
                throw new InvalidAccNoException("Please enter a valid Account Number");
            }
            List<PragatiSbtransaction> transactions = (from i in db.PragatiSbtransactions
                                                         where i.AccountNumber==accno       
                                                             select i).ToList();
            return transactions;
        }
    }

}