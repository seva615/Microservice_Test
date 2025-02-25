using User.API.Data;
using User.Data;

namespace User.Service.Models
{
    public class UserModel
    {
        public Guid Id { get; set; }

        public UserRoles.Roles Role { get; set; }

        public string? Name { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

    }
}
