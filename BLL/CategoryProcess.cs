using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using BLL.Contracts;
using DAL.Contracts;
using vm = ViewModel;
using dm = DAL.Domain;
namespace BLL
{
    public class CategoryProcess : ICategoryProcess
    {
        private readonly IUnitOfWork _unitOfWork;

        private readonly IMapper _mapper;
        //private readonly ICategoryData _categoryData;

        public CategoryProcess(IUnitOfWork unitOfWork, ICategoryData categoryData, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _unitOfWork.CategoryData = categoryData;
            //_categoryData = categoryData;
        }

        public async Task<IEnumerable<vm.Category>> GetAll()
        {
            var result = await _unitOfWork.CategoryData.GetAll();
            List<vm.Category> categories = _mapper.Map<List<vm.Category>>(result);
            return categories;
        }

        public async Task AddAsync(vm.Category category)
        {
            dm.Category dmCategory = _mapper.Map<dm.Category>(category);
            await _unitOfWork.CategoryData.AddAsync(dmCategory);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task<vm.Category> GetCategoryAsync(int id)
        {
            var result = await _unitOfWork.CategoryData.GetCategoryAsync(id);
            return _mapper.Map<vm.Category>(result);
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
