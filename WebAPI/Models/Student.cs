using System.ComponentModel.DataAnnotations;

namespace WebAPI.Models
{
    public class Student
    {
        public int StudentID { get; set; }

        [Required]
        public string Firstname { get; set; }

        [Required]
        public string Lastname { get; set; }

        public bool? IsActive { get; set; }

        public string? MacAdress { get; set; }

        public int? GroupID { get; set; }

        public Group? Group { get; set; }

        public int? PromotionID { get; set; }

        public Promotion? Promotion { get; set; }

        public ICollection<Signature> Signatures { get; set; }
    }
}
