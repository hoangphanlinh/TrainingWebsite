using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace TrainingWebsite.Models
{
    public class ChuDe
    {
        [Key]
        public int IDChuDe { get; set; }
        public string TenChuDe { get; set; }
        public string NoiDung { get; set; }
        [ForeignKey("course")]
        public int IDKhoaHoc { get; set; }
        public Course course { get; set; }
    }
}
