using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TrainingWebsite.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace TrainingWebsite.ViewModels
{
    public class CourseViewModel
    {
        public IEnumerable<CourseHomeViewModel> Popular { get; set; }
       
        public IEnumerable<Apartment> Apt { get; set; }
    }
    public class CourseHomeViewModel
    {
        public int Id { get; set; }
        public string Image { get; set; }
        public string TrainerName { get; set; }
        public string TenKhoaHoc { get; set; }
        public int JobPosID { get; set; }
        public int ApartID { get; set; }
      
    }
    public class CourseDetailViewModel
    {
        public int Id { get; set; }
        public string TenKhoaHoc { get; set; }
        public string ImageKH { get; set; }
        public string ImageTrainer { get; set; }
        public string TenGV { get; set; }
        public string JobPosName_Trainer { get; set; } 
        public string JobPosName_Course { get; set; }
        public int ThoiLuongKH { get; set; }
        public string MucTieuKH { get; set; }
        public string HinhThucDanhGia { get; set; }
        public int CountEnroll { get; set; }

    }
   

}
