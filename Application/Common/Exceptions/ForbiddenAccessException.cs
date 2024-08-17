using Resources.Messages;
using System.Net;

namespace Application.Common.Exceptions
{
    public class ForbiddenAccessException : Exception
    {
        public ForbiddenAccessException(string message )
            : base(message) { }
    }
}