using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GzReservation.Shared
{
    public class UserEntity
    {
        public int Id { get; set; }
        public string Email { get; set; } = string.Empty;

        public byte[] PasswordHash { get; set; }

        public byte[] PasswordSalt { get; set; }
        public DateTime DataCreated { get; set; } = DateTime.UtcNow;

        public string Role { get; set; } = "User";
        public string fullname { get; set; } = string.Empty;
        public string new_user { get; set; } = "yes";
        public int EntityId { get; set; }

        public Entity? Entity { get; set; }
    }
}
