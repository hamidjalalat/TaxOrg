using Domain;
using Domain.Anemic;
using Domain.Anemic.Entities;
using EntityFrameworkCore.AutoHistory.Extensions;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Persistence.Common;
using System.Reflection;

namespace Persistence.Configurations.Anemic.Contexts
{
    /// <summary>
    /// https://docs.microsoft.com/en-us/ef/core/dbcontext-configuration/
    /// </summary>
    public class EFContext : IdentityDbContext<User>
    {
        public EFContext
            (DbContextOptions<EFContext> options) : base(options: options)
        {
            //Database.EnsureCreated();
        }

        public DbSet<ActionMethod> ActionMethods { get; set; }
        public DbSet<Controller> Controllers { get; set; }
        public DbSet<LoginHistory> LoginHistories { get; set; }
        public DbSet<MenuControllerAction> MenuControllerActions { get; set; }
        public DbSet<MenuController> MenuControllers { get; set; }
        public DbSet<Menu> Menus { get; set; }
        public DbSet<RegulationGroup> RegulationGroups { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<EntityAutoHistory> AutoHistories { get; set; }
        public DbSet<Tspagent> Tspagents { get; set; }
        public DbSet<Nazm_tspagent> Nazm_tspagents { get; set; }
        public DbSet<Branch> Branchs { get; set; }
        public DbSet<Netsale> Netsales { get; set; }
        public DbSet<DimProduct> DimProducts { get; set; }
        public DbSet<InvoiceModel> InvoiceModels { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            var BaseEntityType = typeof(ISoftDeletable);
            foreach (var entityType in modelBuilder.Model.GetEntityTypes())
            {
                if (BaseEntityType.IsAssignableFrom(entityType.ClrType))
                    modelBuilder.Entity(entityType.ClrType).AddQueryFilter<ISoftDeletable>(e => e.IsDeleted == false);
            }
            modelBuilder.EnableAutoHistory<EntityAutoHistory>(o => o.LimitChangedLength = false);
            Seed(modelBuilder);

            base.OnModelCreating(modelBuilder);
        }

        private static void Seed(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Menu>().HasData(
              new Menu
              {
                  MenuId = 1,
                  Title = "",
                  ParentId = HierarchyId.Parse("/"),
                  IsVisible = true,
                  MenuIcon = string.Empty,
                  PageDescription = string.Empty,
                  PageIcon = string.Empty,
                  PageTitle = string.Empty,
                  Sort = 1000,
                  Url = string.Empty,
                  EntityName = string.Empty,
              },
              new Menu
              {
                  MenuId = 2,
                  Title = "صفحه اصلی",
                  ParentId = HierarchyId.Parse("/"),
                  IsVisible = true,
                  MenuIcon = string.Empty,
                  PageDescription = string.Empty,
                  PageIcon = string.Empty,
                  PageTitle = "صفحه اصلی",
                  Sort = 1000,
                  Url = "/Index",
                  EntityName = string.Empty,
              },
              new Menu
              {
                  MenuId = 100,
                  Title = "تنظیم شاخص",
                  ParentId = HierarchyId.Parse("/"),
                  IsVisible = true,
                  MenuIcon = "cog-refresh-outline",
                  PageDescription = string.Empty,
                  PageIcon = string.Empty,
                  PageTitle = string.Empty,
                  Sort = 1,
                  Url = "regulationPrerequisite-menus",
                  EntityName = string.Empty,
              },
              new Menu
              {
                  MenuId = 101,
                  Title = "گروه بندی شاخص",
                  ParentId = HierarchyId.Parse("/100/"),
                  IsVisible = true,
                  MenuIcon = string.Empty,
                  PageDescription = string.Empty,
                  PageIcon = string.Empty,
                  PageTitle = "گروه بندی شاخص",
                  Sort = 1,
                  Url = "/RegulationGroups/List",
                  EntityName = string.Empty,
              }
              );

            modelBuilder.Entity<RegulationGroup>().HasData(
             new RegulationGroup
             {
                 Id = 1,
                 Code = 1,
                 Title = "قوانین بانک مرکزی",
                 Sort = 1,
                 IsActive = true,
                 IsDeleted = false
             },
             new RegulationGroup
             {
                 Id = 2,
                 Code = 2,
                 Title = "کشف قمار",
                 Sort = 2,
                 IsActive = true,
                 IsDeleted = false
             }
            );
        }


    }
}
