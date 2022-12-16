using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TrainingWebsite.Models;

namespace TrainingWebsite.ViewModels
{
    public class CourseViewModel
    {
        public IEnumerable<CourseHomeViewModel> Popular { get; set; }
       
        public IEnumerable<Apartment> Apt { get; set; }
    }
    public class CourseHomeViewModel
    {
        public string Image { get; set; }
        public string TrainerName { get; set; }
        public string TenKhoaHoc { get; set; }

        

    }

}
