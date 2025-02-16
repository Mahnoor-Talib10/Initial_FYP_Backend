using Microsoft.AspNetCore.Mvc;
using FYPBackend.Data;
using FYPBackend.Models;
using Microsoft.EntityFrameworkCore;
namespace FYPBackend.Controllers
{
    [Route("api/[controller]")]
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

        [HttpGet("booked-slots")]
        public async Task<IActionResult> GetBookedSlots([FromQuery] string doctor_ID, [FromQuery] string day)
        {
            if (string.IsNullOrEmpty(doctor_ID) || string.IsNullOrEmpty(day))
            {
                return BadRequest(new { message = "Doctor ID and day are required." });
            }

            var bookedSlots = await _context.Appointments
        .Where(a => a.doctor_ID == doctor_ID && a.Appointment_Day == day)
        .Select(a => new
        {
            StartTime = a.Appointment_Start_Time != null
                ? $"{a.Appointment_Start_Time.Hours:D2}:{a.Appointment_Start_Time.Minutes:D2}"
                : null,
            EndTime = a.Appointment_End_Time != null
                ? $"{a.Appointment_End_Time.Hours:D2}:{a.Appointment_End_Time.Minutes:D2}"
                : null
        })
        .ToListAsync();

            return Ok(bookedSlots);
        }

    }
}
