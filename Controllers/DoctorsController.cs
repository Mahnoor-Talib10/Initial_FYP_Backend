using Microsoft.AspNetCore.Mvc;
using FYPBackend.Data;
using FYPBackend.Models;
using Microsoft.EntityFrameworkCore;
namespace FYPBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DoctorsController:ControllerBase
    {
        private readonly AppDbContext _context;
        public DoctorsController (AppDbContext context)
        {
            _context = context;
        }
        [HttpPost ("Signup-d")] public async Task<IActionResult> Signup_D([FromBody] Doctor doctor)
        {
            _context.Doctors.Add(doctor);
            await _context.SaveChangesAsync();
            return Ok(new { message = "Doctor Signed Up Successfully!" });
        }


        [HttpGet("GetAllDoctors")]
        public async Task<ActionResult<IEnumerable<Doctor>>> GetAllDoctors()
        {
            var doctors = await _context.Doctors.ToListAsync();

            if (doctors == null || doctors.Count == 0)
                return NotFound("No doctors found.");

            return Ok(doctors);  // Return the list of doctors
        }
    }
}
