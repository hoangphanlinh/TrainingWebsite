using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using TrainingWebsite.Areas.Identity.Data;

namespace TrainingWebsite.Models
{
    public class Level
    {
        
        [Key]
        public int ID { get; set; }
        public string LevelName { get; set; }
        public ICollection<ApplicationUser> AspNetUsers { get; set; }


    }
   

}
