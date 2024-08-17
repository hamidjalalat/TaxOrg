using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Common
{
    public static class HttpContextExtension
    {

        public static string? IPAddress(this HttpContext context)
        {
            if (context == null)
                return "";
            if (context.Request.Headers.ContainsKey("X-Forwarded-For"))
                return context.Request.Headers["X-Forwarded-For"];
            else
                return context.Connection.RemoteIpAddress?.ToString();
        }
    }
}
