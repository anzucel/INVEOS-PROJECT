using INVEOSAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace INVEOSAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CategoryController : Controller
    {
        private readonly InveosContext _context;

        public CategoryController()
        {
            _context = new InveosContext();
        }

        [Route("GetList")]
        [HttpGet]
        public async Task<IActionResult> GetList()
        {
            try
            {
                IEnumerable<Models.Category> categories = await _context.Categories
                                                .Select(c =>
                                                new Models.Category
                                                {
                                                    CategoryId = c.CategoryId,
                                                    Name = c.Name,
                                                    Code = c.Code,
                                                    Description = c.Description,
                                                    Image = c.Image,
                                                }).ToListAsync();


                if (categories != null)
                {
                    return Ok(categories);
                }

                return Ok(null);
            }
            catch (Exception)
            {

                return BadRequest();
                throw;
            }

        }

        [Route("Set")]
        [HttpPost]
        public async Task<IActionResult> Set(InveosModel.Category category)
        {
            try
            {
                Models.Category newCategory = new Models.Category
                {
                    Name = category.Name,
                    Code = category.Code,
                    Description = category.Description,
                    Image = category.Image,
                };

                _context.Categories.Add(newCategory);
                await _context.SaveChangesAsync();
                return Ok();
            }

            catch (Exception)
            {
                return BadRequest();
                throw;
            }
        }

        [Route("GetByID/{id}")]
        [HttpGet]
        public async Task<ActionResult> GetByID(int id)
        {
            try
            {
                if (id == null || _context.Categories == null)
                {
                    return NotFound();
                }

                var category = await _context.Categories.FirstOrDefaultAsync(c => c.CategoryId == id);

                if (category == null)
                {
                    return NotFound();
                }
                else
                {
                    return Ok(category);
                }
            }
            catch (Exception)
            {
                return BadRequest();
                throw;
            }
        }

        [Route("Edit/{id}")]
        [HttpPut]
        public async Task<ActionResult> Edit(int id, InveosModel.Category category)
        {
            try
            {
                Models.Category newCategory = new Models.Category
                {
                    CategoryId = category.CategoryId,
                    Name = category.Name,
                    Code = category.Code,
                    Description = category.Description,
                    Image = category.Image,
                };

                _context.Update(newCategory);
                await _context.SaveChangesAsync();
                return Ok();
            }
            catch (DbUpdateConcurrencyException)
            {
                return BadRequest();
            }
        }

        [Route("Delete/{id}")]
        [HttpDelete]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                if (_context.Users == null)
                {
                    return NotFound("Entity set 'InveosContext.Category' is null.");
                }

                var category = await _context.Categories.FindAsync(id);

                if (category == null)
                {
                    return NotFound();
                }

                _context.Categories.Remove(category);
                await _context.SaveChangesAsync();
                return Ok();
            }
            catch (Exception)
            {
                BadRequest();
                throw;
            }
        }
    }
}
