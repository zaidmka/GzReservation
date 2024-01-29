using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GzReservation.Shared
{
    public class Entity
    {
        [Key]
        public int id { get; set; }
        public string entity_name { get; set; } = string.Empty;
        public string password { get; set; } = string.Empty;
        public int max_day { get; set; }
        public int max_week { get; set; }

    }
}
