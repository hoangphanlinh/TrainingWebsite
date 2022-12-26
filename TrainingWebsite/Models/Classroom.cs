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
        public int ID { get; set; }
        public string Name { get; set; }
        [DataType(DataType.DateTime)]
        public string startDate { get; set; }
        public string endDate { get; set; }
        [ForeignKey("course")]
        public int CourseID { get; set; }
        public Course course { get; set; }
        [ForeignKey("Admin")]
        public string AdminID { get; set; }
        public ApplicationUser Admin { get; set; }

    }
}
