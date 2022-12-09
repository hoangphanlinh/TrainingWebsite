using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TrainingWebsite.Areas.Trainer.Models
{
    public class SessionListViewModel
    {
        public int ID { get; set; }
       
        public string TenChuDe { get; set; }
      
    }
    public class CreateTopicViewModel
    {
        public int ID { get; set; }
        [Required]
        public string TenChuDe { get; set; }
        [Required]
        public string NoiDung { get; set; }
        public int IDKhoaHoc { get; set;  }
        public IFormFile BaiGiangVideo { get; set; }
    }
}
