using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FYPBackend.Models
{
    [Table("Appointments", Schema = "dbo")]
    public class Appointment
    {
        [Key]
        public int AID { get; set; }
        [Required]
        public string Doctor_Name { get; set; } 
        [Required]
        public string Appointment_Day { get; set; }
        [Required]
        public string Appointment_Time { get;  set; }
        [Required]
        public string Patient_Name { get; set; }
        [Required]
        public string Patient_email { get; set; }
        [Required]
        public string Patient_Disease { get; set; }

    }
}
