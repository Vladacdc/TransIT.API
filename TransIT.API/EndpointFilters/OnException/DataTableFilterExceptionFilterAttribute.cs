using System;
using System.Net;
using System.Threading.Tasks;
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
                Error = context.Exception.ToString()
            };
            context.Result = new ObjectResult(responseBody)
            {
                StatusCode = (int)HttpStatusCode.InternalServerError
            };
            context.ExceptionHandled = true;
            return Task.CompletedTask;
        }
    }
}
