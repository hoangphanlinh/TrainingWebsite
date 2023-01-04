using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using TrainingWebsite.Areas.Identity.Data;
using TrainingWebsite.Models;
using TrainingWebsite.Areas.Trainer.Models;
using TrainingWebsite.ViewModels;

namespace TrainingWebsite.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
      
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<Apartment> Apartments { get; set; }
        public DbSet<Occuption> Occuptions { get; set; }
        public DbSet<Level> Levels { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<ChuDe> Topic { get; set; }
        public DbSet<Classroom> Classrooms { get; set; }
        public DbSet<CourseClassroom> CourseClassrooms { get; set; }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<CourseClassroom>()
                .HasKey(e => new { e.courseID, e.classID });

            builder.Entity<CourseClassroom>()
                .HasOne(x => x.course)
                .WithMany(x => x.CourseClassroom)
                .HasForeignKey(x => x.courseID);

            builder.Entity<CourseClassroom>()
                .HasOne(x => x.classroom)
                .WithMany(x => x.CourseClassroom)
                .HasForeignKey(x => x.classID);

            base.OnModelCreating(builder);
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);

        }
      
    }
}
