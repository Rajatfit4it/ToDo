using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using DAL.Domain;

namespace DAL.Contracts
{
    public interface IUserData
    {
        Task CreateUserAsync(User user);
        Task<User> GetUserByUserNameAsync(string UserName);
    }
}
