using AutoMapper;
using Domain.Anemic.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using ViewModels.Controllers;
using Nazm.Results;
using Application.Common.Interfaces.Repository.Anemic.Base;
using Application.Common.Interfaces.Repository.Anemic.EF;

namespace Application.Features.Anemic.Controllers.Commands
{
    public class ControllerSyncAllCommand : BaseRequest, IRequest<Result<bool>>
    {
        public List<ControllerAndItsActions> ControllerAndItsActions { get; set; }
        public ControllerSyncAllCommand()
        {

        }
        public ControllerSyncAllCommand(List<ControllerAndItsActions> controllerAndItsActions)
        {
            ControllerAndItsActions = controllerAndItsActions;
        }

    }

    public class SyncAllControllerCommandHandler : BaseRequestHandler<ControllerSyncAllCommand, Result<bool>>
    {
        private readonly IControllerRepository _ControllerRepository;

        public SyncAllControllerCommandHandler(IUnitOfWork unitOfWork,
            IMapper mapper,
            IControllerRepository ControllerRepository) : base(unitOfWork, mapper)
        {
            _ControllerRepository = ControllerRepository;
        }

        protected override async Task<Result<bool>> HandleRequestAsync(ControllerSyncAllCommand input, CancellationToken cancellationToken)
        {
            var result = new FluentResults.Result<bool>();
            try
            {
                await _unitOfWork.BeginTransaction(cancellationToken);
                var controllers = await _unitOfWork.Controllers.GetAll.ToListAsync(cancellationToken);
                var actions = await _unitOfWork.ActionMethods.GetAll.ToListAsync(cancellationToken);
                foreach (var item in input.ControllerAndItsActions)
                {
                    var controller = new Controller
                    {
                        TitleEn = item.Controller,
                        TitleFa = item.ControllerNameFa
                    };
                    if (controllers == null || controllers.Any(c => c.TitleEn == item.Controller) == false)
                    {
                        _unitOfWork.Controllers.Insert(controller);
                        await _unitOfWork.Commit(cancellationToken);
                    }
                    else if (controllers != null)
                    {
                        controller.ControllerId = controllers.Where(c => c.TitleEn == item.Controller).Single().ControllerId;
                    }

                    foreach (var action in item.Actions)
                    {
                        var actionMethod = new ActionMethod
                        {
                            TitleEn = action.NameEn,
                            TitleFa = action.NameFa,
                            ControllerId = controller.ControllerId
                        };
                        if (actions.Any(c => c.TitleEn == action.NameEn) == false)
                        {
                            _unitOfWork.ActionMethods.Insert(actionMethod);
                        }

                    }
                    await _unitOfWork.Commit(cancellationToken);
                }

                await _unitOfWork.CommitTransaction(cancellationToken);
            }
            catch (Exception)
            {
                await _unitOfWork.RollbackTransaction(cancellationToken);
                throw;
            }
            return result.WithValue(true).ConvertToDtatResult();
        }
    }
}
