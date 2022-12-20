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
        public IEnumerable<Course> getCourseAll()
        {
            return courses;
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
        public IEnumerable<Course> paginationCourse(int start, int limit)
        {
            var dt = (from s in data.Courses select s);
            var dataCourse = dt.OrderByDescending(x => x.ID).Skip(start).Take(limit);
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
    }
}
