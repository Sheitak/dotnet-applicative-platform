namespace WebAppMVC.Models
{
    public class Auth
    {
        public required string UserName { get; set; }
        public required string Token { get; set; }
        public DateTime Expiration { get; set; }
        public User? User { get; set; }
    }
}
