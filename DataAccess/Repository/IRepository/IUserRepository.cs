using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository.IRepository
{
    public interface IUserRepository : IRepository<User>
    {
        public void Update(User user); 

        public string HashPassword(string password);

        public bool VerifyPassword(User user, string password);
        
        public abstract Task<User> GetUserByEmail(string email);
    }
}
