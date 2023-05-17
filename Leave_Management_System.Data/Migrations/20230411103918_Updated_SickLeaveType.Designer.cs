﻿// <auto-generated />
using System;
using Leave_Management_System.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
#nullable disable
namespace Leave_Management_System.Data.Migrations
{
    [DbContext(typeof(LeaveManagementDbContext))]
    [Migration("20230411103918_Updated_SickLeaveType")]
    partial class Updated_SickLeaveType
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);
            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);
            modelBuilder.Entity("Leave_Management_System.Data.Models.Employee", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");
                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));
                    b.Property<string>("Department")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");
                    b.Property<string>("Designation")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");
                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");
                    b.Property<DateTime>("JoiningDate")
                        .HasColumnType("datetime2");
                    b.Property<string>("Manager")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");
                    b.Property<string>("ManagerId")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");
                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");
                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");
                    b.HasKey("Id");
                    b.ToTable("Employees");
                });
            modelBuilder.Entity("Leave_Management_System.Data.Models.Holiday", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");
                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));
                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");
                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");
                    b.HasKey("Id");
                    b.ToTable("Holidays");
                });
            modelBuilder.Entity("Leave_Management_System.Data.Models.LeaveRequest", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");
                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));
                    b.Property<DateTime>("AppliedOn")
                        .HasColumnType("datetime2");
                    b.Property<int>("EmployeeId")
                        .HasColumnType("int");
                    b.Property<DateTime>("EndDate")
                        .HasColumnType("datetime2");
                    b.Property<string>("FileUrl")
                        .HasColumnType("nvarchar(max)");
                    b.Property<string>("LeaveType")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");
                    b.Property<string>("Reason")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");
                    b.Property<DateTime>("StartDate")
                        .HasColumnType("datetime2");
                    b.Property<string>("Status")
                        .HasColumnType("nvarchar(max)");
                    b.HasKey("Id");
                    b.HasIndex("EmployeeId");
                    b.ToTable("LeaveRequests");
                });
            modelBuilder.Entity("Leave_Management_System.Data.Models.LeaveType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");
                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));
                    b.Property<int>("AllowedLeaves")
                        .HasColumnType("int");
                    b.Property<int>("CarryForward")
                        .HasColumnType("int");
                    b.Property<int>("CreditForAssociate")
                        .HasColumnType("int");
                    b.Property<int>("CreditForManager")
                        .HasColumnType("int");
                    b.Property<bool>("IsApplicable")
                        .HasColumnType("bit");
                    b.Property<int>("LeaveCreditFrequency")
                        .HasColumnType("int");
                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");
                    b.HasKey("Id");
                    b.ToTable("LeaveTypes");
                });
            modelBuilder.Entity("Leave_Management_System.Data.Models.LeaveRequest", b =>
                {
                    b.HasOne("Leave_Management_System.Data.Models.Employee", "Employee")
                        .WithMany("LeaveRequests")
                        .HasForeignKey("EmployeeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                    b.Navigation("Employee");
                });
            modelBuilder.Entity("Leave_Management_System.Data.Models.Employee", b =>
                {
                    b.Navigation("LeaveRequests");
                });
#pragma warning restore 612, 618
        }
    }
}
