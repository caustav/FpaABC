using Application.Common;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.VisualBasic;
using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Threading.Tasks;

namespace Application
{
	public class ApplicationMiddleware : IMiddleware
    {
		public ILogger<ApplicationMiddleware> Logger { get; }

		public ApplicationMiddleware(ILogger<ApplicationMiddleware> logger)
		{
			Logger = logger;
		}

		public async Task InvokeAsync(HttpContext context, RequestDelegate next)
		{
			try
			{
				await next(context);
			}
			catch (InvoiceIncompleteException ex)
			{
				Logger.LogError(ex.Message);
				Logger.LogError(ex.StackTrace);
				Logger.LogDebug(ex.InnerException?.Message);
				context.Response.StatusCode = (int)HttpStatusCode.Forbidden;
				await context.Response.WriteAsync(new Error((int)HttpStatusCode.Forbidden, ex.Message).ToString());				
			}
		}
    }
}
