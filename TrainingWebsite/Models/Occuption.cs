using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using TrainingWebsite.Areas.Identity.Data;

namespace TrainingWebsite.Models
{
    public class Occuption
    {
       
        [Key]
        public int OccuptionID { get; set; }
        public string OccuptionName { get; set; }

        [ForeignKey("Apartment")]
        public int ApartmentID { get; set; }
        public Apartment Apartment { get; set; }
        public ICollection<ApplicationUser> AspNetUsers { get; set; }




    }
}
