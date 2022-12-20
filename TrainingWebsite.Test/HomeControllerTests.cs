using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using TrainingWebsite.Controllers;
using TrainingWebsite.Data;
using TrainingWebsite.ViewModels;
using Xunit;

namespace TrainingWebsite.Test
{
    public class HomeControllerTests
    {
        private readonly Mock<ICourse> course;
        private readonly Mock<ILogger<HomeController>>_mockLogger;
        private Mock<IConfiguration> _mockConfiguration;
        private HomeController _controller;
        public HomeControllerTests()
        {
            _mockLogger = new Mock<ILogger<HomeController>>();
            _mockConfiguration = new Mock<IConfiguration>();
            course = new Mock<ICourse>();
            _controller = new HomeController(_mockLogger.Object,_mockConfiguration.Object,course.Object);
        }
        [Fact]
        public void Index_ActionExecutes_ReturnViewForIndex()
        {
            //var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            //    .UseInMemoryDatabase(databaseName: "TrainingWebsite")
            //    .Options;

            //using(var context = new ApplicationDbContext(options))
            //{
            //    context.Courses.Add(new Models.Course
            //    {
            //        ID = 9,
            //        MaKhoaHoc="CSC007",
            //        TenKhoaHoc="BI",
            //        ThoiLuongKhoaHoc=80,
            //        MucTieuKhoaHoc="ABC",
            //        HinhThucDanhGia="ABC",
            //        IDKhoaHocTienQuyet=null,
            //        IDTrainer= "081953a0-9e87-4931-8633-b773d95fc3d7",
            //        ImageTrainer= "27ea95a0a8144d15ae30944e9b42f2e1creativity-gbac0fc22a_640.jpg",
            //        IDJobPos = 15
            //    });
            //    context.SaveChanges();
            //}
            //using (var context = new ApplicationDbContext(options))
            //{
            //    ItemCourse controller = new ItemCourse(context);
            //var course = controller.getCourseAll();
            var course = _controller.Index();
            var result = course as ViewResult;
            Assert.NotNull(result);
            

                

            }
        }


    }
