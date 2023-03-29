using INVEOSAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace INVEOSAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductController : Controller
    {
        private readonly InveosContext _context;

        public ProductController()
        {
            _context = new InveosContext();
        }

        [Route("GetList")]
        [HttpGet]
        public async Task<IActionResult> GetList()
        {
            try
            {
                IEnumerable<Models.Product> products = await _context.Products
                                                .Select(c =>
                                                new Models.Product
                                                {
                                                    Idproduct = c.Idproduct,
                                                    CategoryId = c.CategoryId,
                                                    Name = c.Name,
                                                    Code = c.Code,
                                                    Cost = c.Cost,
                                                    Price = c.Price,
                                                    Quantity = c.Quantity,
                                                    Description = c.Description,
                                                    Status = c.Status,
                                                    SuplierId = c.SuplierId,
                                                    Image = c.Image,
                                                    Category = new Category
                                                    {
                                                        Name = c.Category.Name,
                                                        Code = c.Category.Code
                                                    },
                                                    Suplier = new Supplier
                                                    {
                                                        Name = c.Suplier.Name
                                                    }
                                                }).ToListAsync();


                if (products != null)
                {
                    return Ok(products);
                }

                return Ok(null);
            }
            catch (Exception)
            {

                return BadRequest();
                throw;
            }

        }

    }
}
