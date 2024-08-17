using Application.Common.Exceptions;
using Application.Common.Interfaces.Repository.Anemic.Base;
using Application.Common.Interfaces.Repository.Anemic.EF;
using AutoMapper;
using Domain.Anemic.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Nazm.Results;
using NazmMapping.Mappings;
using ViewModels.MenuControllerActions;

namespace Application.Features.Anemic.MenuControllerActions.Commands
{
    public class MenuControllerActionUpdateCommand : BaseRequest, IRequest<Result<MenuControllerActionViewModel>>, IMapFrom<MenuControllerAction>
    {
        public MenuControllerActionViewModel MenuControllerActionViewModel { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<MenuControllerActionViewModel, MenuControllerAction>()
                .ForMember(d => d.MenuControllerActionId, opt => opt.MapFrom(s => s.MenuControllerActionId))
                .ReverseMap();
        }
    }

    public class MenuControllerActionUpdateCommandHandler : BaseRequestHandler<MenuControllerActionUpdateCommand, Result<MenuControllerActionViewModel>>
    {
        private readonly IMenuControllerActionRepository _MenuControllerActionRepository;

        public MenuControllerActionUpdateCommandHandler(IUnitOfWork unitOfWork,
            IMapper mapper,
            IMenuControllerActionRepository MenuControllerActionRepository) : base(unitOfWork, mapper)
        {
            _MenuControllerActionRepository = MenuControllerActionRepository;
        }

        protected override async Task<Result<MenuControllerActionViewModel>> HandleRequestAsync(MenuControllerActionUpdateCommand input, CancellationToken cancellationToken)
        {
            var response = new FluentResults.Result<MenuControllerActionViewModel>();
            try
            {
                var isValid = await _unitOfWork.MenuControllerActions.IsValid(input.MenuControllerActionViewModel, cancellationToken);
                if (isValid.IsFailed)
                {
                    return isValid.ConvertToDtatResult();
                }
                await _unitOfWork.BeginTransaction(cancellationToken);
                var entity = await _unitOfWork.MenuControllerActions.GetAll.Where(s => s.MenuControllerActionId == input.MenuControllerActionViewModel.MenuControllerActionId).SingleOrDefaultAsync(cancellationToken);

                if (entity == null)
                {
                    return response
                         .WithError(Resources.Messages.Errors.RecordEmpty)
                         .ConvertToDtatResult();
                }
                var model = _mapper.Map(input.MenuControllerActionViewModel, entity);

                _unitOfWork.MenuControllerActions.Update(model);
                await _unitOfWork.Commit(cancellationToken);
                await _unitOfWork.CommitTransaction(cancellationToken);
                response
                    .WithSuccess(string.Format(Resources.Messages.Successes.Success, $"{Resources.Buttons.Save}"));
            }
            catch (Exception)
            {
                await _unitOfWork.RollbackTransaction(cancellationToken);
                throw;
            }
            return response
                    .WithValue(input.MenuControllerActionViewModel)
                    .ConvertToDtatResult();
        }
    }
}
