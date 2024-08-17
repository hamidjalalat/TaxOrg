using Domain.Rich.Aggregates.Projects;
using Domain.Rich.SeedWork;
using Domain.Rich.SharedKernel;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Configurations.Rich.Projects
{
    internal class ProjectConfiguration : object,
                        IEntityTypeConfiguration
                        <Project>
    {
        public ProjectConfiguration() : base()
        {
        }

        public void Configure(
                Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder
                <Project>
                builder)
        {


            //// **************************************************
            //builder
            //	using Microsoft.EntityFrameworkCore;
            //	.ToTable(name: "Projects")
            //	;
            //// **************************************************


            builder
                .HasKey("Id");

            builder.OwnsOne(m => m.Title);

            //builder
            //    .Property(p => p.Title)
            //    .HasMaxLength(maxLength: 200)
            //    .IsRequired(required: true)
            //    .UsePropertyAccessMode(propertyAccessMode: PropertyAccessMode.Field)
            //    .HasColumnName(name: nameof(Title))
            //    .HasConversion(p => p.Value,
            //                        p => Domain.Rich.Aggregates.Projects.ValueObjects.Title.Create(p).Value);


            //builder
            //    .HasIndex(p => p.Title)
            //    .IsUnique(unique: true);


            builder
                .HasMany(project => project.Tasks)
                .WithOne(task => task.Project)
                .IsRequired(required: true)
                .HasForeignKey(nameof(Project) + nameof(Entity.Id))
                .OnDelete(deleteBehavior: DeleteBehavior.NoAction)
                ;


            builder
                .HasMany(project => project.PersonnelProjects)
                .WithOne(personnelProjects => personnelProjects.Project)
                .IsRequired(required: true)
                .HasForeignKey(nameof(Project) + nameof(Entity.Id))
                .OnDelete(deleteBehavior: DeleteBehavior.NoAction)
                ;

        }

    }
}
