using AuthService.Application.DTOs.Bus;
using AuthService.Application.Helpers;
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
            var response = ResponseBuilder.Success(buses, "Fetched all buses");
            return StatusCode(response.Status, response);
        }

        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetById(Guid id)
        {
            var bus = await _busService.GetByIdAsync(id);
            var response = ResponseBuilder.Success(bus, "Fetched bus details");
            return StatusCode(response.Status, response);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create([FromBody] CreateBusRequest request)
        {
            var createdBus = await _busService.CreateAsync(request);
            var response = ResponseBuilder.Success(createdBus, "Bus created successfully", 201);
            return StatusCode(response.Status, response);
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _busService.DeleteAsync(id);
            var response = ResponseBuilder.Success<object>(null, "Bus deleted successfully", 200);
            return StatusCode(response.Status, response);
        }
    }
}
