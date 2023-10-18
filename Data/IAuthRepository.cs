using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using dotnet7.Models;

namespace dotnet7.Data
{
    public interface IAuthRepository
    {
        Task<ServiceResponce<int>> Register(User user, string passwords);
        Task<ServiceResponce<string>> Login(string username, string password);
        Task<bool> UserExists(string username);
    }
}