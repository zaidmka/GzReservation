using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GzReservation.Shared
{
	public class UserEntityChangeDetails
	{
		public string Email { get; set; } = string.Empty;
		public string fullname { get; set; } = string.Empty;
		public int EntityId { get; set; }
		public string state { get; set; } = "no";
    }
}
