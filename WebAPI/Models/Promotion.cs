namespace WebAPI.Models
{
    public class Promotion
    {
        public int PromotionID { get; set; }

        public string Name { get; set; }

        public ICollection<Student> Students { get; set; }
    }
}
