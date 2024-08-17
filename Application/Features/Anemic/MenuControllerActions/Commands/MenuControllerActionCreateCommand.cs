using Application.Common.Interfaces.Repository.Anemic.Base;
using AutoMapper;
using Domain.Anemic.Entities;
using MediatR;
using Nazm.Results;
using NazmMapping.Mappings;
using ViewModels.MenuControllerActions;

namespace Application.Features.Anemic.MenuControllerActions.Commands
{
    public class MenuControllerActionCreateCommand : BaseRequest, IRequest<Result<MenuControllerActionViewModel>>, IMapFrom<MenuControllerAction>
    {
        public MenuControllerActionViewModel MenuControllerActionViewModel { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<MenuControllerActionViewModel, MenuControllerAction>()
                .ReverseMap();
        }
    }

    public class MenuControllerActionCreateCommandHandler : BaseRequestHandler<MenuControllerActionCreateCommand, Result<MenuControllerActionViewModel>>
    {
        public MenuControllerActionCreateCommandHandler(IUnitOfWork unitOfWork,
            IMapper mapper
           ) : base(unitOfWork, mapper)
        {

        }

        protected override async Task<Result<MenuControllerActionViewModel>> HandleRequestAsync(MenuControllerActionCreateCommand input, CancellationToken cancellationToken)
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
                var model = _mapper.Map<MenuControllerAction>(input.MenuControllerActionViewModel);

                _unitOfWork.MenuControllerActions.Insert(model);
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
