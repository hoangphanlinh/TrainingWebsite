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
        public Course()
        {
            this.Course1 = new HashSet<Course>();
        }
        [Key]
        public int ID { get; set; }
        public string MaKhoaHoc { get; set; }
        public string TenKhoaHoc { get; set; }
        public int ThoiLuongKhoaHoc { get; set; }
        public string MucTieuKhoaHoc { get; set; }
        public string HinhThucDanhGia { get; set; }

        [ForeignKey("Course2")]
        public int? IDKhoaHocTienQuyet { get; set; }

        [ForeignKey("Trainer")]
        public string IDTrainer { get; set; }
        public ApplicationUser Trainer { get; set; }
        public string ImageTrainer { get; set; }

        [ForeignKey("JobPos")]
        public int IDJobPos { get; set; }

        public Occuption JobPos { get; set; }
        public virtual ICollection<Course> Course1 { get; set; }
        public virtual Course Course2 { get; set; }

       
    }
}
