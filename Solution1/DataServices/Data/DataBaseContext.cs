﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataServices.Models;
using Microsoft.EntityFrameworkCore;

namespace DataServices.Data
{
    public class DataBaseContext : DbContext
    {
        public DataBaseContext(DbContextOptions<DataBaseContext> options) : base(options)
        {
        }

        public DbSet<Employee> TblEmployee { get; set; }
        public DbSet<Role> TblRole { get; set; }
        public DbSet<Blogs> TblBlogs { get; set; }        
        public DbSet<Designation> TblDesignation { get; set; }
        public DbSet<Technology> TblTechnology { get; set; }
        public DbSet<Project> TblProject { get; set; }
        public DbSet<ProjectEmployee> TblProjectEmployee { get; set; }
        public DbSet<ProjectTechnology> TblProjectTechnology { get; set; }
        public DbSet<Client> TblClient { get; set; }
        public DbSet<Interviews> TblInterviews { get; set; }
        public DbSet<SOWRequirement> TblSOWRequirement { get; set; }
        public DbSet<InterviewStatus> TblInterviewStatus { get; set; }
        public DbSet<SOWStatus> TblSOWStatus { get; set; }
        public DbSet<SOW> TblSOW { get; set; }
        public DbSet<Department> TblDepartment { get; set; }
        public DbSet<EmployeeTechnology> TblEmployeeTechnology { get; set; }
        public DbSet<ContactType> TblContactType { get; set; }
        public DbSet<ClientContact> TblClientContact { get; set; }
        public DbSet<Webinars> TblWebinars { get; set; }
        public DbSet<SOWProposedTeam> TblSOWProposedTeam { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
           
            //----------3rd table_Employee------------------------------------
            modelBuilder.Entity<Employee>()
            .HasOne(e => e.Designation)
            .WithMany(d => d.Employee)
            .HasForeignKey(e => e.DesignationId);

            modelBuilder.Entity<Employee>()
            .HasOne(e => e.Department)
            .WithMany(d => d.Employee)
            .HasForeignKey(e => e.DepartmentId);

            modelBuilder.Entity<Employee>()
            .HasOne(e => e.Roles)
            .WithMany(d => d.Employee)
            .HasForeignKey(e => e.Role);

            modelBuilder.Entity<Employee>()
            .HasOne(e => e.ReportingToEmployee)
            .WithMany(d => d.Subordinates)
            .HasForeignKey(e => e.ReportingTo);    


            //----------4th table Technology------------------------------------
            modelBuilder.Entity<Technology>()
                        .HasOne(t => t.Department)
                        .WithMany(d => d.Technology)
                        .HasForeignKey(t => t.DepartmentId);



            //----------5th table EmployeeTechnology------------------------------------
            modelBuilder.Entity<EmployeeTechnology>()
                        .HasOne(et => et.Technologies)
                        .WithMany(t => t.EmployeeTechnology)
                        .HasForeignKey(et => et.Technology);

            modelBuilder.Entity<EmployeeTechnology>()
            .HasOne(et => et.Employee)
            .WithMany(t => t.Technology)
            .HasForeignKey(et => et.EmployeeID);


            //----------6th table Client------------------------------------
            modelBuilder.Entity<Client>()
                        .HasOne(c => c.Employee)
                        .WithMany(e => e.Client)
                        .HasForeignKey(c => c.SalesEmployee);


            //----------8th table ClientContact------------------------------------
            modelBuilder.Entity<ClientContact>()
                        .HasOne(cc => cc.Client)
                        .WithMany(c => c.ClientContact)
                        .HasForeignKey(cc => cc.ClientId);

            //----------13th SOW table------------------------------------

            modelBuilder.Entity<SOW>()
                        .HasOne(pt => pt.SowClients)
                        .WithMany(c => c.SOWs)
                        .HasForeignKey(pt => pt.ClientId);

            modelBuilder.Entity<SOW>()
                        .HasOne(pt => pt.SowProjects)
                        .WithMany(c => c.SOWs)
                        .HasForeignKey(pt => pt.ProjectId);

            modelBuilder.Entity<SOW>()
                        .HasOne(pt => pt.SOWStatus)
                        .WithMany(c => c.SOWs)
                        .HasForeignKey(pt => pt.Status);

            //----------14th SOWRequirement table------------------------------------
            modelBuilder.Entity<SOWRequirement>()
                        .HasOne(pt => pt.SOWs)
                        .WithMany(c => c.SOWRequirement)
                        .HasForeignKey(pt => pt.SOW);

            modelBuilder.Entity<SOWRequirement>()
                        .HasOne(pt => pt.Designation)
                        .WithMany(c => c.SOWRequirement)
                        .HasForeignKey(pt => pt.DesignationId);

            //----------15th SOWPropsedTeam table------------------------------------
            modelBuilder.Entity<SOWProposedTeam>()
                        .HasOne(pt => pt.SOWRequirements)
                        .WithMany(c => c.SOWProposedTeam)
                        .HasForeignKey(pt => pt.SOWRequirementId);

            modelBuilder.Entity<SOWProposedTeam>()
                        .HasOne(pt => pt.Employees)
                        .WithMany(c => c.SOWProposedTeam)
                        .HasForeignKey(pt => pt.EmployeeId);



            //----------17th Interviews table------------------------------------
            modelBuilder.Entity<Interviews>()
                        .HasOne(pt => pt.SOWRequirements)
                        .WithMany(c => c.Interviews)
                        .HasForeignKey(pt => pt.SOWRequirementId);

            modelBuilder.Entity<Interviews>()
                                    .HasOne(pt => pt.Employee)
                                    .WithMany(c => c.Interviews)
                                    .HasForeignKey(pt => pt.Recruiter);

            modelBuilder.Entity<Interviews>()
                                    .HasOne(pt => pt.InterviewStatus)
                                    .WithMany(c => c.Interviews)
                                    .HasForeignKey(pt => pt.StatusId);


            //----------18th Webinars table------------------------------------
            modelBuilder.Entity<Webinars>()
                                    .HasOne(pt => pt.Employee)
                                    .WithMany(c => c.Webinars)
                                    .HasForeignKey(pt => pt.Speaker);


            //----------19th Blogs table------------------------------------
            modelBuilder.Entity<Blogs>()
                                   .HasOne(pt => pt.Employee)
                                   .WithMany(c => c.Blog)
                                   .HasForeignKey(pt => pt.Author);

            //-----------Foreign key relation for Project model class-----------

            modelBuilder.Entity<Project>()
                .HasOne(c => c.Client)
                .WithMany(c => c.Project)
                .HasForeignKey(c => c.ClientId);

            modelBuilder.Entity<Project>()
                .HasOne(c => c.TechnicalProjectManagers)
                .WithMany(c => c.TechnicalProjects)
                .HasForeignKey(c => c.TechnicalProjectManager);

            modelBuilder.Entity<Project>()
                .HasOne(c => c.SalesContacts)
                .WithMany(c => c.SalesProjects)
                .HasForeignKey(c => c.SalesContact);

            modelBuilder.Entity<Project>()
                .HasOne(c => c.PMOs)
                .WithMany(c => c.PMOProjects)
                .HasForeignKey(c => c.PMO);

            //Foreign key relation for ProjectTechnology model class

            modelBuilder.Entity<ProjectEmployee>()
               .HasOne(c => c.Project)
               .WithMany(c => c.ProjectEmployees)
               .HasForeignKey(c => c.ProjectId);

            modelBuilder.Entity<ProjectEmployee>()
                .HasOne(c => c.Employee)
                .WithMany(c => c.ProjectEmployees)
                .HasForeignKey(c => c.EmployeeId);

            //Foreign key relation for ProjectTechnology model class

            modelBuilder.Entity<ProjectTechnology>()
               .HasOne(c => c.Project)
               .WithMany(c => c.Technology)
               .HasForeignKey(c => c.ProjectId);

            modelBuilder.Entity<ProjectTechnology>()
                .HasOne(c => c.Technologies)
                .WithMany(c => c.ProjectTechnology)
                .HasForeignKey(c => c.TechnologyId);

        }

    }

}
