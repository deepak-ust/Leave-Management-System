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
    [Migration("20230505065503_UserRoles_Table")]
    partial class UserRoles_Table
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
                    b.Property<int>("Band")
                        .HasColumnType("int");
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
                    b.Property<int>("ManagerId")
                        .HasColumnType("int");
                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");
                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");
                    b.Property<int>("RoleId")
                        .HasColumnType("int");
                    b.HasKey("Id");
                    b.HasIndex("RoleId");
                    b.ToTable("Employees");
                });
            modelBuilder.Entity("Leave_Management_System.Data.Models.EmployeeLeaveBalance", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");
                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));
                    b.Property<double?>("Balance")
                        .HasColumnType("float");
                    b.Property<double?>("CarryForward")
                        .HasColumnType("float");
                    b.Property<int>("EmployeeId")
                        .HasColumnType("int");
                    b.Property<int>("LeaveTypeId")
                        .HasColumnType("int");
                    b.HasKey("Id");
                    b.HasIndex("EmployeeId");
                    b.HasIndex("LeaveTypeId");
                    b.ToTable("EmployeeLeaveBalances");
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
                    b.Property<DateTime?>("EndDate")
                        .HasColumnType("datetime2");
                    b.Property<string>("FileUrl")
                        .HasColumnType("nvarchar(max)");
                    b.Property<string>("LeaveType")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");
                    b.Property<string>("Reason")
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
                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");
                    b.HasKey("Id");
                    b.ToTable("LeaveTypes");
                });
            modelBuilder.Entity("Leave_Management_System.Data.Models.Role", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");
                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));
                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");
                    b.HasKey("Id");
                    b.ToTable("Roles");
                });
            modelBuilder.Entity("Leave_Management_System.Data.Models.Rule", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");
                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));
                    b.Property<int>("AllowedLeaves")
                        .HasColumnType("int");
                    b.Property<int>("Band")
                        .HasColumnType("int");
                    b.Property<double>("Credit")
                        .HasColumnType("float");
                    b.Property<double>("DefaultBalance")
                        .HasColumnType("float");
                    b.Property<bool>("IsApplicable")
                        .HasColumnType("bit");
                    b.Property<int>("LeaveCreditFrequency")
                        .HasColumnType("int");
                    b.Property<int>("LeaveTypeId")
                        .HasColumnType("int");
                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");
                    b.HasKey("Id");
                    b.HasIndex("LeaveTypeId");
                    b.ToTable("Rules");
                });
            modelBuilder.Entity("Leave_Management_System.Data.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");
                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));
                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");
                    b.Property<int>("EmployeeId")
                        .HasColumnType("int");
                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");
                    b.HasKey("Id");
                    b.HasIndex("EmployeeId");
                    b.ToTable("Users");
                });
            modelBuilder.Entity("Leave_Management_System.Data.Models.UserRole", b =>
                {
                    b.Property<int>("UserId")
                        .HasColumnType("int");
                    b.Property<int>("RoleId")
                        .HasColumnType("int");
                    b.HasKey("UserId", "RoleId");
                    b.HasIndex("RoleId");
                    b.ToTable("UserRoles");
                });
            modelBuilder.Entity("Leave_Management_System.Data.Models.Employee", b =>
                {
                    b.HasOne("Leave_Management_System.Data.Models.Role", "Role")
                        .WithMany("Employees")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                    b.Navigation("Role");
                });
            modelBuilder.Entity("Leave_Management_System.Data.Models.EmployeeLeaveBalance", b =>
                {
                    b.HasOne("Leave_Management_System.Data.Models.Employee", "Employee")
                        .WithMany("LeaveBalances")
                        .HasForeignKey("EmployeeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                    b.HasOne("Leave_Management_System.Data.Models.LeaveType", "LeaveType")
                        .WithMany("EmployeeBalances")
                        .HasForeignKey("LeaveTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                    b.Navigation("Employee");
                    b.Navigation("LeaveType");
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
            modelBuilder.Entity("Leave_Management_System.Data.Models.Rule", b =>
                {
                    b.HasOne("Leave_Management_System.Data.Models.LeaveType", "LeaveType")
                        .WithMany("Rules")
                        .HasForeignKey("LeaveTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                    b.Navigation("LeaveType");
                });
            modelBuilder.Entity("Leave_Management_System.Data.Models.User", b =>
                {
                    b.HasOne("Leave_Management_System.Data.Models.Employee", "Employee")
                        .WithMany()
                        .HasForeignKey("EmployeeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                    b.Navigation("Employee");
                });
            modelBuilder.Entity("Leave_Management_System.Data.Models.UserRole", b =>
                {
                    b.HasOne("Leave_Management_System.Data.Models.Role", "Role")
                        .WithMany("UserRoles")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                    b.HasOne("Leave_Management_System.Data.Models.User", "User")
                        .WithMany("UserRoles")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                    b.Navigation("Role");
                    b.Navigation("User");
                });
            modelBuilder.Entity("Leave_Management_System.Data.Models.Employee", b =>
                {
                    b.Navigation("LeaveBalances");
                    b.Navigation("LeaveRequests");
                });
            modelBuilder.Entity("Leave_Management_System.Data.Models.LeaveType", b =>
                {
                    b.Navigation("EmployeeBalances");
                    b.Navigation("Rules");
                });
            modelBuilder.Entity("Leave_Management_System.Data.Models.Role", b =>
                {
                    b.Navigation("Employees");
                    b.Navigation("UserRoles");
                });
            modelBuilder.Entity("Leave_Management_System.Data.Models.User", b =>
                {
                    b.Navigation("UserRoles");
                });
#pragma warning restore 612, 618
        }
    }
}
