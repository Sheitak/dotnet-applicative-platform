using System.Text.Json.Serialization;

namespace WebAPI.Models
{
    public class Group
    {
        public int GroupID { get; set; }

        public string Name { get; set; }

        [JsonIgnore]
        public ICollection<Student> Students { get; set; }
    }
}
