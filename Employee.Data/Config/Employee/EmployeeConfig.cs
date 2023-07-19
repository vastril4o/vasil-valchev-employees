using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Employee.Data.Config.Employee
{
    public class EmployeeConfig : IEntityTypeConfiguration<Models.Employee.Employee>
    {
        public void Configure(EntityTypeBuilder<Models.Employee.Employee> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedOnAdd();
        }
    }
}
