using flightapi.Models;
using Microsoft.EntityFrameworkCore;

namespace flightapi.Repository{
    public class UserRepo : IPragatiFlightUser<PragatiFlightUser>
    {
        private readonly Ace52024Context db;
        public UserRepo(){}

        public UserRepo(Ace52024Context _db){
            db=_db;
        }
        public async Task<List<PragatiFlightUser>> GetAllUsers(){
            return await db.PragatiFlightUsers.ToListAsync();
        }
        public void AddUser(PragatiFlightUser u)
        {
            db.PragatiFlightUsers.Add(u);
            db.SaveChanges();
        }

        public void DeleteUser(string username)
        {
            PragatiFlightUser u = db.PragatiFlightUsers.Find(username);
            db.PragatiFlightUsers.Remove(u);
            db.SaveChanges();
        }

        public async Task<PragatiFlightUser> GetUserByUsername(string username)
        {
            return await db.PragatiFlightUsers.FindAsync(username);
        }

        public void UpdateUser(string username, PragatiFlightUser u)
        {
            db.PragatiFlightUsers.Update(u);
            db.SaveChanges();
        }
    }
}


      
            