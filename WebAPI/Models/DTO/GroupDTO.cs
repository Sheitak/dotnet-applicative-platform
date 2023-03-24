namespace WebAPI.Models.DTO
{
    public class GroupDTO
    {
        public int GroupID { get; set; }

        public string Name { get; set; }

        public ICollection<Student> Students { get; set; }
    }
}
