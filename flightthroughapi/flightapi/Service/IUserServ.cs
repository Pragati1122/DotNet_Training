namespace flightapi.Service
{
    public interface IUserServ<PragatiFlightUser>
    {
        Task<List<PragatiFlightUser>> GetAllUsers();

        void AddUser(PragatiFlightUser u);

        void UpdateUser(string username, PragatiFlightUser u);

        Task<PragatiFlightUser> GetUserByUsername(string username);

        void DeleteUser(string username);

        string Message(string name){
            return "Hello "+name;
        }
    }
}