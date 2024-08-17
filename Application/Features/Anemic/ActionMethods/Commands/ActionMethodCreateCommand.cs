using Application.Common.Interfaces.Repository.Anemic.Base;
using Application.Common.Interfaces.Repository.Anemic.EF;
using AutoMapper;
using Domain.Anemic.Entities;
using MediatR;
using Nazm.Results;
using NazmMapping.Mappings;
using ViewModels.ActionMethods;

namespace Application.Features.Anemic.ActionMethods.Commands
{
    public class ActionMethodCreateCommand : BaseRequest, IRequest<Result<ActionMethodViewModel>>, IMapFrom<ActionMethod>
    {
        public ActionMethodViewModel ActionMethodViewModel { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<ActionMethodViewModel, ActionMethod>()
                .ForMember(d => d.ActionMethodId, opt => opt.MapFrom(s => s.ActionMethodId))
                .ReverseMap();
        }
    }

    public class CreateActionMethodCommandHandler : BaseRequestHandler<ActionMethodCreateCommand, Result<ActionMethodViewModel>>
    {
        private readonly IActionMethodRepository _ActionMethodRepository;

        public CreateActionMethodCommandHandler(IUnitOfWork unitOfWork,
            IMapper mapper,
            IActionMethodRepository ActionMethodRepository) : base(unitOfWork, mapper)
        {
            _ActionMethodRepository = ActionMethodRepository;
        }

        protected override async Task<Result<ActionMethodViewModel>> HandleRequestAsync(ActionMethodCreateCommand input, CancellationToken cancellationToken)
        {
            var result = new FluentResults.Result<ActionMethodViewModel>();
            try
            {
                await _unitOfWork.BeginTransaction(cancellationToken);
                var model = _mapper.Map<ActionMethod>(input.ActionMethodViewModel);

                _unitOfWork.ActionMethods.Insert(model);
                await _unitOfWork.Commit(cancellationToken);
                await _unitOfWork.CommitTransaction(cancellationToken);
                result
                    .WithSuccess(string.Format(Resources.Messages.Successes.Success, $"{Resources.Buttons.Save}"));
            }
            catch (Exception)
            {
                await _unitOfWork.RollbackTransaction(cancellationToken);
                throw;
            }
            return result
                .WithValue(input.ActionMethodViewModel)
                .ConvertToDtatResult();
        }
    }
}
