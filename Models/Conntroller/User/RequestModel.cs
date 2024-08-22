
namespace Myproject.Models.Controller.User
{
    public class CreateUser
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
    }

    public class ModifyUser
    {
        public string Email { get; set; }
    }
}