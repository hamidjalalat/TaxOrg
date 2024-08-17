using Application.Common.Interfaces.Repository.Rich;
using AutoMapper;
using MediatR;

namespace Application.Features.Rich
{
    public abstract class BaseRequestHandler_Rich<TInput, TOutput> : IRequestHandler<TInput, TOutput> where TInput : IRequest<TOutput>
    {
        protected readonly IUnitOfWork _unitOfWork;
        protected readonly IMapper _mapper;
        public BaseRequestHandler_Rich(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

        }
        public BaseRequestHandler_Rich(IUnitOfWork unitOfWork, IMapper mapper) : this(unitOfWork)
        {
            _mapper = mapper;
        }
        public async Task<TOutput> Handle(TInput request, CancellationToken cancellationToken)
        {
            return await HandleRequestAsync(request, cancellationToken);
        }
        protected abstract Task<TOutput> HandleRequestAsync(TInput input, CancellationToken cancellationToken);
    }
}