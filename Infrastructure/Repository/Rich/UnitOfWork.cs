using Application.Common.Interfaces.Repository.Rich;
using EntityFrameworkCore.AutoHistory.Extensions;
using Infrastructure.Common;
using MediatR;
using Persistence.Common;
using Persistence.Configurations.Rich;

namespace Infrastructure.Repository.Rich
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private readonly DatabaseContext _dbContext;
        private readonly IMediator _mediator;
        //private readonly IAuthenticatedUserService _authenticatedUserService;

        public UnitOfWork(DatabaseContext dbContext,
            IMediator mediator
            //IAuthenticatedUserService authenticatedUserService
            )
        {
            _dbContext = dbContext;
            _mediator = mediator;
            //_authenticatedUserService = authenticatedUserService;
        }

        public async Task BeginTransaction()
        {
            await _dbContext.Database.BeginTransactionAsync();
        }

        public async Task CommitTransaction()
        {
            await _dbContext.Database.CommitTransactionAsync();
        }

        public async Task RollbackTransaction()
        {
            await _dbContext.Database.RollbackTransactionAsync();
        }

        public async Task Commit()
        {
            //_dbContext.EnsureAutoHistory(() => new EntityAutoHistory()
            //{
            //    //ModifiedByUserId = _authenticatedUserService.UserId,
            //    //IPAddress = _authenticatedUserService.IPAddress
            //});
            await _mediator.DispatchDomainEvents(_dbContext);
            await _dbContext.SaveChangesAsync();
        }

        private bool _disposed = false;



        IProjectRepository IUnitOfWork.Projects => new ProjectRepository(_dbContext);

        IMenuRepository IUnitOfWork.Menus => new MenuRepository(_dbContext);

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _dbContext.Dispose();
                }
            }
            _disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
