using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TrainingWebsite.Areas.Identity.Data;
using TrainingWebsite.Data;
using TrainingWebsite.Models;

namespace TrainingWebsite.ViewModels
{
    public class ItemCourse : ICourse
    {
        private readonly ApplicationDbContext data;
        private List<Course> courses = new List<Course>();
        public ItemCourse(ApplicationDbContext _data)
        {
            data = _data;
            //this.courses = _data.Courses.ToList();
        }
        public IEnumerable<CourseHomeViewModel> getCourseAll()
        {
            var model = (from c in data.Courses
                         join u in data.Users
                         on c.IDTrainer equals u.Id
                         select new CourseHomeViewModel
                         {
                             Id = c.courseID,
                             TenKhoaHoc = c.TenKhoaHoc,
                             Image = c.ImageTrainer,
                             TrainerName = u.FullName,

                         });
            return model;
        }
        public int totalCourse()
        {
            return courses.Count();
        }
        public int numberPage(int totalCourse, int limit)
        {
            float numberpage = totalCourse / limit;
            return (int)Math.Ceiling(numberpage);
        }
        public IEnumerable<CourseHomeViewModel> paginationCourse(int start, int limit)
        {
            var dt = (from s in data.Courses
                      join u in data.Users on s.IDTrainer equals u.Id
                      select new CourseHomeViewModel
                      {
                          Id = s.courseID,
                          TenKhoaHoc = s.TenKhoaHoc,
                          TrainerName = u.FullName,
                          Image = s.ImageTrainer
                      });
            var dataCourse = dt.OrderByDescending(x => x.Id).Skip(start).Take(limit);
            return dataCourse.ToList();

        }
        public void Add(Course course)
        {
            data.Courses.Add(course);
            data.SaveChanges();
        }
        public void Edit(Course course)
        {
            data.Entry(course).State = EntityState.Modified;

        }
        public Course FindById(int id)
        {
            var result = (from r in data.Courses where r.courseID == id select r).FirstOrDefault();
            return result;
        }
        public void Delete(int id)
        {
            Course course = data.Courses.Find(id);
            data.Courses.Remove(course);
            data.SaveChanges();
        }
        public IEnumerable<CourseHomeViewModel> getCourseAllTake()
        {
            var model = (from c in data.Courses
                         join u in data.Users
                         on c.IDTrainer equals u.Id
                         select new CourseHomeViewModel
                         {
                             Id = c.courseID,
                             TenKhoaHoc = c.TenKhoaHoc,
                             Image = c.ImageTrainer,
                             TrainerName = u.FullName,

                         }).Take(9).ToList();
            return model;
        }
        public IEnumerable<Occuption> JobPosDropDown()
        {
            return data.Occuptions;
        }
        public IEnumerable<SelectListItem> OccuptionDropDown()
        {
            var OccuptionList = (from s in data.Occuptions
                                 select new SelectListItem()
                                 {
                                     Text = s.OccuptionName,
                                     Value = s.OccuptionID.ToString()


                                 }).ToList();
            OccuptionList.Insert(0, new SelectListItem()
            {
                Text = "-----JobPos-----",
                Value = string.Empty
            });
            return new SelectList(OccuptionList, "Value", "Text");
        }
        public IEnumerable<SelectListItem> LevelDropDown()
        {
            var LevelList = (from s in data.Levels
                             select new SelectListItem()
                             {
                                 Text = s.LevelName,
                                 Value = s.ID.ToString()


                             }).ToList();
            LevelList.Insert(0, new SelectListItem()
            {
                Text = "-----Level-----",
                Value = string.Empty
            });
            return new SelectList(LevelList, "Value", "Text");
        }
        public IEnumerable<SelectListItem> ApartmentDropDown()
        {
            var ApartList = (from s in data.Apartments
                             select new SelectListItem()
                             {
                                 Text = s.ApartmentName,
                                 Value = s.ApartmentID.ToString()


                             }).ToList();
            ApartList.Insert(0, new SelectListItem()
            {
                Text = "-----Apartment-----",
                Value = string.Empty
            });
            return ApartList;
        }
        public IEnumerable<CourseHomeViewModel> SearchCourse(string searchString)
        {
            var result = (from c in data.Courses
                          join u in data.Users on c.IDTrainer equals u.Id
                          select new CourseHomeViewModel
                          {
                              Id = c.courseID,
                              TenKhoaHoc = c.TenKhoaHoc,
                              Image = c.ImageTrainer,
                              TrainerName = u.FullName,
                          });

            result = result.Where(x => x.TenKhoaHoc.Contains(searchString));

            return result;
        }
        public IEnumerable<CourseHomeViewModel> getCourse_JobPos_ApartmentAll()
        {
            var result = (from u in data.Users
                          join c in data.Courses on u.Id equals c.IDTrainer
                          join job in data.Occuptions on c.IDJobPos equals job.OccuptionID
                          join apart in data.Apartments on job.ApartmentID equals apart.ApartmentID
                          select new CourseHomeViewModel
                          {
                              Id = c.courseID,
                              TenKhoaHoc = c.TenKhoaHoc,
                              Image = c.ImageTrainer,
                              TrainerName = u.FullName,
                              JobPosID = job.OccuptionID,
                              ApartID = apart.ApartmentID
                          });
            return result;
        }
        public IEnumerable<CourseDetailViewModel> GetCourseDetail(int id)
        {
            var model = (from u in data.Users
                         join c in data.Courses on u.Id equals c.IDTrainer
                         join o in data.Occuptions on c.IDJobPos equals o.OccuptionID
                         join a in data.Apartments on o.ApartmentID equals a.ApartmentID
                         where c.courseID == id
                         select new CourseDetailViewModel
                         {
                             Id = id,
                             TenKhoaHoc = c.TenKhoaHoc,
                             ImageKH = c.ImageTrainer,
                             TenGV = u.FullName,
                             ThoiLuongKH = c.ThoiLuongKhoaHoc,
                             JobPosName_Course = o.OccuptionName,
                             ApartName = a.ApartmentName,
                             MucTieuKH = c.MucTieuKhoaHoc,
                             HinhThucDanhGia = c.HinhThucDanhGia
                         });
            return model;
        }
        public IEnumerable<CourseDetailViewModel> CourseFeatureDDetail(int id)
        {
            var model = (from c in data.Courses
                         where c.courseID == id
                         select new CourseDetailViewModel
                         {
                             ThoiLuongKH = c.ThoiLuongKhoaHoc,
                         });
            return model;
        }
        public IEnumerable<CourseDetailViewModel> TeacherFeatureDDetail(int id)
        {
            var model = (from u in data.Users
                         join o in data.Occuptions on u.OccuptionID equals o.OccuptionID
                         join c in data.Courses on u.Id equals c.IDTrainer
                         where c.courseID == id
                         select new CourseDetailViewModel
                         {
                             TenGV = u.FullName,
                             JobPosName_Trainer = o.OccuptionName,
                             ImageTrainer = u.Image
                         });
            return model;
        }
        public IEnumerable<CourseDetailViewModel> getLatestCourse(int id)
        {
            var jobID = data.Courses.Where(x => x.courseID == id).FirstOrDefault().IDJobPos;

            var model = (from c in data.Courses
                         where c.IDJobPos == jobID
                         select new CourseDetailViewModel
                         {
                             Id = c.courseID,
                             TenKhoaHoc = c.TenKhoaHoc,
                             ImageKH = c.ImageTrainer
                         });
            return model;
        }

