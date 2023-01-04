using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace TrainingWebsite.Models
{
    public class CourseClassroom
    {
        
        public int courseID { get; set; }
        public  Course course { get; set; }
        public int classID { get; set; }
        public Classroom classroom { get; set; }

    }
}
