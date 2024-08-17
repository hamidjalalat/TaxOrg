using Application.Common.Exceptions;
using FluentValidation.Results;
using MediatR;
using Microsoft.Extensions.Logging;
using Nazm.Results;

namespace Application.Common.Behaviours
{
    public class UnhandledExceptionBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse>
    {
        private readonly ILogger<TRequest> _logger;

        public UnhandledExceptionBehaviour(ILogger<TRequest> logger)
        {
            _logger = logger;
        }

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            try
            {
                return await next();
            }
            catch (ValidationException errorList)
            {
                //var type = typeof(TResponse);
                //var res = new FluentResults.Result();
                //foreach (var item in errorList.Errors)
                //{
                //    res.WithErrors(item.Value);
                //}
                //var s = res.ConvertToDtatResult();
                //return (TResponse)s;
                throw;
            }
            catch (Exception ex)
            {
                var requestName = typeof(TRequest).Name;

                _logger.LogError(ex, $" Request: Unhandled Exception for Request {requestName} {request}");

                throw;
            }
        }
    }
}