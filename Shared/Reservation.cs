using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GzReservation.Shared
{
    public class Reservation
    {
        [Key]
        public int id { get; set; }
        public int doc_no { get; set; }
        public string full_name { get; set; } = string.Empty;
        public string mother_name { get; set; } = string.Empty;
        public DateTime action_date { get; set; }
        public string uuser { get; set; } = string.Empty;
        public bool state { get; set; }
        public DateOnly reservation_date { get; set; }
        public int EntityId { get; set; }
        public Entity? Entity { get; set; }



    }
}
