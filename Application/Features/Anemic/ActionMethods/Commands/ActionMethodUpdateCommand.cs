using Application.Common.Exceptions;
using Application.Common.Interfaces.Repository.Anemic.Base;
using Application.Common.Interfaces.Repository.Anemic.EF;
using AutoMapper;
using Domain.Anemic.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Nazm.Results;
using NazmMapping.Mappings;
using ViewModels.ActionMethods;

namespace Application.Features.Anemic.ActionMethods.Commands
{
    public class ActionMethodUpdateCommand : BaseRequest, IRequest<Result<ActionMethodViewModel>>, IMapFrom<ActionMethod>
    {
        public ActionMethodViewModel ActionMethodViewModel { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<ActionMethodViewModel, ActionMethod>()
                .ForMember(d => d.ActionMethodId, opt => opt.MapFrom(s => s.ActionMethodId))
                .ReverseMap();
        }
    }

    public class ActionMethodUpdateCommandHandler : BaseRequestHandler<ActionMethodUpdateCommand, Result<ActionMethodViewModel>>
    {
        private readonly IActionMethodRepository _ActionMethodRepository;

        public ActionMethodUpdateCommandHandler(IUnitOfWork unitOfWork,
            IMapper mapper,
            IActionMethodRepository ActionMethodRepository) : base(unitOfWork, mapper)
        {
            _ActionMethodRepository = ActionMethodRepository;
        }

        protected override async Task<Result<ActionMethodViewModel>> HandleRequestAsync(ActionMethodUpdateCommand input, CancellationToken cancellationToken)
        {
            var response = new FluentResults.Result<ActionMethodViewModel>();
            try
            {
                await _unitOfWork.BeginTransaction(cancellationToken);
                var entity = await _unitOfWork.ActionMethods.GetAll.Where(s => s.ActionMethodId == input.ActionMethodViewModel.ActionMethodId).SingleOrDefaultAsync(cancellationToken);

                if (entity == null)
                {
                    return response
                         .WithError(Resources.Messages.Errors.RecordEmpty)
                         .ConvertToDtatResult();
                }
                var model = _mapper.Map(input.ActionMethodViewModel, entity);

                _unitOfWork.ActionMethods.Update(model);
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
                .WithValue(input.ActionMethodViewModel)
                .ConvertToDtatResult();
        }
    }
}
