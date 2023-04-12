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
                                                    Category = c.Category,
                                                    Suplier = c.Suplier//new Category
                                                    //{
                                                    //    Name = c.Category.Name,
                                                    //    Code = c.Category.Code
                                                    //},
                                                    //Suplier = new Supplier
                                                    //{
                                                    //    Name = c.Suplier.Name
                                                    //}
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


        [Route("Set")]
        [HttpPost]
        public async Task<IActionResult> Set(InveosModel.Product product)
        {
            try
            {
                Models.Product newProduct = new Models.Product
                {
                    CategoryId = product.CategoryId,
                    Name = product.Name,
                    Code = product.Code,
                    Cost = product.Cost,
                    Price = product.Price,
                    Quantity = product.Quantity,
                    Description = product.Description,
                    Status = product.Status,
                    SuplierId = product.SuplierId,
                    Image = product.Image
                };

                _context.Products.Add(newProduct);
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
                if (id == null || _context.Products == null)
                {
                    return NotFound();
                }

                var product = await _context.Products.FirstOrDefaultAsync(p => p.Idproduct == id);

                if (product == null)
                {
                    return NotFound();
                }
                else
                {
                    Models.Category category = await _context.Categories.FirstOrDefaultAsync(c => c.CategoryId == product.CategoryId);
                    Models.Supplier supplier = await _context.Suppliers.FirstOrDefaultAsync(s => s.SupplierId == product.SuplierId);

                    if (category == null || supplier == null)
                    {
                        return NotFound();
                    }

                    return Ok(product);
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
        public async Task<ActionResult> Edit(int id, InveosModel.Product product)
        {
            try
            {
                Models.Product newProduct = new Models.Product
                {
                    Idproduct = product.Idproduct,
                    CategoryId = product.CategoryId,
                    Name = product.Name,
                    Code = product.Code,
                    Cost = product.Cost,
                    Price = product.Price,
                    Quantity = product.Quantity,
                    Description = product.Description,
                    Status = product.Status,
                    SuplierId = product.SuplierId,
                    Image = product.Image
                };

                _context.Update(newProduct);
                await _context.SaveChangesAsync();
                return Ok();
            }
            catch (DbUpdateConcurrencyException)
            {
                return BadRequest();
            }
        }
    }
}
