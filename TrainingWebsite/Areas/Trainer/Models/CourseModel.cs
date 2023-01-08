using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TrainingWebsite.Areas.Trainer.Models
{
   public class ListCourseViewModel
    {
        public string MaKhoaHoc { get; set; }
        public int ID { get; set; }
        public string Name { get; set; }
        public int ThoiLuongKhoaHoc { get; set; }
        public string MucTieuKhoaHoc { get; set; }
        public string HinhThucDanhGia { get; set; }
        public int? IDKhoaHocTienQuyet { get; set; }
        public string JobPosName { get; set; }
    }
    public class CreateCourseViewModel
    {
        public int ID { get; set; }
        [Required]
        public string MaKhoaHoc { get; set; }
        [Required]
        public string TenKhoaHoc { get; set; }
        [Required]
        public int ThoiLuongKhoaHoc { get; set; }
        [Required]
        public string MucTieuKhoaHoc { get; set; }
        [Required]
        public string HinhThucDanhGia { get; set; }

        public string IDTrainer { get; set; }
        public string ImageTrainer { get; set; }
        public IFormFile Photo { get; set; }
        [Required]
        public int IDJobPos { get; set; }

    }

}
