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

        public async Task<IActionResult> GetCategoriesAsync()
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

        [Route("{id}")]
        public async Task<IActionResult> GetCategoryAsync(int id)
        {
            var category = await _categoryProcess.GetCategoryAsync(id);
            if (category == null)
                return NotFound();
            return Ok(category);
        }

        [Route("{id}")]
        [HttpPut]
        public async Task<IActionResult> UpdateCategoryAsync(int id, [FromBody] Category category)
        {
            var result = await _categoryProcess.UpdateAsync(category);
            return Ok("Updated");

        }

        [Route("{id}")]
        [HttpDelete]
        public async Task<IActionResult> DeleteCategoryAsync(int id)
        {
            var result = await _categoryProcess.RemoveCategoryAsync(id);
            return Ok("Deleted");

        }
    }
}


//TODO
// Category: study, office, other
// Tags: dotnet core, mvc, angular, other
// DueDate: today, this week, any specific date
// common paging module
// category section to be appear to admin only
// all sub url of category to be moved to child module
// validations - both client and server side
// automapper
// to do page - add quick and detailed description form (quick will have only one field and detailed page have other fields also)
// dashboard - show no of task added / task completed in last week/month
// add + icon at the bottom of every page. click of + will  open small popup with textbox and dropdown(to do, notes, etc) and submit.
