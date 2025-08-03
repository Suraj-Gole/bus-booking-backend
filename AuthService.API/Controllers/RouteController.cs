using AuthService.Application.DTOs.Route;
using AuthService.Application.Helpers;
using AuthService.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AuthService.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RouteController : ControllerBase
    {
        private readonly IRouteService _routeService;

        public RouteController(IRouteService routeService)
        {
            _routeService = routeService;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetAll()
        {
            var routes = await _routeService.GetAllAsync();
            var response = ResponseBuilder.Success(routes, "Fetched all routes");
            return StatusCode(response.Status, response);
        }

        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetById(Guid id)
        {
            var route = await _routeService.GetByIdAsync(id);
            var response = ResponseBuilder.Success(route, "Fetched route details");
            return StatusCode(response.Status, response);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create([FromBody] CreateRouteRequest request)
        {
            var route = await _routeService.CreateAsync(request);
            var response = ResponseBuilder.Success(route, "Route created successfully", 201);
            return StatusCode(response.Status, response);
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _routeService.DeleteAsync(id);
            var response = ResponseBuilder.Success<object>(null, "Route deleted");
            return StatusCode(response.Status, response);
        }
    }
}
