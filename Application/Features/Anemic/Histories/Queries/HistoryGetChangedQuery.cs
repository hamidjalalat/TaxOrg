using Application.Common.Interfaces.Repository.Anemic.Base;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Nazm.Results;
using Newtonsoft.Json;
using ViewModels.Histories;

namespace Application.Features.Anemic.Histories.Queries
{

	public class HistoryGetChangedQuery : BaseRequest, IRequest<Result<HistoryChangedViewModel>>
	{
		private HistoryGetChangedQuery()
		{
		}
		public HistoryGetChangedQuery(string entity, int id)
		{
			Id = id;
			Entity = entity;
		}
		public string Entity { get; private set; }
		public int Id { get; private set; }

	}

	public class AutoHistoryGetChangedQueryHandler : BaseRequestHandler<HistoryGetChangedQuery, Result<HistoryChangedViewModel>>
	{
		public AutoHistoryGetChangedQueryHandler(
			IMapper mapper,
			IUnitOfWork unitOfWork) : base(unitOfWork, mapper)
		{

		}

		protected async override Task<Result<HistoryChangedViewModel>> HandleRequestAsync(HistoryGetChangedQuery input, CancellationToken cancellationToken)
		{
			var result = new FluentResults.Result<HistoryChangedViewModel>();

			var response = await _unitOfWork.AutoHistories.GetAll
			.Where(s => s.Id == input.Id)
			//.Select(s => new HistorySimpleViewModel
			.Select(s => new HistoryChangedViewModel
			{
				Kind = s.Kind,
				HistoryActionType = s.HistoryActionType,
				Changed = s.Changed,
				IsDeleted = s.IsDeleted
			})
			.SingleOrDefaultAsync(cancellationToken);

			//var type = Type.GetType($"ViewModels.{input.Entity.GetFolderName}.{input.Entity},ViewModels");
			var type = Type.GetType($"{input.Entity},ViewModels");
			var obj = JsonConvert.DeserializeObject<dynamic>(response.Changed);

			object? before = null;
			object? after = null;

			if (response.HistoryActionType != Domain.Enums.HistoryActionTypeEnum.None)
			{
				var objTemp = obj;

				string jsonString = Convert.ToString(objTemp);
				before = JsonConvert.DeserializeObject(jsonString, type);
			}
			else
			{
				if (response.Kind == EntityState.Added)
				{
					var objTemp = obj;

					string jsonString = Convert.ToString(objTemp);
					before = JsonConvert.DeserializeObject(jsonString, type);
				}
				else if (response.Kind == EntityState.Modified && response.IsDeleted == false)
				{
					if (obj?.before != null)
					{
						var objTemp = obj?.before;

						string jsonString = Convert.ToString(objTemp);
						before = JsonConvert.DeserializeObject(jsonString, type);
					}
					if (obj?.after != null)
					{
						var objTemp = obj?.after;

						string jsonString = Convert.ToString(objTemp);
						after = JsonConvert.DeserializeObject(jsonString, type);
					}
				}
				else if (response.IsDeleted)
				{
					if (obj?.before != null)
					{
						var objTemp = obj?.before;

						string jsonString = Convert.ToString(objTemp);
						before = JsonConvert.DeserializeObject(jsonString, type);
					}
				}
			}

			HistoryChangedViewModel historyChangedViewModel = new()
			{
				HistoryActionType = response.HistoryActionType,
				IsDeleted = response.IsDeleted,
				Kind = response.Kind,
				ResultType = type,
				before = before,
				after = after,
			};

			return result.WithValue(historyChangedViewModel).ConvertToDtatResult();
		}
	}
}
