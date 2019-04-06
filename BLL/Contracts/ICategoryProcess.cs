using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using vm = ViewModel;

namespace BLL.Contracts
{
    public interface ICategoryProcess
    {
        Task<IEnumerable<vm.Category>> GetAll();
        Task AddAsync(vm.Category category);
        Task<vm.Category> GetCategoryAsync(int id);
        Task<bool> UpdateAsync(vm.Category category);
        Task<bool> RemoveCategoryAsync(int id);
    }
}
