using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TFG.Assessment.Domain.Entities;
using TFG.Assessment.Domain.Interfaces;
using TFG.Assessment.Domain.Requests;

namespace TFG.Assessment.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        public CustomersController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var customers = await _unitOfWork.Customers.GetAll();

            return Ok(customers);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var customers = await _unitOfWork.Customers.GetById(id);

            return Ok(customers);
        }

        [HttpGet("search")]
        public async Task<IActionResult> Search(string searchTerm)
        {
            var searchTermToLower = searchTerm.ToLower();
            var customers = await _unitOfWork.Customers.Find(x => x.FirstName.ToLower().Contains(searchTermToLower) 
                                                               || x.LastName.Contains(searchTermToLower));

            return Ok(customers);
        }

        [HttpPost]
        [Authorize(Roles = "User, Admin")]
        public async Task<IActionResult> Add([FromBody] AddCustomer customer)
        {
            if (customer == null)
            {
                return BadRequest();
            }
            await _unitOfWork.Customers.Add(new Customer
            {
                FirstName = customer.FirstName,
                LastName = customer.LastName,
            });

            await _unitOfWork.Complete();

            return Ok();
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "User, Admin")]
        public async Task<IActionResult> Update([FromBody] UpdateCustomer customer, int id)
        {
            if (customer == null)
            {
                return BadRequest();
            }
            _unitOfWork.Customers.Update(new Customer
            {
                FirstName = customer.FirstName,
                LastName = customer.LastName,
                Id = id
            });

            await _unitOfWork.Complete();


            return Ok();
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            var customer = await _unitOfWork.Customers.GetById(id);
            if (customer != null)
            {
                _unitOfWork.Customers.Remove(customer);
                await _unitOfWork.Complete();

                return Ok();
            }
            else
            {
                return NoContent();
            }
        }
    }
}
