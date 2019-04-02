using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BLL.Contracts;
using Microsoft.AspNetCore.Mvc;
using ViewModel;

namespace WebApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryProcess _categoryProcess;

        public CategoryController(ICategoryProcess categoryProcess)
        {
            _categoryProcess = categoryProcess;
        }

        public async Task<IActionResult> GetCategoryAsync()
        {
            var result = await _categoryProcess.GetAll();
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> AddCategoryAsync([FromBody]Category category)
        {
            await _categoryProcess.AddAsync(category);
            return Ok("Success");
        }
    }
}


//TODO
// Category: study, office, other
// Tags: dotnet core, mvc, angular, other
// DueDate: today, this week, any specific date