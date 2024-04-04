using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GzReservation.Shared
{
	public class PreMessageReservation
	{
        public int doc_no { get; set; }
        public string reservationDate { get; set; }=string.Empty;
        public string reservationHour { get; set; } = string.Empty;
    }
}
