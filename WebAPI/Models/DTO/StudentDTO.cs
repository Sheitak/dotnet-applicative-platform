using System.ComponentModel.DataAnnotations;

namespace WebAPI.Models.DTO
{
    public class StudentDTO
    {
        public int Id { get; set; }

        [Required]
        public string? Firstname { get; set; }

        [Required]
        public string? Lastname { get; set; }

        public Group? Group { get; set; }
    }
}
