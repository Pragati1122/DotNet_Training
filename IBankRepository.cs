using bankproject.Models;

namespace bankeg{
    public interface IBankRepository{
        void NewAccount(PragatiSbaccount acc);
        List<PragatiSbaccount> GetAllAccounts();
        PragatiSbaccount GetAccountDetails(int accno);
        void DepositAmount(int accno, decimal amt);
        // void WithdrawAmount(int accno, decimal amt);
        List<PragatiSbtransaction> GetTransactions(int accno);
    }


}