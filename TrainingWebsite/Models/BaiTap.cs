using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using TrainingWebsite.Areas.Identity.Data;

namespace TrainingWebsite.Models
{
    public class BaiTap
    {
        [Key]
        public int ID { get; set; }
        public string NoiDung { get; set; }
        public string CreateDate { get; set; }
        public int courseID { get; set; }
        public Course course { get; set; }
    }
}
