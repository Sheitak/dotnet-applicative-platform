using System.ComponentModel.DataAnnotations;

namespace WebAPI.Models.DTO
{
    public class SignatureDTO
    {
        public int SignatureID { get; set; }

        [Required]
        public DateTime CreatedAt { get; set; }

        [Required]
        public bool IsPresent { get; set; }

        public int StudentID { get; set; }

        public Student Student { get; set; }
    }
}
