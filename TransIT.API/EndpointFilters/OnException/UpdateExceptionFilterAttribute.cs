using System;
using System.Data;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using TransIT.BLL.DTOs;

namespace TransIT.API.EndpointFilters.OnException
{
    public class UpdateExceptionFilterAttribute : Attribute, IAsyncExceptionFilter
    {
        public Task OnExceptionAsync(ExceptionContext context)
        {
            var exceptionType = context.Exception.GetType();
            var result = new ObjectResult(
                new ExtendedErrorDTO(context.Exception)
            );

            result.StatusCode =
                exceptionType == typeof(ArgumentException)
                || exceptionType == typeof(ConstraintException)
                    ? StatusCodes.Status409Conflict
                    : StatusCodes.Status500InternalServerError;

            context.Result = result;
            context.ExceptionHandled = true;
            return Task.CompletedTask;
        }
    }
}
