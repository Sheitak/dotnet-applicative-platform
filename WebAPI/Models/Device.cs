namespace WebAPI.Models
{
    public class Device
    {
        public int DeviceID { get; set; }

        public string MacAddress { get; set; }

        public bool IsActive { get; set; }

        public DateTime RegisteredAt { get; set; }

        public int StudentID { get; set; }

        public Student Student { get; set; }
    }
}