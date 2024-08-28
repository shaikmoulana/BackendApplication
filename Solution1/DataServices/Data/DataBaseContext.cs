using System;
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

        //public DbSet<EmployeeLogin> EmployeeLoginTbl { get; set; }

        public DbSet<Employee> TblEmployee { get; set; }
        //public DbSet<Blogs> TblBlogs { get; set; }
        public DbSet<Blogs> Blogs { get; set; }
        
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
            /*modelBuilder.Entity<Department>(entity =>
            {
                entity.HasKey(e => e.Id);

                // Specify that MyGuid maps to a varchar(36) column
                entity.Property(e => e.Id)
                .HasColumnType("varchar(36)")
                .IsRequired();
            });*/

            //----------3rd table_Employee------------------------------------
            modelBuilder.Entity<Employee>()
            .HasOne(e => e.Designation)
            .WithMany(d => d.Employee)
            .HasForeignKey(e => e.DesignationId);

            modelBuilder.Entity<Employee>()
            .HasOne(e => e.Department)
            .WithMany(d => d.Employee)
            .HasForeignKey(e => e.DepartmentId);


            //----------4th table Technology------------------------------------
            modelBuilder.Entity<Technology>()
                        .HasOne(t => t.Department)
                        .WithMany(d => d.Technology)
                        .HasForeignKey(t => t.DepartmentId);



            //----------5th table EmployeeTechnology------------------------------------
            modelBuilder.Entity<EmployeeTechnology>()
                        .HasOne(et => et.Technologies)
                        .WithMany(t => t.EmployeeTechnology)
                        .HasForeignKey(et => et.TechnologyId);


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


            //----------9th table Project------------------------------------
            /*modelBuilder.Entity<Project>()
                        .HasOne(p => p.Client)
                        .WithMany(c => c.Project)
                        .HasForeignKey(p => p.ClientId);

            modelBuilder.Entity<Project>()
                        .HasOne(p => p.Employee)
                        .WithMany(e => e.Project)
                        .HasForeignKey(p => p.TechnicalProjectManager);

            modelBuilder.Entity<Project>()
                        .HasOne(p => p.EmployeeSalesContact)
                        .WithMany(e => e.Project)
                        .HasForeignKey(p => p.SalesContact);

            modelBuilder.Entity<Project>()
                        .HasOne(p => p.EmployeesPMO)
                        .WithMany(e => e.Project)
                        .HasForeignKey(p => p.PMO);
*/
            //----------10th table Project Technology------------------------------------
            /*modelBuilder.Entity<ProjectTechnology>()
                        .HasOne(pt => pt.Client)
                        .WithMany(c => c.ProjectTechnology)
                        .HasForeignKey(pt => pt.Project);

            modelBuilder.Entity<ProjectTechnology>()
                        .HasOne(pt => pt.Technologies)
                        .WithMany(c => c.ProjectTechnology)
                        .HasForeignKey(pt => pt.Technology);
*/
            //----------11th ProjectEmployee table------------------------------------
            /*modelBuilder.Entity<ProjectEmployee>()
                        .HasOne(pt => pt.Projects)
                        .WithMany(c => c.ProjectEmployee)
                        .HasForeignKey(pt => pt.Project);
               
            modelBuilder.Entity<ProjectEmployee>()
                        .HasOne(pt => pt.Employees)
                        .WithMany(c => c.ProjectEmployee)
                        .HasForeignKey(pt => pt.Employee);
*/

            //----------13th SOW table------------------------------------

            modelBuilder.Entity<SOW>()
                        .HasOne(pt => pt.Clients)
                        .WithMany(c => c.SOW)
                        .HasForeignKey(pt => pt.Client);

            modelBuilder.Entity<SOW>()
                        .HasOne(pt => pt.Employee)
                        .WithMany(c => c.SOW)
                        .HasForeignKey(pt => pt.Project);

            modelBuilder.Entity<SOW>()
                        .HasOne(pt => pt.SOWStatus)
                        .WithMany(c => c.SOW)
                        .HasForeignKey(pt => pt.Status);

            //----------14th SOWRequirement table------------------------------------
            modelBuilder.Entity<SOWRequirement>()
                        .HasOne(pt => pt.SOWs)
                        .WithMany(c => c.SOWRequirement)
                        .HasForeignKey(pt => pt.SOW);

            //----------15th SOWPropsedTeam table------------------------------------
            modelBuilder.Entity<SOWProposedTeam>()
                        .HasOne(pt => pt.SOWRequirements)
                        .WithMany(c => c.SOWProposedTeam)
                        .HasForeignKey(pt => pt.SOWRequirement);

            modelBuilder.Entity<SOWProposedTeam>()
                        .HasOne(pt => pt.Employees)
                        .WithMany(c => c.SOWProposedTeam)
                        .HasForeignKey(pt => pt.Employee);



            //----------17th Interviews table------------------------------------
            modelBuilder.Entity<Interviews>()
                        .HasOne(pt => pt.SOWRequirements)
                        .WithMany(c => c.Interviews)
                        .HasForeignKey(pt => pt.SOWRequirement);

            modelBuilder.Entity<Interviews>()
                                    .HasOne(pt => pt.Recruiters)
                                    .WithMany(c => c.Interviews)
                                    .HasForeignKey(pt => pt.Recruiter);

            modelBuilder.Entity<Interviews>()
                                    .HasOne(pt => pt.Statuses)
                                    .WithMany(c => c.Interviews)
                                    .HasForeignKey(pt => pt.Status);


            //----------18th Webinars table------------------------------------
            modelBuilder.Entity<Webinars>()
                                    .HasOne(pt => pt.Employee)
                                    .WithMany(c => c.Webinars)
                                    .HasForeignKey(pt => pt.Speaker);


            //----------19th Blogs table------------------------------------
            modelBuilder.Entity<Blogs>()
                                    .ToTable("Blogs")
                                    .HasOne(pt => pt.employee)
                                    .WithMany(c => c.Blog)
                                    .HasForeignKey(pt => pt.Author);



            //Foreign key relation for Project model class

            modelBuilder.Entity<Project>()
                .HasOne(c => c.Client)
                .WithMany(c => c.Project)
                .HasForeignKey(c => c.ClientId);

            modelBuilder.Entity<Project>()
                .HasOne(c => c.TechnicalProjectManagerId)
                .WithMany(c => c.TechnicalProjectManagerId)
                .HasForeignKey(c => c.TechnicalProjectManager);

            modelBuilder.Entity<Project>()
                .HasOne(c => c.SalesContactId)
                .WithMany(c => c.SalesContactId)
                .HasForeignKey(c => c.SalesContact);

            modelBuilder.Entity<Project>()
                .HasOne(c => c.PMOId)
                .WithMany(c => c.PMOId)
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
               .WithMany(c => c.ProjectTechnologies)
               .HasForeignKey(c => c.ProjectId);

            modelBuilder.Entity<ProjectTechnology>()
                .HasOne(c => c.Technology)
                .WithMany(c => c.ProjectTechnologies)
                .HasForeignKey(c => c.TechnologyId);

        }

    }

}
