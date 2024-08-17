using Application.Common.Interfaces.Repository.Anemic.Base;
using AutoMapper;
using Domain.Anemic.Entities;
using MediatR;
using Nazm.Results;
using ViewModels.MenuControllers;

namespace Application.Features.Anemic.MenuControllers.Commands
{

    public class MenuControllerCreateCommand : BaseRequest, IRequest<Result<MenuControllerViewModel>>
    {
        public MenuControllerViewModel MenuControllerViewModel { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<MenuControllerViewModel, MenuController>()
                .ForMember(d => d.MenuControllerId, opt => opt.MapFrom(s => s.MenuControllerId))
                .ReverseMap();
        }
    }

    public class MenuControllerCreateCommandHandler : BaseRequestHandler<MenuControllerCreateCommand, Result<MenuControllerViewModel>>
    {

        public MenuControllerCreateCommandHandler(IUnitOfWork unitOfWork,
            IMapper mapper
           ) : base(unitOfWork, mapper)
        {

        }

        protected override async Task<Result<MenuControllerViewModel>> HandleRequestAsync(MenuControllerCreateCommand input, CancellationToken cancellationToken)
        {
            var response = new FluentResults.Result<MenuControllerViewModel>();
            try
            {
                var isValid = await _unitOfWork.MenuControllers.IsValid(input.MenuControllerViewModel, cancellationToken);
                if (isValid.IsFailed)
                {
                    return response.ConvertToDtatResult();
                }
                await _unitOfWork.BeginTransaction(cancellationToken);
                var model = _mapper.Map<MenuController>(input.MenuControllerViewModel);

                _unitOfWork.MenuControllers.Insert(model);
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
                    .WithValue(input.MenuControllerViewModel)
                    .ConvertToDtatResult();
        }
    }
}
