using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FYPBackend.Models
{
    [Table("Patient_Signup", Schema = "dbo")]
    public class Patient
    {
        [Key]
        public int Patient_ID {  get; set; }
        [Required]
        public string Patient_FName { get; set; }

        [Required]
        public string Patient_LName { get;  set; }

        [Required]
        public string Patient_email { get; set; }

        [Required]
        public string Patient_password { get; set; }

    }
}
