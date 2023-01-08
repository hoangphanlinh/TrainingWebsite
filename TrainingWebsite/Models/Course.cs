using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using TrainingWebsite.Areas.Identity.Data;

namespace TrainingWebsite.Models
{
    public class Course
    {
      
        [Key]
        public int courseID { get; set; }
        public string MaKhoaHoc { get; set; }
        public string TenKhoaHoc { get; set; }
        public int ThoiLuongKhoaHoc { get; set; }
        public string MucTieuKhoaHoc { get; set; }
        public string HinhThucDanhGia { get; set; }

        [ForeignKey("Trainer")]
        public string IDTrainer { get; set; }
        public ApplicationUser Trainer { get; set; }
        public string ImageTrainer { get; set; }

        [ForeignKey("JobPos")]
        public int IDJobPos { get; set; }
        public Occuption JobPos { get; set; }


        public List<CourseClassroom> CourseClassroom { get; set; }
        public List<TraineeCourse> CourseTrainee { get; set; }


    }
}