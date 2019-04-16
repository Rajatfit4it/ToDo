using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using DAL.Contracts;
using DAL.Domain;
using Microsoft.EntityFrameworkCore;

namespace DAL
{
    public class UserData : IUserData
    {
        private readonly ToDoContext _context;

        public UserData(ToDoContext context)
        {
            _context = context;
        }

        public async Task CreateUserAsync(User user)
        {
            await _context.Users.AddAsync(user);
        }

        public async Task<User> GetUserByUserNameAsync(string UserName)
        {
            return await _context.Users.FirstOrDefaultAsync(e => e.UserName == UserName);
        }
    }
}
