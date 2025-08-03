using System.Security.Claims;
using AuthService.Application.DTOs.Booking;
using AuthService.Application.Helpers;
using AuthService.Application.Interfaces;
using AuthService.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AuthService.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class BookingController : ControllerBase
    {
        private readonly IBookingService _bookingService;

        public BookingController(IBookingService bookingService)
        {
            _bookingService = bookingService;
        }

        private Guid GetUserId() =>
            Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value!);

        [HttpGet]
        public async Task<IActionResult> GetMyBookings()
        {
            var userId = GetUserId();
            var bookings = await _bookingService.GetMyBookingsAsync(userId);
            var response = ResponseBuilder.Success(bookings, "Fetched your bookings");
            return StatusCode(response.Status, response);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var userId = GetUserId();
            var booking = await _bookingService.GetByIdAsync(id, userId);
            var response = ResponseBuilder.Success(booking, "Booking found");
            return StatusCode(response.Status, response);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateBookingRequest request)
        {
            var userId = GetUserId();
            var booking = await _bookingService.CreateAsync(request, userId);
            var response = ResponseBuilder.Success(booking, "Booking confirmed", 201);
            return StatusCode(response.Status, response);
        }

        [HttpPut("{id}/cancel")]
        public async Task<IActionResult> Cancel(Guid id)
        {
            var userId = GetUserId();
            await _bookingService.CancelAsync(id, userId);
            var response = ResponseBuilder.Success<object>(null, "Booking cancelled");
            return StatusCode(response.Status, response);
        }
    }
}
