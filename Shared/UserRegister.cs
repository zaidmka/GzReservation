using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GzReservation.Shared
{
    public class UserRegister
    {
        [Required, EmailAddress]
        public string Email { get; set; } = string.Empty;
        [Required, StringLength(100, MinimumLength = 6)]
        public string Password { get; set; } = string.Empty;
        [Compare("Password", ErrorMessage = "The Passwords do not Match!")]
        public string ConfirmPassword { get; set; } = string.Empty;
        [Required]
        public string fullname { get; set; } = string.Empty;
        [Required]
        public int EntityId { get; set; }
    }
}
