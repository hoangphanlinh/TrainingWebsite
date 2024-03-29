﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using TrainingWebsite.Data;

namespace TrainingWebsite.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20230107092315_UpdateTableBaiTap")]
    partial class UpdateTableBaiTap
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.32")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(256)")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedName")
                        .HasColumnType("nvarchar(256)")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RoleId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(128)")
                        .HasMaxLength(128);

                    b.Property<string>("ProviderKey")
                        .HasColumnType("nvarchar(128)")
                        .HasMaxLength(128);

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("RoleId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(128)")
                        .HasMaxLength(128);

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(128)")
                        .HasMaxLength(128);

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens");
                });

            modelBuilder.Entity("TrainingWebsite.Areas.Identity.Data.ApplicationUser", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<string>("Address")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("BirthDate")
                        .HasColumnType("datetime2");

                    b.Property<int?>("CommentId")
                        .HasColumnType("int");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(256)")
                        .HasMaxLength(256);

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("FullName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<byte[]>("Image")
                        .HasColumnType("varbinary(max)");

                    b.Property<int?>("LevelID")
                        .HasColumnType("int");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("NormalizedEmail")
                        .HasColumnType("nvarchar(256)")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedUserName")
                        .HasColumnType("nvarchar(256)")
                        .HasMaxLength(256);

                    b.Property<int?>("OccuptionID")
                        .HasColumnType("int");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("bit");

                    b.Property<string>("UserName")
                        .HasColumnType("nvarchar(256)")
                        .HasMaxLength(256);

                    b.Property<string>("role")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("CommentId");

                    b.HasIndex("LevelID");

                    b.HasIndex("NormalizedEmail")
                        .HasName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.HasIndex("OccuptionID");

                    b.ToTable("AspNetUsers");
                });

            modelBuilder.Entity("TrainingWebsite.Models.Apartment", b =>
                {
                    b.Property<int>("ApartmentID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ApartmentName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ApartmentID");

                    b.ToTable("Apartments");
                });

            modelBuilder.Entity("TrainingWebsite.Models.BaiTap", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("CreateDate")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NoiDung")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("courseID")
                        .HasColumnType("int");

                    b.HasKey("ID");

                    b.HasIndex("courseID");

                    b.ToTable("BaiTaps");
                });

            modelBuilder.Entity("TrainingWebsite.Models.ChuDe", b =>
                {
                    b.Property<int>("IDChuDe")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("IDKhoaHoc")
                        .HasColumnType("int");

                    b.Property<string>("NoiDung")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TenChuDe")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("IDChuDe");

                    b.HasIndex("IDKhoaHoc");

                    b.ToTable("Topic");
                });

            modelBuilder.Entity("TrainingWebsite.Models.Classroom", b =>
                {
                    b.Property<int>("classID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("AdminID")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Image")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("endDate")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("startDate")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("classID");

                    b.HasIndex("AdminID");

                    b.ToTable("Classrooms");
                });

            modelBuilder.Entity("TrainingWebsite.Models.Comment", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime?>("CreateOn")
                        .HasColumnType("datetime2");

                    b.Property<string>("Text")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Comments");
                });

            modelBuilder.Entity("TrainingWebsite.Models.Course", b =>
                {
                    b.Property<int>("courseID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("HinhThucDanhGia")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("IDJobPos")
                        .HasColumnType("int");

                    b.Property<string>("IDTrainer")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ImageTrainer")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("MaKhoaHoc")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("MucTieuKhoaHoc")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TenKhoaHoc")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("ThoiLuongKhoaHoc")
                        .HasColumnType("int");

                    b.HasKey("courseID");

                    b.HasIndex("IDJobPos");

                    b.HasIndex("IDTrainer");

                    b.ToTable("Courses");
                });

            modelBuilder.Entity("TrainingWebsite.Models.CourseClassroom", b =>
                {
                    b.Property<int>("courseID")
                        .HasColumnType("int");

                    b.Property<int>("classID")
                        .HasColumnType("int");

                    b.HasKey("courseID", "classID");

                    b.HasIndex("classID");

                    b.ToTable("CourseClassrooms");
                });

            modelBuilder.Entity("TrainingWebsite.Models.Exam", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("AnswertList")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Code")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("CourseID")
                        .HasColumnType("int");

                    b.Property<string>("MetaTitle")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("QuestionEssay")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("QuestionList")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ScoreList")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("StartDate")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasColumnType("nvarchar(1)");

                    b.Property<int>("Time")
                        .HasColumnType("int");

                    b.Property<int>("TotalQuestion")
                        .HasColumnType("int");

                    b.Property<int>("TotalScore")
                        .HasColumnType("int");

                    b.Property<string>("Type")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserList")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("endDate")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ID");

                    b.HasIndex("CourseID");

                    b.ToTable("Exams");
                });

            modelBuilder.Entity("TrainingWebsite.Models.Level", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("LevelName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ID");

                    b.ToTable("Levels");
                });

            modelBuilder.Entity("TrainingWebsite.Models.Occuption", b =>
                {
                    b.Property<int>("OccuptionID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("ApartmentID")
                        .HasColumnType("int");

                    b.Property<string>("OccuptionName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("OccuptionID");

                    b.HasIndex("ApartmentID");

                    b.ToTable("Occuptions");
                });

            modelBuilder.Entity("TrainingWebsite.Models.Question", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Answer")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Content")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("CourseID")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Status")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasColumnType("nvarchar(1)");

                    b.HasKey("ID");

                    b.HasIndex("CourseID");

                    b.ToTable("Questions");
                });

            modelBuilder.Entity("TrainingWebsite.Models.Reply", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("CommentId")
                        .HasColumnType("int");

                    b.Property<DateTime?>("CreateOn")
                        .HasColumnType("datetime2");

                    b.Property<string>("Text")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("CommentId");

                    b.HasIndex("UserId");

                    b.ToTable("Replies");
                });

            modelBuilder.Entity("TrainingWebsite.Models.Result", b =>
                {
                    b.Property<int>("ExamID")
                        .HasColumnType("int");

                    b.Property<string>("TraineeID")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("FinishDate")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FinishTime")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ResultEssay")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ResultQuiz")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SatrtDate")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Score")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("StartTime")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Status")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ExamID", "TraineeID");

                    b.HasIndex("TraineeID");

                    b.ToTable("Results");
                });

            modelBuilder.Entity("TrainingWebsite.Models.TaiLieu", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("ChuDeID")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Path")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ID");

                    b.HasIndex("ChuDeID");

                    b.ToTable("TaiLieus");
                });

            modelBuilder.Entity("TrainingWebsite.Models.TraineeCourse", b =>
                {
                    b.Property<int>("CourseID")
                        .HasColumnType("int");

                    b.Property<string>("TraineeID")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("EnrollDate")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Status")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Title")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TraineeLevel")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("CourseID", "TraineeID");

                    b.HasIndex("TraineeID");

                    b.ToTable("CourseTrainees");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("TrainingWebsite.Areas.Identity.Data.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("TrainingWebsite.Areas.Identity.Data.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("TrainingWebsite.Areas.Identity.Data.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("TrainingWebsite.Areas.Identity.Data.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("TrainingWebsite.Areas.Identity.Data.ApplicationUser", b =>
                {
                    b.HasOne("TrainingWebsite.Models.Comment", null)
                        .WithMany("Users")
                        .HasForeignKey("CommentId");

                    b.HasOne("TrainingWebsite.Models.Level", "Level")
                        .WithMany("AspNetUsers")
                        .HasForeignKey("LevelID");

                    b.HasOne("TrainingWebsite.Models.Occuption", "Occuption")
                        .WithMany("AspNetUsers")
                        .HasForeignKey("OccuptionID");
                });

            modelBuilder.Entity("TrainingWebsite.Models.BaiTap", b =>
                {
                    b.HasOne("TrainingWebsite.Models.Course", "course")
                        .WithMany()
                        .HasForeignKey("courseID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("TrainingWebsite.Models.ChuDe", b =>
                {
                    b.HasOne("TrainingWebsite.Models.Course", "course")
                        .WithMany()
                        .HasForeignKey("IDKhoaHoc")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("TrainingWebsite.Models.Classroom", b =>
                {
                    b.HasOne("TrainingWebsite.Areas.Identity.Data.ApplicationUser", "Admin")
                        .WithMany()
                        .HasForeignKey("AdminID");
                });

            modelBuilder.Entity("TrainingWebsite.Models.Comment", b =>
                {
                    b.HasOne("TrainingWebsite.Areas.Identity.Data.ApplicationUser", "User")
                        .WithMany()
                        .HasForeignKey("UserId");
                });

            modelBuilder.Entity("TrainingWebsite.Models.Course", b =>
                {
                    b.HasOne("TrainingWebsite.Models.Occuption", "JobPos")
                        .WithMany()
                        .HasForeignKey("IDJobPos")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("TrainingWebsite.Areas.Identity.Data.ApplicationUser", "Trainer")
                        .WithMany()
                        .HasForeignKey("IDTrainer");
                });

            modelBuilder.Entity("TrainingWebsite.Models.CourseClassroom", b =>
                {
                    b.HasOne("TrainingWebsite.Models.Classroom", "classroom")
                        .WithMany("CourseClassroom")
                        .HasForeignKey("classID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("TrainingWebsite.Models.Course", "course")
                        .WithMany("CourseClassroom")
                        .HasForeignKey("courseID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("TrainingWebsite.Models.Exam", b =>
                {
                    b.HasOne("TrainingWebsite.Models.Course", "course")
                        .WithMany()
                        .HasForeignKey("CourseID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("TrainingWebsite.Models.Occuption", b =>
                {
                    b.HasOne("TrainingWebsite.Models.Apartment", "Apartment")
                        .WithMany("Occuptions")
                        .HasForeignKey("ApartmentID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("TrainingWebsite.Models.Question", b =>
                {
                    b.HasOne("TrainingWebsite.Models.Course", "course")
                        .WithMany()
                        .HasForeignKey("CourseID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("TrainingWebsite.Models.Reply", b =>
                {
                    b.HasOne("TrainingWebsite.Models.Comment", "Comment")
                        .WithMany("Replies")
                        .HasForeignKey("CommentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("TrainingWebsite.Areas.Identity.Data.ApplicationUser", "User")
                        .WithMany()
                        .HasForeignKey("UserId");
                });

            modelBuilder.Entity("TrainingWebsite.Models.Result", b =>
                {
                    b.HasOne("TrainingWebsite.Models.Exam", "Exam")
                        .WithMany("Results")
                        .HasForeignKey("ExamID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("TrainingWebsite.Areas.Identity.Data.ApplicationUser", "trainee")
                        .WithMany("Results")
                        .HasForeignKey("TraineeID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("TrainingWebsite.Models.TaiLieu", b =>
                {
                    b.HasOne("TrainingWebsite.Models.ChuDe", "chuDe")
                        .WithMany()
                        .HasForeignKey("ChuDeID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("TrainingWebsite.Models.TraineeCourse", b =>
                {
                    b.HasOne("TrainingWebsite.Models.Course", "course")
                        .WithMany("CourseTrainee")
                        .HasForeignKey("CourseID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("TrainingWebsite.Areas.Identity.Data.ApplicationUser", "trainee")
                        .WithMany("CourseTrainee")
                        .HasForeignKey("TraineeID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
