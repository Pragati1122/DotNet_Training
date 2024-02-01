namespace flightapi.Repository
{

    public interface IPragatiFlightUser<PragatiFlightUser>
    {
        Task<List<PragatiFlightUser>> GetAllUsers();

        void AddUser(PragatiFlightUser u);

        void UpdateUser(string username, PragatiFlightUser u);

        Task<PragatiFlightUser> GetUserByUsername(string username);

        void DeleteUser(string username);
    }
}