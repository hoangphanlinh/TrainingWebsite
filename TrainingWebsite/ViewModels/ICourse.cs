using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TrainingWebsite.Models;

namespace TrainingWebsite.ViewModels
{
    public interface ICourse
    {
        IEnumerable<CourseHomeViewModel> getCourseAllTake();
        IEnumerable<CourseHomeViewModel> getCourseAll();
        IEnumerable<CourseHomeViewModel> getCourse_JobPos_ApartmentAll();

        int totalCourse();
        int numberPage(int totalCourse, int limit);
        IEnumerable<CourseHomeViewModel> paginationCourse(int start, int limit);
        void Add(Course course);
        void Edit(Course course);
        void Delete(int id);
        Course FindById(int id);
        public List<SelectListItem> OccuptionDropDown();
        List<SelectListItem> LevelDropDown();
        IEnumerable<CourseHomeViewModel> SearchCourse(string searchString);
        IEnumerable<Occuption> JobPosDropDown();
        //int getCountCourse();
        IEnumerable<CourseDetailViewModel> GetCourseDetail(int id);
        IEnumerable<CourseDetailViewModel> CourseFeatureDDetail(int id);
        IEnumerable<CourseDetailViewModel> TeacherFeatureDDetail(int id);
        IEnumerable<CourseDetailViewModel> getLatestCourse(int id);


    }

}

