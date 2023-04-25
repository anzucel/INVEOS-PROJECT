using INVEOSAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace INVEOSAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SupplierController : Controller
    {
        private readonly InveosContext _context;

        public SupplierController()
        {
            _context = new InveosContext();
        }

        [Route("GetList")]
        [HttpGet]
        public async Task<IActionResult> GetList()
        {
            try
            {
                IEnumerable<Models.Supplier> suppliers = await _context.Suppliers
                                                .Select(c =>
                                                new Models.Supplier
                                                {
                                                    SupplierId = c.SupplierId,
                                                    Name = c.Name,
                                                    Email = c.Email,
                                                    Phone = c.Phone,
                                                    Address = c.Address
                                                }).ToListAsync();


                if (suppliers != null)
                {
                    return Ok(suppliers);
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
        public async Task<IActionResult> Set(InveosModel.Supplier supplier)
        {
            try
            {
                Models.Address newAddress = new Models.Address
                {
                    Department = supplier.Address.Department,
                    Municipality = supplier.Address.Municipality,
                    Zone = supplier.Address.Zone,
                    AddressDetail = supplier.Address.AddressDetail
                };

                Models.Supplier newSupplier = new Models.Supplier
                {
                    Name = supplier.Name,
                    Email = supplier.Email,
                    Phone = supplier.Phone,
                    Address = newAddress,
                };

                _context.Suppliers.Add(newSupplier);
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
                if (id == null || _context.Suppliers == null)
                {
                    return NotFound();
                }

                var supplier = await _context.Suppliers.FirstOrDefaultAsync(s => s.SupplierId == id);

                if (supplier == null)
                {
                    return NotFound();
                }
                else
                {
                    Models.Address address = await _context.Addresses.FirstOrDefaultAsync(a => a.AddressId == supplier.AddressId);

                    if (address == null)
                    {
                        return NotFound();
                    }

                    supplier.Address = address;
                    return Ok(supplier);
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
        public async Task<ActionResult> Edit(int id, InveosModel.Supplier supplier)
        {
            try
            {
                Models.Supplier newSupplier = new Models.Supplier
                {
                    SupplierId = supplier.SupplierId,
                    Name = supplier.Name,
                    Email = supplier.Email,
                    Phone = supplier.Phone,
                    Address = new Address
                    {
                        AddressId = supplier.AddressId,
                        Department = supplier.Address.Department,
                        Municipality = supplier.Address.Municipality,
                        Zone = supplier.Address.Zone,
                        AddressDetail = supplier.Address.AddressDetail
                    }
                };

                _context.Update(newSupplier);
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
                if (_context.Suppliers == null)
                {
                    return NotFound("Entity set 'InveosContext.Supplier' is null.");
                }

                var supplier = await _context.Suppliers.FindAsync(id);

                if (supplier == null)
                {
                    return NotFound();
                }

                var address = await _context.Addresses.FindAsync(supplier.AddressId);
                _context.Suppliers.Remove(supplier);
                _context.Addresses.Remove(address);
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
