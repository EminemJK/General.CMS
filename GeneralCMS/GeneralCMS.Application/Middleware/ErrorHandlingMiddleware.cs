using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using GeneralCMS.Common.LogUtility;
using Microsoft.AspNetCore.Http;

namespace GeneralCMS.Application.Middleware
{
    public class ErrorHandlingMiddleware
    {
        private readonly ILoggerHelper log;
        private readonly RequestDelegate next;

        public ErrorHandlingMiddleware(ILoggerHelper log, RequestDelegate next)
        {
            this.log = log;
            this.next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await next(context);
            }
            catch (Exception ex)
            {
                var statusCode = context.Response.StatusCode;
                if (ex is ArgumentException)
                {
                    statusCode = 200;
                }
                log.Error(context, "中间件捕获异常", ex);
                await next(context);
            } 
        } 
    }
}
