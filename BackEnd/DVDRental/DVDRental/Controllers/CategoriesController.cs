using DVDRental.DTOs.RequestDTO;
using DVDRental.Entities;
using DVDRental.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DVDRental.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {

        private readonly IAdminCategoriesService _categoryService;

        public CategoriesController(IAdminCategoriesService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var categories = await _categoryService.GetAllAsync();
            return Ok(categories);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var category = await _categoryService.GetByIdAsync(id);
            if (category == null)
            {
                return NotFound();
            }
            return Ok(category);
        }

        [HttpPost]
        public async Task<IActionResult> Add(CategoryRequestDTO category)
        {
            await _categoryService.AddAsync(category);
            return CreatedAtAction(nameof(GetById), new { Name = category.Name }, category);
        }

       /* [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, Categories category)
        {
            if (id != category.CategoryID)
            {
                return BadRequest();
            }

            await _categoryService.UpdateAsync(category);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _categoryService.DeleteAsync(id);
            return NoContent();
        }*/

    }
}
