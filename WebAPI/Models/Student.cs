using System.ComponentModel.DataAnnotations;

namespace WebAPI.Models
{
    public class Student
    {
        public int Id { get; set; }

        [Required]
        public string? Firstname { get; set; }

        [Required]
        public string? Lastname { get; set; }

        public Group? Group { get; set; }

        public List<Signature>? Signatures { get; set; }
    }
}
