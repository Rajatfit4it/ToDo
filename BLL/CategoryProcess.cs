using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using BLL.Contracts;
using DAL.Contracts;
using vm = ViewModel;
using dm = DAL.Domain;
namespace BLL
{
    public class CategoryProcess : ICategoryProcess
    {
        private readonly IUnitOfWork _unitOfWork;
        //private readonly ICategoryData _categoryData;

        public CategoryProcess(IUnitOfWork unitOfWork, ICategoryData categoryData)
        {
            _unitOfWork = unitOfWork;
            _unitOfWork.CategoryData = categoryData;
            //_categoryData = categoryData;
        }

        public async Task<IEnumerable<vm.Category>> GetAll()
        {
            var result = await _unitOfWork.CategoryData.GetAll();
            List<vm.Category> categories = new List<vm.Category>();
            foreach (var category in result)
            {
                categories.Add(new vm.Category() { Id = category.Id, Name = category.Name });
            }

            return categories;
        }

        public async Task AddAsync(vm.Category category)
        {
            dm.Category dmCategory = new dm.Category() { Name = category.Name };
            await _unitOfWork.CategoryData.AddAsync(dmCategory);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task<vm.Category> GetCategoryAsync(int id)
        {
            var result = await _unitOfWork.CategoryData.GetCategoryAsync(id);
            if(result != null)
                return new vm.Category(){ Id = result.Id, Name = result.Name};
            return null;
        }

        public async Task<bool> UpdateAsync(vm.Category category)
        {
            var dmCategory = await _unitOfWork.CategoryData.GetCategoryAsync(category.Id);
            if (dmCategory != null)
            {
                dmCategory.Name = category.Name;
                await _unitOfWork.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<bool> RemoveCategoryAsync(int id)
        {
            var dmCategory = await _unitOfWork.CategoryData.GetCategoryAsync(id);
            if (dmCategory != null)
            {
                _unitOfWork.CategoryData.RemoveCategory(dmCategory);
                await _unitOfWork.SaveChangesAsync();
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
