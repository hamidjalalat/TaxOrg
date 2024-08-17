using Application.Common.Interfaces.Repository.Anemic.Base;
using Application.Common.Interfaces.Repository.Anemic.EF;
using AutoMapper;
using Domain.Anemic.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Nazm.Results;
using NazmMapping.Mappings;

namespace Application.Features.Anemic.Controllers.Commands
{
    public class DeleteControllerCommand : BaseRequest, IRequest<Result<bool>>, IMapFrom<Controller>
    {
        public DeleteControllerCommand()
        {

        }
        public DeleteControllerCommand(int id)
        {
            ControllerId = id;
        }
        public int ControllerId { get; set; }
    }

    public class ControllerDeleteCommandHandler : BaseRequestHandler<DeleteControllerCommand, Result<bool>>
    {
        private readonly IControllerRepository _ControllerRepository;

        public ControllerDeleteCommandHandler(IUnitOfWork unitOfWork,
            IMapper mapper,
            IControllerRepository ControllerRepository) : base(unitOfWork, mapper)
        {
            _ControllerRepository = ControllerRepository;
        }

        protected override async Task<Result<bool>> HandleRequestAsync(DeleteControllerCommand input, CancellationToken cancellationToken)
        {
            bool result = false;
            var response = new FluentResults.Result<bool>();
            try
            {
                await _unitOfWork.BeginTransaction(cancellationToken);
                var model = await _ControllerRepository.FindByIdAsync(input.ControllerId, cancellationToken);
                if (model != null)
                {
                    var actionMethods = await _unitOfWork.ActionMethods
                        .GetAll.Where(s => s.ControllerId == model.ControllerId).ToListAsync(cancellationToken);
                    var menuControllers = await _unitOfWork.MenuControllers
                       .GetAll.Where(s => s.ControllerId == model.ControllerId).ToListAsync(cancellationToken);
                    if (actionMethods != null || menuControllers != null)
                    {
                        return response
                            .WithError(Resources.Messages.Errors.DependentTables)
                            .ConvertToDtatResult();
                    }
                    else
                    {
                        _unitOfWork.Controllers.Delete(model);
                        response
                            .WithSuccess(string.Format(Resources.Messages.Successes.Success, $"{Resources.Buttons.Delete}"));
                    }
                }
                await _unitOfWork.Commit(cancellationToken, isDeleted: true);
                await _unitOfWork.CommitTransaction(cancellationToken);
                result = true;
            }
            catch (Exception)
            {
                await _unitOfWork.RollbackTransaction(cancellationToken );
                result = false;
                throw;
            }
            return response
                .WithValue(result)
                .ConvertToDtatResult();
        }
    }
}
