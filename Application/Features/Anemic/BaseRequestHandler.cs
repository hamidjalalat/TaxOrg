using Application.Common.Interfaces.Repository.Anemic.Base;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Application.Features.Anemic
{
    public abstract class BaseRequestHandler<TInput, TOutput> : IRequestHandler<TInput, TOutput> where TInput : IRequest<TOutput>
    {
        protected readonly IUnitOfWork _unitOfWork;

        protected readonly IMapper _mapper;
        public BaseRequestHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

        }
        public BaseRequestHandler(IUnitOfWork unitOfWork, IMapper mapper) : this(unitOfWork)
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