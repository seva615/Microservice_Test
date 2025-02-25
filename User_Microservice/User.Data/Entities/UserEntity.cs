using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace User.Data.Entities
{
    public class UserEntity : IEntity
    {
        public Guid Id { get; set; }

        public UserRoles.Roles Role { get; set; }

        public string? Name { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

    }
}