        public IEnumerable<CourseHomeViewModel> getCourseInAdmin()
        {
            var courses = (from u in data.Users
                           join c in data.Courses on u.Id equals c.IDTrainer
                           join o in data.Occuptions on c.IDJobPos equals o.OccuptionID
                           join a in data.Apartments on o.ApartmentID equals a.ApartmentID
                           select new CourseHomeViewModel
                           {
                               Id = c.courseID,
                               TrainerName = u.FullName,
                               TenKhoaHoc = c.TenKhoaHoc,
                               JobPosName = o.OccuptionName,
                               ApartName = a.ApartmentName,
                               ThoiLuongKhoaHoc = c.ThoiLuongKhoaHoc,
                               ApartID = a.ApartmentID,
                               JobPosID = o.OccuptionID
                           });
            return courses;
        }
        public IEnumerable<EmployeeListViewModel> getEmployeeList()
        {
            var employeeList = (from level in data.Levels
                                join u in data.Users on level.ID equals u.LevelID
                                join o in data.Occuptions on u.OccuptionID equals o.OccuptionID
                                join a in data.Apartments on o.ApartmentID equals a.ApartmentID
                                select new EmployeeListViewModel
                                {
                                    Id = u.Id,
                                    FullName = u.FullName,
                                    Address = u.Address,
                                    BirthDate = u.BirthDate,
                                    Image = u.Image,
                                    OccuptionName = o.OccuptionName,
                                    ApartName = a.ApartmentName,
                                    LevelName = level.LevelName,
                                    LevelID = level.ID,
                                    OccuptionID = o.OccuptionID,
                                    ApartID = a.ApartmentID

                                });
            return employeeList;
        }
        public IEnumerable<profileViewModel> getProfileDetail(string Id)
        {
            var profile = (from level in data.Levels
                           join u in data.Users on level.ID equals u.LevelID
                           join o in data.Occuptions on u.OccuptionID equals o.OccuptionID
                           join a in data.Apartments on o.ApartmentID equals a.ApartmentID
                           where u.Id == Id
                           select new profileViewModel
                           {
                               Id = Id,
                               FullName = u.FullName,
                               Address = u.Address,
                               BirthDate = u.BirthDate,
                               Email = u.Email,
                               JobName = o.OccuptionName,
                               ApartmentName = a.ApartmentName,
                               LevelName = level.LevelName,
                               Phone = u.PhoneNumber
                           });
            return profile;
        }
        public IEnumerable<profileViewModel> getProfileImage(string Id)
        {
            var profile = (from u in data.Users
                           join o in data.Occuptions on u.OccuptionID equals o.OccuptionID
                           where u.Id == Id
                           select new profileViewModel
                           {
                               Id = Id,
                               FullName = u.FullName,
                               Image = u.Image,
                               JobName = o.OccuptionName,
                           });
            return profile;
        }
        public IEnumerable<ListClassroomViewModel> getListClassroom(string Id)
        {
            var model = (from c in data.Classrooms
                         where c.AdminID.Contains(Id)
                         select new ListClassroomViewModel
                         {
                             Id = c.classID,
                             Name = c.Name,
                             NgayBatDau = c.startDate,
                             NgayKetThuc = c.endDate,

                         });
            return model;
        }
        public void addClassroom(string unifile, string Id, createClassViewModel model)
        {

            var classroom = new Classroom()
            {
                Name = model.Name,
                startDate = model.NgayBatDau,
                endDate = model.NgayKetThuc,
                Image = unifile,
                AdminID = Id
            };
            data.Classrooms.Add(classroom);
            data.SaveChanges();
        }

    }
}