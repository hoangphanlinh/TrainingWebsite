using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TrainingWebsite.Models
{
    public class Exam
    {
        [Key]
        public int ID { get; set; }
        public string Name { get; set; }
        public string MetaTitle { get; set; }
        public string Code { get; set; }
        public string QuestionList { get; set; }
        public string AnswertList { get; set; }
        public int CourseID { get; set; }
        public Course course { get; set; }
        public string StartDate { get; set; }
        public string endDate { get; set; }
        public int TotalScore { get; set; }
        public int Time { get; set; }
        public int TotalQuestion { get; set; }
        public string Type { get; set; }
        public char Status { get; set; }
        public string QuestionEssay { get; set; }
        public string UserList { get; set; }
        public string ScoreList { get; set; }
        public List<Result> Results { get; set; }

    }
}
