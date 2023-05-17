using System.Text.Json.Serialization;

namespace WebAPI.Models
{
    public class Promotion
    {
        public int PromotionID { get; set; }

        public string Name { get; set; }

        [JsonIgnore]
        public ICollection<Student> Students { get; set; }
    }
}
