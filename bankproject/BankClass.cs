namespace bankeg{
    public class SBAccount{                             // by default class is internal but it created a problem in accessiblity with interface which has all methods as public by default, so class is made public here
        public int AccountNumber{get;set;}

        public string CustomerName{get;set;}

        public string CustomerAddress{get;set;}

        public decimal CurrentBalance{get;set;}

        public SBAccount(){}

        public SBAccount(int acc, string customerName, string customerAddress, decimal currentBalance){
            AccountNumber=acc;
            CustomerName=customerName;
            CustomerAddress=customerAddress;
            CurrentBalance=currentBalance;
        } 

        public override string ToString(){
            return AccountNumber+" "+CustomerName+" "+CustomerAddress+" "+CurrentBalance;
        }
    }

    public class SBTransaction{
        public int TransactionId{get;set;}

        public DateTime TransactionDate{get;set;}

        public int AccountNumber{get;set;}

        public decimal Amount{get;set;}

        public string TransactionType{get;set;}

        public SBTransaction(){}

        public SBTransaction(int tID, DateTime tDate, int acNo, decimal amount, string tType){
            TransactionId = tID;
            TransactionDate = tDate;
            AccountNumber = acNo;
            Amount = amount;
            TransactionType = tType;
        } 

        public override string ToString(){
            return TransactionId+" "+TransactionDate+" "+AccountNumber+" "+Amount+" "+TransactionType;
        }
    }

    class BankRepository : IBankRepository{
        public List<SBAccount> ListOfAccounts = new List<SBAccount>();
        public List<SBTransaction> ListOfTransactions = new List<SBTransaction>();

        int transID = 1;

        public void NewAccount(SBAccount acc){
            ListOfAccounts.Add(acc);
        }

        public List<SBAccount> GetAllAccounts(){
            return ListOfAccounts;
        }

        public SBAccount GetAccountDetails(int accno){
            var account = (from i in ListOfAccounts
                             where i.AccountNumber==accno
                             select i).SingleOrDefault();
            return account;
        }

        public void DepositAmount(int accno, decimal amt){
            SBAccount account = (from i in ListOfAccounts
                             where i.AccountNumber==accno
                             select i).SingleOrDefault();
            account.CurrentBalance = account.CurrentBalance + amt;
            transID++;
            DateTime curDateTime = DateTime.Now;
            ListOfTransactions.Add(new SBTransaction(transID,curDateTime,accno,amt,"Deposit"));
            System.Console.WriteLine(amt+" rupees added into your account.");
        }
        public void WithdrawAmount(int accno, decimal amt){
            SBAccount account = (from i in ListOfAccounts
                             where i.AccountNumber==accno
                             select i).SingleOrDefault();
            if(account.CurrentBalance >= amt){
                account.CurrentBalance = account.CurrentBalance - amt;
                transID++;
                DateTime curDateTime = DateTime.Now;
                ListOfTransactions.Add(new SBTransaction(transID,curDateTime,accno,amt,"Withdraw"));
                System.Console.WriteLine(amt+" rupees withdrawn from your account.");
            }
            else{
                System.Console.WriteLine("Insufficient Balance!!!");
            }
        }
        public List<SBTransaction> GetTransactions(int accno){
            List<SBTransaction> transactions = (from i in ListOfTransactions
                                                 where i.AccountNumber==accno
                                                 select i).ToList();
            return transactions;
        }
    }

}