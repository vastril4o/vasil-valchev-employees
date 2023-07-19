using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Employee.Data.Config.Project
{
    public class ProjectConfig : IEntityTypeConfiguration<Models.Project.Project>
    {
        public void Configure(EntityTypeBuilder<Models.Project.Project> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedOnAdd();
        }
    }
}
