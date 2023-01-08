using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TrainingWebsite.Models
{
    public class TaiLieu
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Path { get; set; }
        public int ChuDeID { get; set; }
        public ChuDe chuDe { get; set; }
    }
}
