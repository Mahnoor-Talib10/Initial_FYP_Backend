using Microsoft.AspNetCore.Mvc;
using FYPBackend.Data;
using FYPBackend.Models;
namespace FYPBackend.Controllers
{
    [Route("api/controller")]
    [ApiController]
    public class AppointmentController: ControllerBase
    {
        private readonly AppDbContext _context;
        public AppointmentController(AppDbContext context)
        {
            _context = context;
        }
        [HttpPost("book")]
        public async Task<IActionResult> BookAppointment([FromBody] Appointment appointment)
        {
            _context.Appointments.Add(appointment);
            await _context.SaveChangesAsync();
            return Ok(new { message = "Appointment booked successfully!" });
        }
    }
}
