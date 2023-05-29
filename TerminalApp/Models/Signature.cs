namespace TerminalApp.Models
{
    internal class Signature
    {
        public int SignatureID { get; set; }

        public DateTime CreatedAt { get; set; }

        public bool IsPresent { get; set; }

        public int StudentID { get; set; }
    }
}
