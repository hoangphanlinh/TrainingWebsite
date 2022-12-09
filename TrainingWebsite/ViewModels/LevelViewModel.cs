using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TrainingWebsite.ViewModels
{
    public class LevelViewModel
    {
        public string LevelID { get; set; }
        public List<SelectListItem> ListLevel { get; set; }
    }
}
