using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using TrainingWebsite.Areas.Identity.Data;

namespace TrainingWebsite.Models
{
    public class Classroom
    {
      
        [Key]
        public int classID { get; set; }
        public string Name { get; set; }
        [DataType(DataType.Date)]
        public string startDate { get; set; }
        [DataType(DataType.Date)]

        public string endDate { get; set; }
        public string Image { get; set; }
        [ForeignKey("Admin")]
        public string AdminID { get; set; }
        public ApplicationUser Admin { get; set; }

        public List<CourseClassroom> CourseClassroom { get; set; }


    }
}
