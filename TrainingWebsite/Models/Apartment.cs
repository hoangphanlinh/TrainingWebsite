using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TrainingWebsite.Models
{
    public class Apartment
    {
        [Key]
        public int ApartmentID { get; set; }
        public string ApartmentName { get; set; }
        public ICollection<Occuption> Occuptions { get; set; }

    }
}
