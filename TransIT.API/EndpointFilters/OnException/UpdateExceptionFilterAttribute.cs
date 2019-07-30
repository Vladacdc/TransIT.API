using System;
using System.Data;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace TransIT.API.EndpointFilters.OnException
{
    public class UpdateExceptionFilterAttribute : Attribute, IAsyncExceptionFilter
    {
        public Task OnExceptionAsync(ExceptionContext context)
        {
            var exceptionType = context.Exception.GetType();
            var result = new ContentResult();

            result.Content = context.Exception.Message;
            result.StatusCode =
                exceptionType == typeof(ArgumentException)
                || exceptionType == typeof(ConstraintException)
                    ? StatusCodes.Status409Conflict
                    : StatusCodes.Status400BadRequest;

            context.Result = result;
            context.ExceptionHandled = true;
            return Task.CompletedTask;
        }
    }
}
