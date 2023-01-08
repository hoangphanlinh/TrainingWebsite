using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TrainingWebsite.Areas.Identity.Data;

namespace TrainingWebsite.Models
{
    public class Result
    {
        public string TraineeID { get; set; }
        public ApplicationUser trainee { get; set; }
        public int ExamID { get; set; }
        public Exam Exam { get; set; }
        public string ResultQuiz { get; set; }
        public string ResultEssay { get; set; }
        public string SatrtDate { get; set; }
        public string StartTime { get; set; }
        public string FinishDate { get; set; }
        public string FinishTime { get; set; }
        public string Status { get; set; }
        public string Score { get; set; }
    }
}
