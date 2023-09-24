using DataAccess.Data;
using DataAccess.Repository.IRepository;
using Microsoft.EntityFrameworkCore;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        private readonly ApplicationDbContext _db; 
        public UserRepository(ApplicationDbContext db) : base(db)
        {
            _db = db; 
        }

        public string HashPassword(string password)
        {
            return BCrypt.Net.BCrypt.HashPassword(password);
        }

        public async void Update(User _user)
        {
            var productFromDb = _db.Users.FirstOrDefault(user => user.Id == _user.Id);
            if(productFromDb != null)
            {
                productFromDb.Name = _user.Name;
                productFromDb.Email = _user.Email;
                productFromDb.Password = _user.Password;

                await _db.SaveChangesAsync(); 
            }
        }

        public bool VerifyPassword(User user, string password)
        {
            return BCrypt.Net.BCrypt.Verify(password, user.Password);
        }

        public async Task<User> GetUserByEmail(string email) => await _db.Users.FirstOrDefaultAsync(Task => Task.Email == email);
    }
}
