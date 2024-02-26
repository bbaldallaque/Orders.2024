using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Orders.Backend.Data;
using Orders.Shared.Entities;

namespace Orders.Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CountriesController(OrdersContext context) : ControllerBase
    {
        private readonly OrdersContext _context = context;
      
        [HttpGet("id:int")]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> GetAsync(int? id)
        {
            var country = await _context.Countries.FindAsync(id);

            if (country == null)
            {
                return NotFound(new
                {
                    StatusCode = StatusCodes.Status404NotFound,
                    message = "Record not found"
                });
            }

            try
            {

                return Ok(new
                {                
                    country
                });

            }
            catch (Exception ex)
            {
                return Ok(new
                {
                    StatusCode = StatusCodes.Status500InternalServerError,
                    message = $"Internal Server Error {ex.Message}."
                });
            }
        }

        [HttpGet]
        [ProducesDefaultResponseType]
        public IActionResult Get()
        {
            try
            {
                return Ok(_context.Countries.AsNoTracking());
            }
            catch (Exception ex)
            {
                return Ok(new
                {
                    StatusCode = StatusCodes.Status500InternalServerError,
                    message = $"Internal Server Error {ex.Message}."
                });
            }
        }

        [HttpPost]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> PostAsync(Country country)
        {
            try
            {
                _context.Add(country);
                await _context.SaveChangesAsync();

                return Ok(new
                {
                    StatusCode = StatusCodes.Status201Created,
                    message = "Record Created Successfully",
                    country
                });
            }
            catch (Exception ex)
            {
                return Ok(new
                {
                    StatusCode = StatusCodes.Status500InternalServerError,
                    message = $"Internal Server Error {ex.Message}."
                });
            }
        }

        [HttpPut]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> PutAsync(Country country)
        {
            _context.Update(country);
            await _context.SaveChangesAsync();
            return Ok(new
            {
                StatusCode = StatusCodes.Status204NoContent,
                message = "Record Updated Successfully",
              
            });          
        }

        [HttpDelete("id:int")]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> DeleteAsync(int? id)
        {
            var country = await _context.Countries.FindAsync(id);

            if (country == null)
            {
                return NotFound(new
                {
                    StatusCode = StatusCodes.Status404NotFound,
                    message = "Record not found"
                });
            }

            _context.Countries.Remove(country);
            await _context.SaveChangesAsync();

            return Ok(new
            {
                StatusCode = StatusCodes.Status200OK,
                message = "Record Deleted Successfully!",

            });
        }
    }
}
