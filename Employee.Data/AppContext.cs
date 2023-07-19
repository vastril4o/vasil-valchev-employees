using Employee.Data.Config.Employee;
using Employee.Data.Config.Project;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Reflection;
using System.Reflection.Emit;
using System.Reflection.Metadata;

namespace Employee.Data
{
    public class AppContext : DbContext
    {
        public AppContext()
        {
        }

        public AppContext(DbContextOptions options) : base(options)
        {
        }

        /*
        public AppContext(DbContextOptions<AppContext> options) : base(options)
        {
        }
        */

        public DbSet<Models.Employee.Employee> employees { get; set; }
        public DbSet<Models.Employee.EmployeeProject> employeeProjects { get; set; }
        public DbSet<Models.Project.Project> projects { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                IConfigurationRoot configuration = new ConfigurationBuilder()
                   .SetBasePath(Directory.GetCurrentDirectory())
                   .AddJsonFile("appsettings.json")
                   .Build();
                var connectionString = configuration.GetConnectionString("DefaultConnection");
                optionsBuilder.UseSqlServer(connectionString);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            new EmployeeConfig().Configure(modelBuilder.Entity<Models.Employee.Employee>());
            new EmployeeProjectConfig().Configure(modelBuilder.Entity<Models.Employee.EmployeeProject>());
            new ProjectConfig().Configure(modelBuilder.Entity<Models.Project.Project>());
        }
    }
}
