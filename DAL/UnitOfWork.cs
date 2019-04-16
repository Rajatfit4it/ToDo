using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using DAL.Contracts;
using dm = DAL.Domain;

namespace DAL
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly dm.ToDoContext _context;
        public ICategoryData CategoryData { get; set; }
        public IUserData UserData { get; set; }

        public UnitOfWork(dm.ToDoContext context)
        {
            _context = context;
        }
        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
