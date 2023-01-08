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
        public DbSet<TraineeCourse> CourseTrainees { get; set; }
        public DbSet<Exam> Exams { get; set; }
        public DbSet<Question> Questions { get; set; }
        public DbSet<Result> Results { get; set; }
        public DbSet<TaiLieu> TaiLieus { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Reply> Replies { get; set; }
        public DbSet<BaiTap> BaiTaps{ get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Result>()
               .HasKey(e => new { e.ExamID, e.TraineeID });

            builder.Entity<Result>()
                .HasOne(x => x.trainee)
                .WithMany(x => x.Results)
                .HasForeignKey(x => x.TraineeID);

            builder.Entity<Result>()
                .HasOne(x => x.Exam)
                .WithMany(x => x.Results)
                .HasForeignKey(x => x.ExamID);

            builder.Entity<TraineeCourse>()
               .HasKey(e => new { e.CourseID, e.TraineeID});

            builder.Entity<TraineeCourse>()
                .HasOne(x => x.course)
                .WithMany(x => x.CourseTrainee)
                .HasForeignKey(x => x.CourseID);

            builder.Entity<TraineeCourse>()
                .HasOne(x => x.trainee)
                .WithMany(x => x.CourseTrainee)
                .HasForeignKey(x => x.TraineeID);


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
