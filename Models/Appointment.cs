using System;
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
        public string doctor_ID { get; set; } 
        [Required]
        public string Appointment_Day { get; set; }
        [Required]
        public TimeSpan Appointment_Start_Time { get;  set; }
        [Required]
        public TimeSpan Appointment_End_Time { get; set; }
        [Required]
        public string patient_ID { get; set; }
        [Required]
        public string Patient_Disease { get; set; }

    }
}
