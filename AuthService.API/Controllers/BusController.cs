using AuthService.Application.DTOs.Bus;
using AuthService.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AuthService.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BusController : ControllerBase
    {
        private readonly IBusService _busService;

        public BusController(IBusService busService)
        {
            _busService = busService;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetAll()
        {
            var buses = await _busService.GetAllAsync();
            return Ok(buses);
        }

        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetById(Guid id)
        {
            var bus = await _busService.GetByIdAsync(id);
            return Ok(bus);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create([FromBody] CreateBusRequest request)
        {
            var createdBus = await _busService.CreateAsync(request);
            return CreatedAtAction(nameof(GetById), new { id = createdBus.Id }, createdBus);
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _busService.DeleteAsync(id);
            return NoContent();
        }
    }
}
