using bankproject.Models;

namespace bankeg{
    class InvalidAccNoException : ApplicationException
    {
        public InvalidAccNoException(string message):base(message){}
    }
    
    class InvalidBalException : ApplicationException
    {
        public InvalidBalException(string message):base(message){}
    }

    class DepositException : ApplicationException
    {
        public DepositException(string message):base(message){}
    }

    class WithdrawException : ApplicationException
    {
        public WithdrawException(string message):base(message){}
    }
    class BankClientDb{
        public static void Main(){
            BankRepository SBI = new BankRepository();

            System.Console.WriteLine("   --Welcome to SBI Banking--  ");

            bool exit = false;
            while(exit!=true){
                System.Console.WriteLine("Choose an option from below: ");
                System.Console.WriteLine("1. New Account Creation");
                System.Console.WriteLine("2. Get your account Details");
                System.Console.WriteLine("3. Fetch all accounts");
                System.Console.WriteLine("4. Deposit Amount");
                System.Console.WriteLine("5. Withdraw Amount");
                System.Console.WriteLine("6. Get Transaction details");
                int choice = Convert.ToInt32(System.Console.ReadLine());
                switch(choice)
                {
                case 1:
                PragatiSbaccount newAccountRecord = new PragatiSbaccount();

                // System.Console.WriteLine("Enter the new Account Number: ");
                // newAccountRecord.AccountNumber = Convert.ToInt32(System.Console.ReadLine());

                System.Console.WriteLine("Enter the Customer's Name: ");
                newAccountRecord.CustomerName = System.Console.ReadLine();

                System.Console.WriteLine("Enter the Customer's Address: ");
                newAccountRecord.CustomerAddress = System.Console.ReadLine();

                System.Console.WriteLine("Enter initial balance:");
                newAccountRecord.CurrentBalance  = decimal.Parse(System.Console.ReadLine());

                try{
                    SBI.NewAccount(newAccountRecord);
                }
                catch(InvalidBalException x){
                    System.Console.WriteLine(x.Message);
                }
                

                break;

                case 2:
                System.Console.WriteLine("Enter the Account Number for details: ");
                int acctNo = Convert.ToInt32(System.Console.ReadLine());
                try{
                    PragatiSbaccount accountDetails = SBI.GetAccountDetails(acctNo);
                    if(acctNo==null){
                        continue;
                    }

                    System.Console.WriteLine("Details of this account are as follows: ");
                    System.Console.WriteLine("Account Number : "+accountDetails.AccountNumber+", Account Holder: "+accountDetails.CustomerName+", Address of Account Holder: "+accountDetails.CustomerAddress+", Current Balance: "+accountDetails.CurrentBalance);
                }
                catch(InvalidAccNoException x){
                    System.Console.WriteLine(x.Message);
                }
                

                break;

                case 3:
                System.Console.WriteLine("Below are all available Account details in SBI: ");
                List<PragatiSbaccount> allAccounts = new List<PragatiSbaccount>(SBI.GetAllAccounts());
                
                if (!allAccounts.Any()){
                    System.Console.WriteLine("No records available!");
                }
                else{
                    foreach(PragatiSbaccount item in allAccounts){
                        System.Console.WriteLine(item.AccountNumber+" "+item.CustomerName+" "+item.CustomerAddress+" "+item.CurrentBalance);
                    }
                }

                break;

                case 4:
                System.Console.WriteLine("Enter the Account No and amount to be deposited: ");
                int depositAcNo = Convert.ToInt32(System.Console.ReadLine());
                decimal depositAmount = decimal.Parse(System.Console.ReadLine());
                try{
                    SBI.DepositAmount(depositAcNo, depositAmount);
                }
                catch(DepositException x){
                    System.Console.WriteLine(x.Message);
                }
                

                break;

                case 5:
                System.Console.WriteLine("Enter the Account No and amount to be withdrawn: ");
                int withdrawalAcNo = Convert.ToInt32(System.Console.ReadLine());
                decimal withdrawalAmount = decimal.Parse(System.Console.ReadLine());
                try{
                    SBI.WithdrawAmount(withdrawalAcNo, withdrawalAmount);
                }
                catch(WithdrawException x){
                    System.Console.WriteLine(x.Message);
                }

                break;

                case 6:
                System.Console.WriteLine("Enter the concerned Account No: ");
                int transactionsAcNo = Convert.ToInt32(System.Console.ReadLine());
                List<PragatiSbtransaction> allTransactions = new List<PragatiSbtransaction>(SBI.GetTransactions(transactionsAcNo));
                if(!allTransactions.Any()){
                    System.Console.WriteLine("No record available for this account!!!");
                }
                else{
                    foreach(PragatiSbtransaction item in allTransactions){
                        System.Console.WriteLine(item.TransactionId+" "+item.TransactionDate+" "+item.AccountNumber+" "+item.TransactionType+" "+item.Amount);
                    }
                }

                break;

                default:
                System.Console.WriteLine("Invalide Choice!");

                break;


            }
            }
            

        }
    }
}