using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using dm = DAL.Domain;

namespace DAL.Contracts
{
    public interface ICategoryData
    {
        Task<IEnumerable<dm.Category>> GetAll();
        Task AddAsync(dm.Category category);
        Task<dm.Category> GetCategoryAsync(int id);
        void RemoveCategory(dm.Category category);
    }
}
