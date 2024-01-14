namespace bankeg{
    class BankClient{
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
                System.Console.WriteLine("Enter the new Account Number: ");
                int accNo = Convert.ToInt32(System.Console.ReadLine());

                System.Console.WriteLine("Enter the Customer's Name: ");
                string customerName = System.Console.ReadLine();

                System.Console.WriteLine("Enter the Customer's Address: ");
                string customerAddress = System.Console.ReadLine();

                System.Console.WriteLine("Enter initial balance:");
                decimal balance  = decimal.Parse(System.Console.ReadLine());

                SBI.NewAccount(new SBAccount(accNo,customerName,customerAddress,balance));

                System.Console.WriteLine("Account added successfully.");

                break;

                case 2:
                System.Console.WriteLine("Enter the Account Number for details: ");
                int acctNo = Convert.ToInt32(System.Console.ReadLine());
                
                SBAccount accountDetails = SBI.GetAccountDetails(acctNo);

                System.Console.WriteLine("Details of this account are as follows: ");
                System.Console.WriteLine(accountDetails);

                break;

                case 3:
                System.Console.WriteLine("Below are all available Account details in SBI: ");
                List<SBAccount> allAccounts = new List<SBAccount>(SBI.GetAllAccounts());
                
                if (!allAccounts.Any()){
                    System.Console.WriteLine("No records available!");
                }
                else{
                    foreach(SBAccount item in allAccounts){
                        System.Console.WriteLine(item);
                    }
                }

                break;

                case 4:
                System.Console.WriteLine("Enter the Account No and amount to be deposited: ");
                int depositAcNo = Convert.ToInt32(System.Console.ReadLine());
                decimal depositAmount = decimal.Parse(System.Console.ReadLine());
                SBI.DepositAmount(depositAcNo, depositAmount);

                break;

                case 5:
                System.Console.WriteLine("Enter the Account No and amount to be withdrawn: ");
                int withdrawalAcNo = Convert.ToInt32(System.Console.ReadLine());
                decimal withdrawalAmount = decimal.Parse(System.Console.ReadLine());
                SBI.WithdrawAmount(withdrawalAcNo, withdrawalAmount);

                break;

                case 6:
                System.Console.WriteLine("Enter the concerned Account No: ");
                int transactionsAcNo = Convert.ToInt32(System.Console.ReadLine());
                List<SBTransaction> allTransactions = new List<SBTransaction>(SBI.GetTransactions(transactionsAcNo));
                if(!allTransactions.Any()){
                    System.Console.WriteLine("No record available for this account!!!");
                }
                else{
                    foreach(SBTransaction item in allTransactions){
                        System.Console.WriteLine(item);
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