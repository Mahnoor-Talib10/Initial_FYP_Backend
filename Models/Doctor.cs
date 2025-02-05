using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FYPBackend.Models
{
    [Table("Doctor_Signup", Schema = "dbo")]
    public class Doctor
    {
        [Key]
        public int Doctor_ID { get; set; }
        [Required]
        public string Doctor_Username { get; set; }
        [Required]
        public string Doctor_email { get; set; }
        [Required]
        public string Doctor_password { get; set; }
        [Required]
        public string Doctor_city { get; set; }
        [Required]
        public string Doctor_Speciality { get;   set; }
        [Required]
        public string Doctor_Qualification { get; set; }
        [Required]
        public string Doctor_Clinic_Name { get; set; }  
    }
}
