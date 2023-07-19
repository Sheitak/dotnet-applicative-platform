using WebAPI.Models.DTO;

namespace WebAPI.Models
{
    public class AuthenticationResponse
    {
        public string Token { get; set; }
        public DateTime Expiration { get; set; }
        public UserDTO User { get; set; }
    }
}
