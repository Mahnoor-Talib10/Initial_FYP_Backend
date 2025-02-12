using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FYPBackend.Data;
using FYPBackend.Models;
using System.Threading.Tasks;

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
        //patientSignup
        [HttpPost("signup-p")]
        public async Task<IActionResult> Signup_P([FromBody] Patient patient)
        {
            if (await _context.Patients.AnyAsync(p => p.Patient_email == patient.Patient_email))
            {
                return BadRequest(new { message = "Email is already registered!" });
            }

            _context.Patients.Add(patient);
            await _context.SaveChangesAsync();
            return Ok(new { message = "Patient Signed Up Successfully!" });
        }
        //patientLogin
        [HttpPost("login-p")]
        public async Task<IActionResult> Login_P([FromBody] LoginRequestP request)
        {
            if (string.IsNullOrEmpty(request.Patient_email) || string.IsNullOrEmpty(request.Patient_password))
            {
                return BadRequest(new { message = "Email and Password are required!" });
            }

            var patient = await _context.Patients
                .FirstOrDefaultAsync(p => p.Patient_email == request.Patient_email);

            if (patient == null)
            {
                return NotFound(new { message = "Patient not found!" });
            }

            if (patient.Patient_password != request.Patient_password) // Hashing recommended
            {
                return Unauthorized(new { message = "Invalid credentials!" });
            }

            return Ok(new { message = "Login successful!", patientId = patient.Patient_ID });
        }
    }
}
