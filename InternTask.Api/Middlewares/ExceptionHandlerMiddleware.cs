﻿using InternTask.Api.Models;
using InternTask.Service.Exceptions;

namespace InternTask.Api.Middlewares;
public class ExceptionHandlerMiddleware
{
    private readonly RequestDelegate next;
    private readonly ILogger<ExceptionHandlerMiddleware> logger;

    public ExceptionHandlerMiddleware(RequestDelegate next, ILogger<ExceptionHandlerMiddleware> logger)
    {
        this.next = next;
        this.logger = logger;
    }

    public async Task Invoke(HttpContext context)
    {
        try
        {
            await next(context);
        }
        catch (InternTaskException exception)
        {
            context.Response.StatusCode = exception.Code;
            await context.Response.WriteAsJsonAsync(new Response
            {
                Code = exception.Code,
                Error = exception.Message
            });
        }
        catch (Exception exception)
        {
            this.logger.LogError($"{exception}\n\n");
            context.Response.StatusCode = 500;
            await context.Response.WriteAsJsonAsync(new Response
            {
                Code = 500,
                Error = exception.Message
            });
        }
    }
}
