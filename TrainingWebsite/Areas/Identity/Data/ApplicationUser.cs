using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using TrainingWebsite.Models;

namespace TrainingWebsite.Areas.Identity.Data
{
    public class ApplicationUser : IdentityUser
    {


        
        [PersonalData]
        public string FullName { get; set; }
        [PersonalData]
        public string Address { get; set; }
        [PersonalData]
        public DateTime BirthDate { get; set; }
        [PersonalData]
        public byte[] Image { get; set; }

        [PersonalData]
        [ForeignKey("Occuption")]

        public Nullable<int> OccuptionID { get; set; } = 24;
        public Occuption Occuption { get; set; }

        [PersonalData]
        [ForeignKey("Level")]
        public Nullable<int> LevelID { get; set; } = 9;
        public Level Level { get; set; }



    }
}
