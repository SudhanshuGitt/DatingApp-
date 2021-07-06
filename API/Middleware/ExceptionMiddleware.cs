using System;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;
using API.Errors;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace API.Middleware
{
  public class ExceptionMiddleware
  { // Request delegate is what next whats coming next in middleware pipeline
    // we will use ILogger to display our exception in the terminal
    // we will also check which enviorment we are running in 
    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionMiddleware> _logger;
    private readonly IHostEnvironment _env;
    public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger, IHostEnvironment env)
    {
      _env = env;
      _logger = logger;
      _next = next;
    }

    // we are going to give this middleware its required method
    // when we add middleware we will have access to Http Request
    public async Task InvokeAsync(HttpContext context)
    {
      try
      {
        // we are passing our context to next piece of middleware
        // this middleware will live at very top we may have lot of middleware below this middleware
        // if any of the middleware get the excpetion they will through the excpetion until someone handles it
        await _next(context);
      }
      catch(Exception ex)
      {
        // we are logging the exception
        _logger.LogError(ex, ex.Message);
        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

        var response = _env.IsDevelopment() ?
        new ApiException(context.Response.StatusCode, ex.Message, ex.StackTrace?.ToString())
        : new ApiException(context.Response.StatusCode, "Internal Server Error");

        // this will make sure our resoponse just goes back as the normal Json formatted response in Camel Case
        var options = new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };

        var json = JsonSerializer.Serialize(response, options);

        await context.Response.WriteAsync(json);
      }
    }
  }
}