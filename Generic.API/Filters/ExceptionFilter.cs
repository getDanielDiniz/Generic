using Generic.Comunication.DTO_s.Response;
using Generic.Exception.BaseExceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Generic.API.Filters
{
    public class ExceptionFilter : IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            if (context.Exception is GenericBaseException exception)
            {
                HandleGenericBaseException(context, exception);
                return;
            }
            HandleGenericException(context);

        }

        private void HandleGenericBaseException(ExceptionContext context, GenericBaseException exception)
        {
            var response = new ResponseErrorJson(exception.Errors);
            context.HttpContext.Response.StatusCode = (int)exception.StatusCode;
            context.Result = new ObjectResult(response);
        }

        private void HandleGenericException(ExceptionContext context)
        {
            var response = new ResponseErrorJson(["An unexpected error occurred."]);
            context.HttpContext.Response.StatusCode = 500;
            context.Result = new ObjectResult(response);
        }
    }
}
