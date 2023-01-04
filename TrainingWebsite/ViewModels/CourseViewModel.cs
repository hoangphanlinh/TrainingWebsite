using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TrainingWebsite.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using TrainingWebsite.Areas.Identity.Data;
using System.ComponentModel.DataAnnotations;

namespace TrainingWebsite.ViewModels
{
    public class CourseViewModel
    {
        public profileViewModel profile { get; set; }
        public ApplicationUser user { get; set; }
    }
    public class LoginViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name = "Remember me")]
        public bool RememberMe { get; set; }
    }
    public class CourseHomeViewModel
    {
        public int Id { get; set; }
        public string Image { get; set; }
        public string TrainerName { get; set; }
        public string TenKhoaHoc { get; set; }

        public string JobPosName { get; set; }
        public string ApartName { get; set; }
        public int ThoiLuongKhoaHoc { get; set; }
        public int JobPosID { get; set; }
        public int ApartID { get; set; }
      
    }
    public class CourseDetailViewModel
    {
        public int Id { get; set; }
        public string TenKhoaHoc { get; set; }
        public string ImageKH { get; set; }
        public byte[] ImageTrainer { get; set; }
        public string TenGV { get; set; }
        public string JobPosName_Trainer { get; set; } 
        public string JobPosName_Course { get; set; }
        public int ThoiLuongKH { get; set; }
        public string MucTieuKH { get; set; }
        public string HinhThucDanhGia { get; set; }
        public string ApartName { get; set; }
        public int CountEnroll { get; set; }

    }
   public class EmployeeListViewModel
    {
        public string Id { get; set; }
        public string FullName { get; set; }
        public string Address { get; set; }
        public DateTime BirthDate { get; set; }
        public byte[] Image { get; set; }
        public string OccuptionName { get; set; }
        public string LevelName { get; set; }
        public string ApartName { get; set; }
        public int OccuptionID { get; set; }
        public int LevelID { get; set; }
        public int ApartID { get; set; }
    }
    public class profileViewModel
    {
        public string Id { get; set; }
        public string FullName { get; set; }
        public string JobName { get; set; }
        public string ApartmentName { get; set; }
        public string LevelName { get; set; }
        public DateTime BirthDate { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public byte[] Image { get; set; }
        public int? JobID { get; set; }
        public int? LevelID { get; set; }

    }
    public class EditProfile
    {
        [Key]
        public string Id { get; set; }
        public string FullName { get; set; }
        public DateTime BirthDate { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public byte[] Image { get; set; }
        public int? JobID { get; set; }
        public int? LevelID { get; set; }
    }
    public class ListClassroomViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string NgayBatDau { get; set; }
        public string NgayKetThuc { get; set; }
        public int courseID { get; set; }
        public string courseName { get; set; }
    }
    public class createClassViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        [DataType(DataType.Date)]
        public string NgayBatDau { get; set; }
        [DataType(DataType.Date)]
        public string NgayKetThuc { get; set; }
        public string Image { get; set; }
        public string AdminID { get; set; }
       
    }
    public class ClassesViewModel
    {
        public int classID { get; set; }
        public string Name { get; set; }
        [DataType(DataType.Date)]
        public string startDate { get; set; }
        [DataType(DataType.Date)]
        public string endDate { get; set; }
        public string Image { get; set; }

        public string AdminID { get; set; }
        public List<Course> CourseList { get; set; }
        public List<checkBoxViewModel> Courses { get; set; }

    }
    public class checkBoxViewModel
    {
        public int courseID { get; set; }
        public string courseName { get; set; }
        public bool IsSelected { get; set; }
    }
}
