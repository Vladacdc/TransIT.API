using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using TransIT.BLL.DTOs;

namespace TransIT.API.EndpointFilters.OnException
{
    public class DataTableFilterExceptionFilterAttribute : Attribute, IAsyncExceptionFilter
    {
        public Task OnExceptionAsync(ExceptionContext context)
        {
            var responseBody = new DataTableResponseDTO
            {
                Error = context.Exception.Message,
                ServerErrorInfo = new ExtendedErrorDTO(context.Exception)
            };
            context.Result = new ObjectResult(responseBody)
            {
                StatusCode = StatusCodes.Status500InternalServerError
            };
            context.ExceptionHandled = true;
            return Task.CompletedTask;
        }
    }
}
