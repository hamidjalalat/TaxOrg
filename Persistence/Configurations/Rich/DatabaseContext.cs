using System.Linq;
using System.Reflection;
using Domain.Rich.Aggregates.Menus;
using Domain.Rich.Aggregates.PersonnelProjects;
using Domain.Rich.Aggregates.Personnels;
using Domain.Rich.Aggregates.PersonnelTaskTimes;
using Domain.Rich.Aggregates.Projects;
using Domain.Rich.Aggregates.Tasks;
using Domain.Rich.Aggregates.Menus;
using Domain.Rich.SeedWork;


namespace Persistence.Configurations.Rich
{
    /// <summary>
    /// https://docs.microsoft.com/en-us/ef/core/dbcontext-configuration/
    /// </summary>
    public class DatabaseContext : Microsoft.EntityFrameworkCore.DbContext
    {
        public DatabaseContext
            (Microsoft.EntityFrameworkCore.DbContextOptions<DatabaseContext> options, MediatR.IMediator mediator) : base(options: options)
        {
            Mediator = mediator;

            //Database.EnsureCreated();
        }

        private MediatR.IMediator Mediator { get; }

        public Microsoft.EntityFrameworkCore.DbSet<Menu> Menus { get; set; }
        public Microsoft.EntityFrameworkCore.DbSet<Project> Projects { get; set; }

        public Microsoft.EntityFrameworkCore.DbSet<Task> Tasks { get; set; }

        public Microsoft.EntityFrameworkCore.DbSet<Personnel> Personnels { get; set; }

        public Microsoft.EntityFrameworkCore.DbSet<PersonnelProject> PersonnelProjects { get; set; }

        public Microsoft.EntityFrameworkCore.DbSet<PersonnelTaskTime> PersonnelTaskTime { get; set; }
        
       

        protected override void OnModelCreating
            (Microsoft.EntityFrameworkCore.ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly
                (Assembly.GetExecutingAssembly());
            //modelBuilder.ApplyConfigurationsFromAssembly
            //     (typeof(Persistence.Configurations.Rich.Projects.ProjectConfiguration).Assembly);

        }

        public override
            async
            System.Threading.Tasks.Task<int>
            SaveChangesAsync(System.Threading.CancellationToken cancellationToken = default)
        {
            // **************************************************
            // دستورات ذیل بینهایت اهمیت دارند
            // **************************************************
            var enumerationEntries =
                ChangeTracker.Entries()
                ////.Where(current => EnumerationTypes.Contains(current.Entity.GetType()));
                //.Where(current => current.Entity.GetType().BaseType == typeof(Domain.SeedWork.Enumeration));
                .Where(current => current.Entity.GetType().IsSubclassOf(typeof(Enumeration)));

            foreach (var enumerationEntry in enumerationEntries)
            {
                enumerationEntry.State =
                    Microsoft.EntityFrameworkCore.EntityState.Unchanged;
            }
            // **************************************************

            int affectedRows =
                await base.SaveChangesAsync(cancellationToken: cancellationToken);

            if (affectedRows > 0)
            {
                var aggregateRoots =
                    ChangeTracker.Entries()
                    .Where(current => current.Entity is Dtat.Ddd.IAggregateRoot)
                    .Select(current => current.Entity as Dtat.Ddd.IAggregateRoot)
                    .ToList()
                    ;

                foreach (var aggregateRoot in aggregateRoots)
                {
                    // Dispatch Events!
                    foreach (var domainEvent in aggregateRoot.DomainEvents)
                    {
                        await Mediator.Publish(domainEvent, cancellationToken);
                    }

                    // Clear Events!
                    aggregateRoot.ClearDomainEvents();
                }
            }

            return affectedRows;
        }
    }
}
