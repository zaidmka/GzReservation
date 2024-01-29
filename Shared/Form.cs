using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace GzReservation.Shared
{
    public class Form
    {
        [Key]
        public int id { get; set; }

         public Guid UUID { get; set; }


        [Required(ErrorMessage = "DID is required")]
        public int did { get; set; }

        [Required(ErrorMessage = "Document number is required")]
        public int? doc_no { get; set; }

        [Required(ErrorMessage = "id number is required")]
        public long? id_number { get; set; }

        [Required(ErrorMessage = "Name is required")]
        public string name { get; set; } = string.Empty;

        [Required(ErrorMessage = "Entity is required")]
        public string entity { get; set; } = string.Empty;

        [Required(ErrorMessage = "Father's work is required")]
        public string father_work { get; set; } = string.Empty;

        [Required(ErrorMessage = "Mother's name is required")]
        public string mother_name { get; set; } = string.Empty;

        [Required(ErrorMessage = "Mother's work is required")]
        public string mother_work { get; set; } = string.Empty;

        [Required(ErrorMessage = "Wife's name is required")]
        public string wife_name { get; set; } = string.Empty;

        [Required(ErrorMessage = "Wife's work is required")]
        public string wife_work { get; set; } = string.Empty;

        [Required(ErrorMessage = "Badge color is required")]
        public string bagde_color { get; set; } = string.Empty;

        public DateTime actiondate { get; set; }

        [Required(ErrorMessage = "Username is required")]
        public string username { get; set; } = string.Empty;

        [Required(ErrorMessage = "Info book is required")]
        public int? info_book { get; set; }

        [Required(ErrorMessage = "Sequence is required")]
        public int? seq { get; set; }
        [Required(ErrorMessage = "review_date is required")]

        public DateOnly review_date { get; set; }
        [Required(ErrorMessage = "birthdate is required")]

        public DateOnly birthdate { get; set; }

        [Required(ErrorMessage = "State is required")]
        public string state { get; set; } = string.Empty;

        [Required(ErrorMessage = "Nationalism is required")]
        public string nationalism { get; set; } = string.Empty;

        [Required(ErrorMessage = "Religion is required")]
        public string religion { get; set; } = string.Empty;

        [Required(ErrorMessage = "Place govern is required")]
        public string place_govern { get; set; } = string.Empty;

        [Required(ErrorMessage = "City is required")]
        public string place_city { get; set; } = string.Empty;

        [Required(ErrorMessage = "Mhala is required")]
        public string place_mhala { get; set; } = string.Empty;

        [Required(ErrorMessage = "Zuqaq is required")]
        public string place_zuqaq { get; set; } = string.Empty;

        [Required(ErrorMessage = "Dar is required")]
        public string place_dar { get; set; } = string.Empty;

        [Required(ErrorMessage = "Point is required")]
        public string place_point { get; set; } = string.Empty;

        [Required(ErrorMessage = "Phone 1 is required")]
        public long? phone1 { get; set; }

        [Required(ErrorMessage = "Phone 2 is required")]
        public string phone2 { get; set; }

        [Required(ErrorMessage = "Work place is required")]
        public string work_place { get; set; } = string.Empty;

        [Required(ErrorMessage = "Second work place is required")]
        public string work_place2 { get; set; } = string.Empty;

        [Required(ErrorMessage = "Study is required")]
        public string study { get; set; } = string.Empty;

        [Required(ErrorMessage = "grad_year is required")]

        public DateOnly grad_year { get; set; }

        [Required(ErrorMessage = "old_place is required")]

        public string old_place { get; set; }=string.Empty;
        public int? passport_no { get; set; }

        [Required(ErrorMessage = "a1 is required")]
        public string a1 { get; set; } = string.Empty;

        [Required(ErrorMessage = "a2 is required")]
        public string a2 { get; set; } = string.Empty;

        [Required(ErrorMessage = "a3 is required")]
        public string a3 { get; set; } = string.Empty;

        [Required(ErrorMessage = "a4 is required")]
        public string a4 { get; set; } = string.Empty;

        [Required(ErrorMessage = "a5 is required")]
        public string a5 { get; set; } = string.Empty;

        [Required(ErrorMessage = "a6 is required")]
        public string a6 { get; set; } = string.Empty;

        [Required(ErrorMessage = "a7 is required")]
        public string a7 { get; set; } = string.Empty;

        [Required(ErrorMessage = "a8 is required")]
        public string a8 { get; set; } = string.Empty;

        [Required(ErrorMessage = "a9 is required")]
        public string a9 { get; set; } = string.Empty;

        [Required(ErrorMessage = "a10 is required")]
        public string a10 { get; set; } = string.Empty;

        [Required(ErrorMessage = "a11 is required")]
        public string a11 { get; set; } = string.Empty;

        [Required(ErrorMessage = "a12 is required")]
        public string a12 { get; set; } = string.Empty;

        [Required(ErrorMessage = "a13 is required")]
        public string a13 { get; set; } = string.Empty;

        [Required(ErrorMessage = "a14 is required")]
        public string a14 { get; set; } = string.Empty;

        [Required(ErrorMessage = "a15 is required")]
        public string a15 { get; set; } = string.Empty;

        [Required(ErrorMessage = "a16 is required")]
        public string a16 { get; set; } = string.Empty;

        [Required(ErrorMessage = "a17 is required")]
        public string a17 { get; set; } = string.Empty;

        public bool deleted { get; set; } = false;
    }
}
