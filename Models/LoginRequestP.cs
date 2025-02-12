using System.ComponentModel.DataAnnotations;
namespace FYPBackend.Models
{
    public class LoginRequestP
    {
        [Required]
        public string Patient_email { get; set; }
        [Required]
        public string Patient_password { get; set; }
    }
}
