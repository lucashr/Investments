using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Investments.Application.Dtos;
using Microsoft.AspNetCore.Http;
using Microsoft.Data.Sqlite;
using Newtonsoft.Json;
using OpenQA.Selenium;

namespace Investments.API.Middlewares
{
    public class ErrorHandlerMiddleware
    {
        private readonly RequestDelegate _next;

        public ErrorHandlerMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {

            var response = context.Response;
            response.ContentType = "application/json";
            var statusCode = HttpStatusCode.InternalServerError; // Default to 500

            var message = "An unexpected error occurred.";
            var details = exception.Message;

            // Handling different types of exceptions
            switch (exception)
            {
                case ArgumentNullException _:
                    statusCode = HttpStatusCode.BadRequest; // 400 - Bad Request
                    message = "A required argument was null.";
                    break;
                    
                case ArgumentException _:
                    statusCode = HttpStatusCode.BadRequest; // 400 - Bad Request
                    message = "An argument is invalid.";
                    break;

                case UnauthorizedAccessException _:
                    statusCode = HttpStatusCode.Unauthorized; // 401 - Unauthorized
                    message = "You are not authorized to perform this action.";
                    break;

                case InvalidOperationException _:
                    statusCode = HttpStatusCode.Conflict; // 409 - Conflict
                    message = "There was a conflict with the operation.";
                    break;

                case SqliteException _:
                    statusCode = HttpStatusCode.InternalServerError; // 500 - Internal Server Error
                    message = "A database error occurred.";
                    break;

                case NotFoundException _:
                    statusCode = HttpStatusCode.NotFound; // 404 - Not Found
                    message = "The requested resource was not found.";
                    break;

                default:
                    statusCode = HttpStatusCode.InternalServerError; // 500 - Internal Server Error
                    message = "An unexpected error occurred.";
                    break;
            }

            // Send the response to the client
            context.Response.StatusCode = (int)statusCode;
            var result = JsonConvert.SerializeObject(new { error = message, details = details });
            
            await context.Response.WriteAsync(result);

        }

    }

}