namespace WebAPI.Models.DTO
{
    public class DeviceDTO
    {
        public int DeviceID { get; set; }

        public string MacAddress { get; set; }

        public bool IsActive { get; set; }

        public int? StudentID { get; set; }

        public StudentDTO? Student { get; set; }
    }
}
