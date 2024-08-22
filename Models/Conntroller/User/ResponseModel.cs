
using System.ComponentModel.DataAnnotations;

namespace Myproject.Models.Controller.Controllser
{
    public class CreateUserResponse
    {
        public string Token { get; set; }
        public DateTime ExpireTimeToken { get; set; }
    }

    public class UserInfoResponse
    {
        public string Username { get; set; }
        public string Email { get; set; }
    }
}