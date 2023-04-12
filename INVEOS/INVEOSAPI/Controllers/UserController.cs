using INVEOSAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata.Ecma335;

namespace INVEOSAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : Controller
    {
        private readonly InveosContext _context;

        public UserController()
        {
            _context = new InveosContext();
        }

        [Route("GetList")]
        [HttpGet]
        public async Task<IActionResult> GetList()
        {
            try
            {
                IEnumerable<Models.User> users = await _context.Users
                                                .Select(c =>
                                                new Models.User
                                                {
                                                    UserId = c.UserId,
                                                    Username = c.Username,
                                                    Status = c.Status,
                                                    EmployeeId = c.EmployeeId,
                                                    Employee = new Models.Employee
                                                    {
                                                        EmployeeId = c.EmployeeId,
                                                        Role = c.Employee.Role,
                                                        Address = c.Employee.Address,
                                                        FirstName = c.Employee.FirstName,
                                                        FirstLastname = c.Employee.FirstLastname,
                                                        Phone = c.Employee.Phone,
                                                        Email = c.Employee.Email,
                                                        AddressNavigation = c.Employee.AddressNavigation,
                                                        RoleNavigation = c.Employee.RoleNavigation,
                                                    },
                                                }).ToListAsync();


                if (users != null)
                {
                    return Ok(users);
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
        public async Task<IActionResult> Set(InveosModel.User user)
        {
            try
            {
                Models.Address newAddress = new Models.Address
                {
                    Department = user.Employee.AddressNavigation.Department,
                    Municipality = user.Employee.AddressNavigation.Municipality,
                    Zone = user.Employee.AddressNavigation.Zone,
                    AddressDetail = user.Employee.AddressNavigation.AddressDetail
                };

                Models.Employee newEmployee = new Models.Employee
                {
                    FirstName = user.Employee.FirstName,
                    SecondName = user.Employee.SecondName,
                    FirstLastname = user.Employee.FirstLastname,
                    SecondLastname = user.Employee.SecondLastname,
                    Phone = user.Employee.Phone,
                    Email = user.Employee.Email,
                    Gender = user.Employee.Gender,
                    Role = user.Employee.Role,
                    Address = newAddress.AddressId,
                    AddressNavigation = newAddress
                };

                Models.User newUser = new Models.User
                {
                    Username = user.Username,
                    Password = user.Password,
                    Status = user.Status,
                    EmployeeId = newEmployee.EmployeeId,
                    Employee = newEmployee,
                };


                _context.Users.Add(newUser);
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
                if (id == null || _context.Users == null)
                {
                    return NotFound();
                }

                var personalInformation = await _context.Users.FirstOrDefaultAsync(u => u.UserId == id);

                if (personalInformation == null)
                {
                    return NotFound();
                }
                else
                {
                    Models.Employee employee = await _context.Employees.FirstOrDefaultAsync(e => e.EmployeeId == personalInformation.EmployeeId);
                    Models.Address address = await _context.Addresses.FirstOrDefaultAsync(a => a.AddressId == employee.Address);
                    Models.Role role = await _context.Roles.FirstOrDefaultAsync(r => r.RoleId == employee.Role);

                    if (address == null || role == null || employee == null)
                    {
                        return NotFound();
                    }

                    personalInformation.Employee = employee;
                    return Ok(personalInformation);
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
        public async Task<ActionResult> Edit(int id, InveosModel.User user)
        {
            try
            {
                Models.User newUser = new Models.User
                {
                    UserId = user.UserId,
                    Username = user.Username,
                    Password = user.Password,
                    Status = user.Status,
                    EmployeeId = user.EmployeeId,
                    Employee = new Employee {
                        EmployeeId = user.EmployeeId,
                        FirstName = user.Employee.FirstName,
                        SecondName = user.Employee.SecondName,
                        FirstLastname = user.Employee.FirstLastname,
                        SecondLastname = user.Employee.SecondLastname,
                        Phone = user.Employee.Phone,
                        Email = user.Employee.Email,
                        Gender = user.Employee.Gender,
                        Role = user.Employee.Role,
                        Address = user.Employee.Address,
                        AddressNavigation = new Address
                        {
                            AddressId = user.Employee.Address,
                            Department = user.Employee.AddressNavigation.Department,
                            Municipality = user.Employee.AddressNavigation.Municipality,
                            Zone = user.Employee.AddressNavigation.Zone,
                            AddressDetail = user.Employee.AddressNavigation.AddressDetail
                        }
                    }
                };

                _context.Update(newUser);
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
                    return NotFound("Entity set 'InveosContext.Users' is null.");
                }

                var user = await _context.Users.FindAsync(id);

                if (user == null)
                {
                    return NotFound();
                }

                var employee = await _context.Employees.FindAsync(user.EmployeeId);
                var address = await _context.Addresses.FindAsync(employee.Address);
                _context.Employees.Remove(employee);
                _context.Addresses.Remove(address);
                _context.Users.Remove(user);
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
