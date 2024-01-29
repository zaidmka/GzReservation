using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GzReservation.Shared
{
    public class PreReservation
    {
        public int doc_no { get; set; }
        public string full_name { get; set; } = string.Empty;
        public string mother_name { get; set; } = string.Empty;
        public int EntityId { get; set; }
        public Entity? Entity { get; set; }
    }
}
