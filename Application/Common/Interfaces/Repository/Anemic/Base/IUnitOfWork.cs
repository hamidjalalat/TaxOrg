using Application.Common.Interfaces.Repository.Anemic.Dapper;
using Application.Common.Interfaces.Repository.Anemic.EF;
using Application.Common.Interfaces.Repository.Anemic.Oracle;
using Domain.Anemic.Entities;
using Domain.Enums;

namespace Application.Common.Interfaces.Repository.Anemic.Base
{
    public interface IUnitOfWork : IDisposable
    {
        #region EF Repository
		IActionMethodRepository ActionMethods { get; }
        IControllerRepository Controllers { get; }
        ILoginHistoryRepository LoginHistories { get; }
        IMenuControllerActionRepository MenuControllerActions { get; }
        IMenuControllerRepository MenuControllers { get; }
        IMenuRepository Menus { get; }
        IRegulationGroupRepository RegulationGroups { get; }
        IUserRepository<User> Users { get; }
        IHistoryRepository AutoHistories { get; }

        ITspagentRepository Tspagents { get; }
        INazm_tspagentRepository Nazm_tspagents { get; }
        IBranchRepository Branchs { get; }
        INetsaleRepository Netsales { get; }
        IDimProductRepository DimProducts { get; }
        IInvoiceModelRepository InvoiceModels { get; }

        #endregion

        #region Dapper Repository
        IUserDapperRepository UserDapper { get; }
        IProductDapperRepository ProductDapper { get; }
        #endregion

        #region Oracle Repository
        IProductRepository Products { get; }
        #endregion

        Task BeginTransaction(CancellationToken cancellationToken);
        Task CommitTransaction(CancellationToken cancellationToken);
        Task RollbackTransaction(CancellationToken cancellationToken);
        Task Commit(CancellationToken cancellationToken,bool isPublishEvent = false, bool isDeleted = false, HistoryActionTypeEnum historyActionType = HistoryActionTypeEnum.None);
    }
}
