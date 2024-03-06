using Logistics.Application.Logs;
using Logistics.Domain.Settings.ErrorHandler;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Data.SqlClient;

namespace Logistics.Application.Tools.ExceptionHandler
{
    [AllowAnonymous]
    [ApiExplorerSettings(IgnoreApi = true)]
    public class ExceptionHandlerController : ControllerBase
    {
        private readonly IHostEnvironment _hostEnvironment;
        private readonly ILogger<ExceptionHandlerController> _logger;
        public ExceptionHandlerController(IHostEnvironment hostEnvironment, ILogger<ExceptionHandlerController> logger)
        {
            _hostEnvironment = hostEnvironment;
            _logger = logger;
        }

        [Route("error")]
        public ErrorResponse HandleException()
        {
            IExceptionHandlerFeature context = HttpContext.Features.Get<IExceptionHandlerFeature>();
            Exception exception = context.Error;

            if (exception is HttpException httpException)
            {
                Response.StatusCode = (int)httpException.StatusCode;

                return new ErrorResponse(httpException.Errors);
            }

            if (_hostEnvironment.IsDevelopment() == false)
            {
                if (exception is DbUpdateException)
                {
                    Response.StatusCode = 500;
                    Log.GravaLog("Exception", $"Exception: {exception.Message}, InnerException {exception.InnerException}");
                    return new ErrorResponse("Não foi possível finalizar o processo. Por favor, entre em contato com o suporte.");
                }
                else if (exception is SqlException)
                {
                    Response.StatusCode = 500;
                    Log.GravaLog("Exception", $"Exception: {exception.Message}, InnerException {exception.InnerException}");
                    return new ErrorResponse("Não foi possível finalizar o processo. Por favor, entre em contato com o suporte.");
                }
                else if (exception is FormatException)
                {
                    Response.StatusCode = 500;
                    Log.GravaLog("Exception", $"Exception: {exception.Message}, InnerException {exception.InnerException}");
                    return new ErrorResponse("Não foi possível finalizar o processo. Por favor, entre em contato com o suporte.");
                }
            }

            Response.StatusCode = 500;
            Log.GravaLog("Exception", $"Exception: {exception.Message}, InnerException: {exception.InnerException}");
            return new ErrorResponse(exception.Message);
        }
    }
}
