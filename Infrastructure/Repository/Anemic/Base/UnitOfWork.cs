using Application.Common.Interfaces;
using Application.Common.Interfaces.Repository.Anemic.Base;
using Application.Common.Interfaces.Repository.Anemic.Dapper;
using Application.Common.Interfaces.Repository.Anemic.EF;
using Application.Common.Interfaces.Repository.Anemic.Oracle;
using Domain.Anemic.Entities;
using Domain.Enums;
using EntityFrameworkCore.AutoHistory.Extensions;
using FluentValidation.Results;
using Infrastructure.Common;
using Infrastructure.Repository.Anemic.Dapper;
using Infrastructure.Repository.Anemic.EF;
using Infrastructure.Repository.Anemic.Oracle;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence.Common;
using Persistence.Configurations.Anemic.Contexts;
using System.Threading;

namespace Infrastructure.Repository.Anemic.Base
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private readonly EFContext _dbContext;
        private readonly DapperContext _dapperContext;
        private readonly OracleContext _oracleContext;
        private readonly IMediator _mediator;
        private readonly IAuthenticatedUserService _authenticatedUserService;

        public UnitOfWork(EFContext dbContext,
            DapperContext dapperContext,
            OracleContext oracleContext,
            IMediator mediator,
            IAuthenticatedUserService authenticatedUserService
            )
        {
            _dbContext = dbContext;
            _dapperContext = dapperContext;
            _oracleContext = oracleContext;
            _mediator = mediator;
            _authenticatedUserService = authenticatedUserService;
        }

        public async Task BeginTransaction(CancellationToken cancellationToken)
        {
            await _dbContext.Database.BeginTransactionAsync(cancellationToken);
        }

        public async Task CommitTransaction(CancellationToken cancellationToken)
        {
            await _dbContext.Database.CommitTransactionAsync(cancellationToken);
            await _mediator.DispatchDomainEvents(_dbContext);
        }

        public async Task RollbackTransaction(CancellationToken cancellationToken)
        {
            await _dbContext.Database.RollbackTransactionAsync(cancellationToken);
        }

        public async Task Commit(CancellationToken cancellationToken,bool isPublishEvent, bool isDeleted, HistoryActionTypeEnum historyActionType)
        {
            //Publish messages
            if (isPublishEvent)
                await _mediator.DispatchDomainEvents(_dbContext);
            var addedEntities = _dbContext.DetectChanges(EntityState.Added);
            var historyModel = new EntityAutoHistory()
            {
                UserId = _authenticatedUserService.UserId == "" ? null : _authenticatedUserService.UserId,
                IPAddress = _authenticatedUserService.IPAddress,
                ComputerName = _authenticatedUserService.ComputerName,
                HistoryActionType = historyActionType,
                IsDeleted = isDeleted
            };
            _dbContext.EnsureAutoHistory(() => historyModel);
            var affectedRows = await _dbContext.SaveChangesAsync(cancellationToken).ConfigureAwait(false);

            _dbContext.EnsureAutoHistory(() => historyModel, addedEntities);
            affectedRows += await _dbContext.SaveChangesAsync(cancellationToken).ConfigureAwait(false);

            //await _dbContext.SaveChangesAsync();
        }

        private bool _disposed = false;
        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _dbContext.Dispose();
                    _oracleContext.Dispose();
                }
            }
            _disposed = true;
        }

        #region EF Repository

       
        public IUserRepository<User> Users => new UserRepository(_dbContext);

        public IMenuRepository Menus => new MenuRepository(_dbContext);

        public IControllerRepository Controllers => new ControllerRepository(_dbContext);

        public IActionMethodRepository ActionMethods => new ActionMethodRepository(_dbContext);

        public IMenuControllerRepository MenuControllers => new MenuControllerRepository(_dbContext);

        public IMenuControllerActionRepository MenuControllerActions => new MenuControllerActionRepository(_dbContext);


        public IRegulationGroupRepository RegulationGroups => new RegulationGroupRepository(_dbContext);
        public IHistoryRepository AutoHistories => new HistoryRepository(_dbContext);
        public ILoginHistoryRepository LoginHistories => new LoginHistoryRepository(_dbContext);

        public ITspagentRepository Tspagents => new TspagentRepository(_dbContext);
        public INazm_tspagentRepository Nazm_tspagents => new Nazm_tspagentRepository(_dbContext);
        public IBranchRepository Branchs => new BranchRepository(_dbContext);
        public INetsaleRepository Netsales => new NetsaleRepository(_dbContext);
        public IDimProductRepository DimProducts => new DimProductRepository(_dbContext);
        public IInvoiceModelRepository InvoiceModels => new InvoiceModelRepository(_dbContext);

        #endregion

        #region Dapper Repository
        public IUserDapperRepository UserDapper => new UserDapperRepository(_dapperContext);
        public IProductDapperRepository ProductDapper => new ProductDapperRepository(_dapperContext);
        #endregion

        #region Oracle Repository
        public IProductRepository Products => new ProductRepository(_oracleContext);

        #endregion

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
