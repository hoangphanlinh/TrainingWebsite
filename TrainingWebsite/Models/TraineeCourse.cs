using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TrainingWebsite.Areas.Identity.Data;

namespace TrainingWebsite.Models
{
    public class TraineeCourse
    {
        public int CourseID { get; set; }
        public Course course { get; set; }
        public string TraineeID { get; set; }
        public ApplicationUser trainee { get; set; }
        public string TraineeLevel { get; set; }
        public string EnrollDate { get; set; }
        public string Status { get; set; }
        public string Title { get; set; }
    }
}
