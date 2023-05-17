using System.ComponentModel.DataAnnotations;

namespace WebAPI.Models.DTO
{
    public class StudentDTO
    {
        public int StudentID { get; set; }

        [Required]
        public string Firstname { get; set; }

        [Required]
        public string Lastname { get; set; }

        public int? GroupID { get; set; }

        public GroupDTO? Group { get; set; }

        public int? PromotionID { get; set; }

        public PromotionDTO? Promotion { get; set; }
    }
}
