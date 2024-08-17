using Application.Common.Exceptions;
using Application.Common.Interfaces.Repository.Anemic.Base;
using Application.Common.Interfaces.Repository.Anemic.EF;
using AutoMapper;
using Domain.Anemic.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Nazm.Results;
using NazmMapping.Mappings;
using ViewModels.MenuControllers;

namespace Application.Features.Anemic.MenuControllers.Commands
{
    public class MenuControllerUpdateCommand : BaseRequest, IRequest<Result<MenuControllerViewModel>>, IMapFrom<MenuController>
    {
        public MenuControllerViewModel MenuControllerViewModel { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<MenuControllerViewModel, MenuController>()
                .ForMember(d => d.MenuControllerId, opt => opt.MapFrom(s => s.MenuControllerId))
                .ReverseMap();
        }
    }

    public class MenuControllerUpdateCommandHandler : BaseRequestHandler<MenuControllerUpdateCommand, Result<MenuControllerViewModel>>
    {
        private readonly IMenuControllerRepository _MenuControllerRepository;

        public MenuControllerUpdateCommandHandler(IUnitOfWork unitOfWork,
            IMapper mapper,
            IMenuControllerRepository MenuControllerRepository) : base(unitOfWork, mapper)
        {
            _MenuControllerRepository = MenuControllerRepository;
        }

        protected override async Task<Result<MenuControllerViewModel>> HandleRequestAsync(MenuControllerUpdateCommand input, CancellationToken cancellationToken)
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
                var entity = await _unitOfWork.MenuControllers.GetAll.Where(s => s.MenuControllerId == input.MenuControllerViewModel.MenuControllerId).SingleOrDefaultAsync(cancellationToken);

                if (entity == null)
                {
                    return response
                         .WithError(Resources.Messages.Errors.RecordEmpty)
                         .ConvertToDtatResult();
                }
                var model = _mapper.Map(input.MenuControllerViewModel, entity);

                _unitOfWork.MenuControllers.Update(model);
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
