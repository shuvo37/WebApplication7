﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using WebApplication7.Data;

#nullable disable

namespace WebApplication7.Migrations
{
    [DbContext(typeof(taskContext))]
    partial class taskContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("WebApplication7.Models.ProblemInfo", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Explanation")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("InputExample")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("InputFormat")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("OutputExample")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("OutputFormat")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<int?>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("PblmDescription");
                });

            modelBuilder.Entity("WebApplication7.Models.Profile", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("SolvedProblems")
                        .HasColumnType("int");

                    b.Property<int>("TotalSubmissions")
                        .HasColumnType("int");

                    b.Property<int>("UserImgId")
                        .HasColumnType("int");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("UserImgId");

                    b.ToTable("Profile");
                });

            modelBuilder.Entity("WebApplication7.Models.RankViewModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("Rank")
                        .HasColumnType("int");

                    b.Property<int>("SolvedProblems")
                        .HasColumnType("int");

                    b.Property<int>("TotalRejected")
                        .HasColumnType("int");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Rank");
                });

            modelBuilder.Entity("WebApplication7.Models.Submission", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ExpectedOutput")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Input")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Language")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Output")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Pblm_id")
                        .HasColumnType("int");

                    b.Property<int?>("ProfileId")
                        .HasColumnType("int");

                    b.Property<string>("Result")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("SubmissionTime")
                        .HasColumnType("datetime2");

                    b.Property<int?>("UserId")
                        .HasColumnType("int");

                    b.Property<string>("Username")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("pblm_name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("ProfileId");

                    b.ToTable("Submission");
                });

            modelBuilder.Entity("WebApplication7.Models.UserImg", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ImagePath")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("UserImg");
                });

            modelBuilder.Entity("WebApplication7.Models.UserInfo", b =>
                {
                    b.Property<int>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("UserId"));

                    b.Property<string>("ConfirmPassword")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId");

                    b.ToTable("UserInfo");
                });

            modelBuilder.Entity("WebApplication7.Models.problems1", b =>
                {
                    b.Property<int>("PblmId")
                        .HasColumnType("int");

                    b.Property<int>("Accepted")
                        .HasColumnType("int");

                    b.Property<string>("Difficulty")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("MemoryLimit")
                        .HasColumnType("int");

                    b.Property<int>("SolvedBy")
                        .HasColumnType("int");

                    b.Property<double>("SuccessRate")
                        .HasColumnType("float");

                    b.Property<string>("TestCaseInput")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TestCaseOutput")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("TimeLimit")
                        .HasColumnType("int");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("UserId")
                        .HasColumnType("int");

                    b.Property<int>("WrongTry")
                        .HasColumnType("int");

                    b.HasKey("PblmId");

                    b.ToTable("PblmList");
                });

            modelBuilder.Entity("WebApplication7.Models.Profile", b =>
                {
                    b.HasOne("WebApplication7.Models.UserImg", "UserImg")
                        .WithMany()
                        .HasForeignKey("UserImgId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("UserImg");
                });

            modelBuilder.Entity("WebApplication7.Models.Submission", b =>
                {
                    b.HasOne("WebApplication7.Models.Profile", null)
                        .WithMany("Submission")
                        .HasForeignKey("ProfileId");
                });

            modelBuilder.Entity("WebApplication7.Models.Profile", b =>
                {
                    b.Navigation("Submission");
                });
#pragma warning restore 612, 618
        }
    }
}
