using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Microsoft.Extensions.DependencyInjection;

using System.Net;
using Services.Helpers;
using ProjectBase.Core;


public class ExceptionHandlingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly IServiceScopeFactory _scopeFactory;

    public ExceptionHandlingMiddleware(RequestDelegate next, IServiceScopeFactory scopeFactory)
    {
        _next = next;
        _scopeFactory = scopeFactory;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
    
        catch (Exception ex)
        {

            var errorPath =
                $"  Error occurred in {context.Request.Path}";
            var queryParams = string.Join("&", context.Request.Query.Select(q => $"{q.Key}={q.Value}"));
            var errorMessage =
               $"{ex.Message}";
            using var scope = _scopeFactory.CreateScope();
            LogHelper.LogException("Integration", ex, $"{errorPath}?{queryParams}");

            context.Response.StatusCode = StatusCodes.Status500InternalServerError;
            context.Response.ContentType = "application/json";

            var response = new
            {
                StatusCode = (int)HttpStatusCode.InternalServerError,
                Status = false,
                Message = errorMessage,
                error = $"{errorPath}?{queryParams}"

            };
            var jsonResponse = JsonConvert.SerializeObject(response);

            await context.Response.WriteAsync(jsonResponse);
        }
    }
}


