using Domain.Rich.Aggregates.Menus;
using Domain.Rich.Aggregates.Menus.ValueObjects;
using Domain.Rich.SeedWork;
using Domain.Rich.SharedKernel;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Configurations.Rich.Menus
{
    internal class MenuConfiguration : object,
                        IEntityTypeConfiguration
                        <Menu>
    {
        public MenuConfiguration() : base()
        {
        }

        public void Configure(
                Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder
                <Menu>
                builder)
        {


            //// **************************************************
            //builder
            //	using Microsoft.EntityFrameworkCore;
            //	.ToTable(name: "Menus")
            //	;
            //// **************************************************

            builder
                .HasKey("Id");
            builder.OwnsOne(x => x.Title);
            //builder
            //    .Property(p => p.Title)
            //    .HasMaxLength(maxLength: 200)
            //    .IsRequired(required: true)
            //    .UsePropertyAccessMode(propertyAccessMode: PropertyAccessMode.Field)
            //    .HasColumnName(name: nameof(Domain.Rich.SharedKernel.Title))
            //    .HasConversion(p => p.Value,
            //                        p => Domain.Rich.Aggregates.Menus.ValueObjects.Title.Create(p).Value);


            //builder
            //    .HasIndex(p => p.Title)
            //    .IsUnique(unique: true);


        }

    }
}
