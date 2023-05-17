namespace DesktopApp.Models
{
    internal class Student
    {
        public int StudentID { get; set; }

        public string? Firstname { get; set; }

        public string? Lastname { get; set; }

        public int? GroupID { get; set; }

        public Group? Group { get; set; }

        public int? PromotionID { get; set; }

        public Promotion? Promotion { get; set; }
    }
}
