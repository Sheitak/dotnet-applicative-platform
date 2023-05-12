using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebAPI.Models
{
    public class Signature
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int SignatureID { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime CreatedAt { get; set; }

        [Required]
        public bool IsPresent { get; set; }

        public int StudentID { get; set; }

        public Student Student { get; set; }
    }
}
