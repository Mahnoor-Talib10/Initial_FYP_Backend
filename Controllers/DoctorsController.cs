using Microsoft.AspNetCore.Mvc;
using FYPBackend.Data;
using FYPBackend.Models;
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
            return Ok(new { message = "Doctor Signed Up Successfully!"});
        }

    }
}
