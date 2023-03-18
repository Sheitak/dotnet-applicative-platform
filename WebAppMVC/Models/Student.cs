namespace WebAppMVC.Models
{
    public class Student
    {
        public int Id { get; set; }

        public string? Firstname { get; set; }

        public string? Lastname { get; set; }

        public Group? Group { get; set; }

        public List<Signature>? Signatures { get; set; }
    }
}
