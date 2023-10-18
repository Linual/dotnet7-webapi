using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using dotnet7.Models;

namespace dotnet7.Data
{
    public class AuthRepository : IAuthRepository
    {
        private readonly DataContext _context;

        public AuthRepository(DataContext context)
        {
            _context = context;
        }
        public Task<ServiceResponce<string>> Login(string username, string password)
        {
            throw new NotImplementedException();
        }

        public async Task<ServiceResponce<int>> Register(User user, string passwords)
        {
            _context.UsersAdd(user);
            await _context.SaveChangesAsync();
            var responce = new ServiceResponce<int>();
            responce.Data = user.Id;
            return responce;
        }

        public Task<bool> UserExists(string username)
        {
            throw new NotImplementedException();
        }
    }
}