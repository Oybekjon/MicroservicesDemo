using MicroservicesDemo.Errors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroservicesDemo.WebApi
{
    public class ApiExceptionFilterAttribute : ExceptionFilterAttribute
    {

        public override void OnException(ExceptionContext actionExecutedContext)
        {
            // Default code is 500, i.e. if not changed further in the code, 
            // then this is not a user error but a server error
            var code = 500;

            if (actionExecutedContext.Exception is NotFoundException) // User error
            {
                code = 404; // HTTP for Not Found
            }
            else if (actionExecutedContext.Exception is DuplicateEntryException) // User error
            {
                code = 409; // HTTP Conflict code
            }
            else if (actionExecutedContext.Exception is AuthException) // User error
            {
                code = 401; // HTTP Code unauthroized
            }
            else if (actionExecutedContext.Exception is AccessDeniedException) // User error
            {
                code = 403; // HTTP Code Forbidden
            }
            else if (actionExecutedContext.Exception is ParameterInvalidException) // User error
            {
                code = 422; // HTTP Unprocessable entry
            }
            else if (
                actionExecutedContext.Exception is DebugException ||
                actionExecutedContext.Exception is WarningException
            ) // User errors
            {
                code = 400; // All other debug related errors - 400
            }

            actionExecutedContext.HttpContext.Response.StatusCode = code;
            actionExecutedContext.Result = new JsonResult(new
            {
                error = actionExecutedContext.Exception.Message
            });
        }
    }
}
