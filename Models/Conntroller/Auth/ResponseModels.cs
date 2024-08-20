
namespace Myproject.Models.Controller.Auth
{
    public class LoginResponse
    {
        public string Token { get; set; }
        public DateTime ExpireTimeToken { get; set; }
    }
}