using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TrainingWebsite.Models;

namespace TrainingWebsite.ViewModels
{
    public interface ICourse
    {
        IEnumerable<Course> getCourseAll();
        int totalCourse();
        int numberPage(int totalCourse, int limit);
        IEnumerable<Course> paginationCourse(int start, int limit);

        void Add(Course course);
        void Edit(Course course);
        void Delete(int id);
        Course FindById(int id);

    }
    
}

