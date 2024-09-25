using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GzReservation.Shared
{
    public class Dbmessage
    {
        public int id { get; set; }
        public string message { get; set; }
        public bool visible { get; set; }
        public string type { get; set; }
        public string title { get; set; }
    }
}
