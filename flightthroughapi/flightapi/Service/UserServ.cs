using flightapi.Models;
using flightapi.Repository;

namespace flightapi.Service
{
    public class UserServ : IUserServ<PragatiFlightUser>
    {
        private readonly IPragatiFlightUser<PragatiFlightUser> userrepo;
        public UserServ(){}
        public UserServ(IPragatiFlightUser<PragatiFlightUser> _userrepo){
            userrepo = _userrepo;
        }
        public void AddUser(PragatiFlightUser u)
        {
            userrepo.AddUser(u);
        }

        public void DeleteUser(string username)
        {
            userrepo.DeleteUser(username);
        }

        public async Task<List<PragatiFlightUser>> GetAllUsers()
        {
            return await userrepo.GetAllUsers();
        }

        public async Task<PragatiFlightUser> GetUserByUsername(string username)
        {
            return await userrepo.GetUserByUsername(username);
        }

        public void UpdateUser(string username, PragatiFlightUser u)
        {
            userrepo.UpdateUser(username,u);
        }
    }
}