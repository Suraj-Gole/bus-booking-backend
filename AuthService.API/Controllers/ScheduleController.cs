using AuthService.Application.DTOs.Schedule;
using AuthService.Application.Helpers;
using AuthService.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AuthService.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ScheduleController : ControllerBase
    {
        private readonly IScheduleService _scheduleService;

        public ScheduleController(IScheduleService scheduleService)
        {
            _scheduleService = scheduleService;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetAll()
        {
            var schedules = await _scheduleService.GetAllAsync();
            var response = ResponseBuilder.Success(schedules, "Fetched all schedules");
            return StatusCode(response.Status, response);
        }

        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetById(Guid id)
        {
            var schedule = await _scheduleService.GetByIdAsync(id);
            var response = ResponseBuilder.Success(schedule, "Fetched schedule details");
            return StatusCode(response.Status, response);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create([FromBody] CreateScheduleRequest request)
        {
            var schedule = await _scheduleService.CreateAsync(request);
            var response = ResponseBuilder.Success(schedule, "Schedule created", 201);
            return StatusCode(response.Status, response);
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _scheduleService.DeleteAsync(id);
            var response = ResponseBuilder.Success<object>(null, "Schedule deleted");
            return StatusCode(response.Status, response);
        }
    }
}