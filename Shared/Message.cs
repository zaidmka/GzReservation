using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GzReservation.Shared
{
	public class Message
	{
		[Key]
		public int id { get; set; }
        public int template { get; set; }
        public int doc_no { get; set; }
        public long phone_number { get; set; }
        public string value1 { get; set; }=string.Empty;
        public string value2 { get; set; }=string.Empty;
        public string value3 { get; set; } =string.Empty;
        public string value4 { get; set; } = string.Empty;
        public DateTime action_date { get; set; }

    }
}
