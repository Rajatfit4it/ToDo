using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using DAL.Contracts;
using Microsoft.EntityFrameworkCore;
using dm = DAL.Domain;

namespace DAL
{
    public class CategoryData : ICategoryData
    {
        private readonly dm.ToDoContext _context;

        public CategoryData(dm.ToDoContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<dm.Category>> GetAll()
        {
            return await _context.Categories.ToListAsync();
        }

        public async Task AddAsync(dm.Category category)
        {
            await _context.Categories.AddAsync(category);
        }
    }
}
