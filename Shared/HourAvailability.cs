using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GzReservation.Shared
{
    public class HourAvailability
    {
        public string Hour { get; set; } // Changed to string to match your active hour format
        public int AvailableSpots { get; set; }
    }
}
