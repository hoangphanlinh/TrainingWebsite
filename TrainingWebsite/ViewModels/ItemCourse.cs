using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
            this.courses = _data.Courses.ToList();
        }
        public IEnumerable<CourseHomeViewModel> getCourseAll()
        {
            var model = (from c in data.Courses
                         join u in data.Users
                         on c.IDTrainer equals u.Id
                         select new CourseHomeViewModel
                         {
                             Id = c.ID,
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
                      select new CourseHomeViewModel { 
                        Id = s.ID,
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
            var result = (from r in data.Courses where r.ID == id select r).FirstOrDefault();
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
                             Id = c.ID,
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
        public List<SelectListItem> OccuptionDropDown()
        {
            var OccuptionList = (from s in data.Occuptions
                             select new SelectListItem()
                             {
                                 Text = s.OccuptionName,
                                 Value = s.OccuptionID.ToString()


                             }).ToList();
            OccuptionList.Insert(0, new SelectListItem()
            {
                Text = "-----Select JobPos-----",
                Value = string.Empty
            });
            return OccuptionList;
        }
       public  List<SelectListItem> LevelDropDown()
        {
            var LevelList = (from s in data.Levels
                                 select new SelectListItem()
                                 {
                                     Text = s.LevelName,
                                     Value = s.ID.ToString()


                                 }).ToList();
            LevelList.Insert(0, new SelectListItem()
            {
                Text = "-----Select-----",
                Value = string.Empty
            });
            return LevelList;
        }
        public IEnumerable<CourseHomeViewModel> SearchCourse(string searchString)
        {
            var result = (from c in data.Courses
                          join u in data.Users on c.IDTrainer equals u.Id
                          select new CourseHomeViewModel
                          {
                              Id = c.ID,
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
                              Id = c.ID,
                              TenKhoaHoc = c.TenKhoaHoc,
                              Image = c.ImageTrainer,
                              TrainerName = u.FullName,
                              JobPosID = job.OccuptionID,
                              ApartID = apart.ApartmentID
                          });
            return result;
        }



    }
}
