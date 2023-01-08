using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TrainingWebsite.Models
{
    public class Question
    {
        [Key]
        public int ID { get; set; }
        public string Name { get; set; }
        public string Content { get; set; }
        public string Answer { get; set; }
        public char Type { get; set; }
        public int CourseID { get; set; }
        public Course course { get; set; }
        public string Status { get; set; }


    }
}
