﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BLL.Contracts;
using Microsoft.AspNetCore.Authorization;
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
        [Authorize]
        public async Task<IActionResult> AddCategoryAsync([FromBody]Category category)
        {
            await _categoryProcess.AddAsync(category);
            return Ok(new { message = "Category added successfully"});
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
        [Authorize]
        public async Task<IActionResult> UpdateCategoryAsync(int id, [FromBody] Category category)
        {
            var result = await _categoryProcess.UpdateAsync(category);
            return Ok(new { message = "Category updated successfully" });

        }

        [Route("{id}")]
        [HttpDelete]
        [Authorize]
        public async Task<IActionResult> DeleteCategoryAsync(int id)
        {
            var result = await _categoryProcess.RemoveCategoryAsync(id);
            return Ok(new { message = "Category deleted successfully" });

        }
    }
}



