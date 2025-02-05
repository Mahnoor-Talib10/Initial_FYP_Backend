using Microsoft.AspNetCore.Mvc;
using FYPBackend.Data;
using FYPBackend.Models;

namespace FYPBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PatientsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public PatientsController(AppDbContext context)
        {
            _context = context;
        }
        [HttpPost("signup-p")]
        public async Task<IActionResult> Signup_P([FromBody] Patient patient)
        {
            _context.Patients.Add(patient);
            await _context.SaveChangesAsync();
            return Ok(new { message = "Patient Signed Up Successfully !" });
        }
    }
}
