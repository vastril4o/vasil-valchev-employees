using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Employee.Data.Config.Employee
{
    public class EmployeeProjectConfig : IEntityTypeConfiguration<Models.Employee.EmployeeProject>
    {
        public void Configure(EntityTypeBuilder<Models.Employee.EmployeeProject> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedOnAdd();

            builder.HasOne(x => x.Employee).WithMany().HasPrincipalKey(e => e.EmployeeId).IsRequired().OnDelete(DeleteBehavior.Cascade);
            builder.HasOne(x => x.Project).WithMany().HasPrincipalKey(e => e.ProjectId).IsRequired().OnDelete(DeleteBehavior.Cascade);
        }
    }
}
