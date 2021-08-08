using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch.Exceptions;
using Microsoft.Extensions.Logging;

namespace ToDoApi.CustomExceptionMiddleware
{
    public class ErrorHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger _logger;

        public ErrorHandlingMiddleware(RequestDelegate next, ILogger logger)
        {
            _logger = logger;
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                _logger.LogError($"{ex}: {ex.Message} \r\n {ex.StackTrace}");
                await HandleExceptionAsync(context, ex);
            }
        }

        private static Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            var response = context.Response;
            response.ContentType = "application/json";

            response.StatusCode = exception switch
            {
                InvalidEnumArgumentException _ => (int)HttpStatusCode.BadRequest,
                ArgumentNullException _ => (int)HttpStatusCode.BadRequest,
                ArgumentOutOfRangeException _ => (int)HttpStatusCode.NotFound,
                InvalidOperationException _ => (int)HttpStatusCode.NotFound,
                JsonPatchException _ => (int)HttpStatusCode.BadRequest,
                _ => (int) HttpStatusCode.BadRequest
            };

            var result = JsonSerializer.Serialize(new {error = response.StatusCode, exception.Message});
            return response.WriteAsync(result);
        }
    }
}
