using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using TrainingWebsite.Areas.Admin.Controllers;
using TrainingWebsite.Models;
using TrainingWebsite.ViewModels;
using Xunit;

namespace TrainingWebsite.Test
{
    public class AdminCourseControllerTests
    {
        private readonly Mock<ICourse> _course;
        private AdminCourseController controller;
        public AdminCourseControllerTests()
        {
            _course = new Mock<ICourse>();
            controller = new AdminCourseController(_course.Object);
        }
        [Fact]
        public void Index_ReturnView()
        {
            var course = controller.Index();
            var result = course as ViewResult;
            Assert.NotNull(result);
        }
        [Fact]
        public void Index_ActionExcute_ReturnNumberOfData()
        {
            _course.Setup(repo => repo.getCourseAll())
                 .Returns(new List<Course>() { new Course(), new Course() });

            var result = controller.Index();

            var viewResult = Assert.IsType<ViewResult>(result);
            var employees = Assert.IsType<List<Course>>(viewResult.Model);
            Assert.Equal(2, employees.Count);
        }
        [Fact]
        public void Add_ActionExcute_ReturnsViewForAdd()
        {
            var result = controller.Add();

            Assert.IsType<ViewResult>(result);
        }
        [Fact]
        public void Add_InvalidModelState_CreateEmployeeNeverExecutes()
        {
            controller.ModelState.AddModelError("MucTieuKhoaHoc", "MucTieuKhoaHoc is required");

            var course = new Course
            {
                MaKhoaHoc = "CSC008",
                TenKhoaHoc = "BI",
                ThoiLuongKhoaHoc = 50,
                //MucTieuKhoaHoc = "ABC",
                HinhThucDanhGia = "ABC",
                IDKhoaHocTienQuyet = null,
                IDTrainer = "081953a0-9e87-4931-8633-b773d95fc3d7",
                ImageTrainer = null,
                IDJobPos = 15
            };

            var result = controller.Add(course);

            _course.Verify(x => x.Add(It.IsAny<Course>()), Times.Never);


            var viewResult = Assert.IsType<ViewResult>(result);
            var testCourse = Assert.IsType<Course>(viewResult.Model);

            Assert.Equal(course.MaKhoaHoc, testCourse.MaKhoaHoc);
            Assert.Equal(course.TenKhoaHoc, testCourse.TenKhoaHoc);
            Assert.Equal(course.ThoiLuongKhoaHoc, testCourse.ThoiLuongKhoaHoc);
            //Assert.Equal(emp.MucTieuKhoaHoc, course.MucTieuKhoaHoc);
            Assert.Equal(course.HinhThucDanhGia, testCourse.HinhThucDanhGia);
            Assert.Equal(course.IDKhoaHocTienQuyet, testCourse.IDKhoaHocTienQuyet);
            Assert.Equal(course.IDTrainer, testCourse.IDTrainer);
            Assert.Equal(course.ImageTrainer, testCourse.ImageTrainer);
            Assert.Equal(course.IDJobPos, testCourse.IDJobPos);
        }
        [Fact]
        public void Add_ModelStateValid_AddCourseCalledOnce()
        {
            Course emp = null;
            _course.Setup(r => r.Add(It.IsAny<Course>()))
                .Callback<Course>(x => emp = x);

            var course = new Course { 
                MaKhoaHoc="CSC008",
                TenKhoaHoc="BI",
                ThoiLuongKhoaHoc= 50,
                MucTieuKhoaHoc="ABC",
                HinhThucDanhGia="ABC",
                IDKhoaHocTienQuyet=null,
                IDTrainer = "081953a0-9e87-4931-8633-b773d95fc3d7",
                ImageTrainer=null,
                IDJobPos=15
            };

            var result = controller.Add(course);

            _course.Verify(x => x.Add(It.IsAny<Course>()), Times.Once);


            Assert.Equal(emp.MaKhoaHoc, course.MaKhoaHoc);
            Assert.Equal(emp.TenKhoaHoc, course.TenKhoaHoc);
            Assert.Equal(emp.ThoiLuongKhoaHoc, course.ThoiLuongKhoaHoc);
            Assert.Equal(emp.MucTieuKhoaHoc, course.MucTieuKhoaHoc);
            Assert.Equal(emp.HinhThucDanhGia, course.HinhThucDanhGia);
            Assert.Equal(emp.IDKhoaHocTienQuyet, course.IDKhoaHocTienQuyet);
            Assert.Equal(emp.IDTrainer, course.IDTrainer);
            Assert.Equal(emp.ImageTrainer, course.ImageTrainer);
            Assert.Equal(emp.IDJobPos, course.IDJobPos);
        }
        [Fact]
        public void Add_ActionExcuted_RedirectsToIndex()
        {
            //Arrange
            var course = new Course
            {
                MaKhoaHoc = "CSC008",
                TenKhoaHoc = "BI",
                ThoiLuongKhoaHoc = 50,
                MucTieuKhoaHoc = "ABC",
                HinhThucDanhGia = "ABC",
                IDKhoaHocTienQuyet = null,
                IDTrainer = "081953a0-9e87-4931-8633-b773d95fc3d7",
                ImageTrainer = null,
                IDJobPos = 15
            };
            //Action
            var result = controller.Add(course);
            //Assert

            var redirecToActionResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("Index", redirecToActionResult.ActionName);
        }
        [Fact]
        public void Edit_ActionExcuted_ReturnViewForEditWhenIdNull()
        {
            //Act
            var result = controller.Edit(id: null);

            //Assert
            var redirectToActionResult = Assert.IsType<NotFoundResult>(result);
        }
        [Fact]
        public void Edit_ActionExcuted__ReturnNotFoundWhenCourseNull()
        {
            var result = controller.Edit(id: 1);

            //Assert
            var contentResult = Assert.IsType<NotFoundResult>(result);
            
        }
        [Fact]
        public void Edit_ActionExcuted_ReturnUpdateCourse()
        {
            //Arrange
            var editcourse = new Course
            {
                ID = 1,
                MaKhoaHoc = "CSC008",
                TenKhoaHoc = "BI",
                //ThoiLuongKhoaHoc = 50,
                //MucTieuKhoaHoc = "ABC",
                //HinhThucDanhGia = "ABC",
                //IDKhoaHocTienQuyet = null,
                //IDTrainer = "081953a0-9e87-4931-8633-b773d95fc3d7",
                //ImageTrainer = null,
                //IDJobPos = 15
            };
            //Act
            var result = controller.Edit(editcourse);

            //Assert
            var redirecToActionResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("Index", redirecToActionResult.ActionName);

            Assert.Equal("BI", editcourse.TenKhoaHoc);
            
        }
        [Fact]
        public void Delete_ActionExcuted_RetunViewForIndex()
        {
            //Act
            var result = controller.Delete(1);
            //Assert
            var redirecToActionResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("Index", redirecToActionResult.ActionName);
        }
    }
}
