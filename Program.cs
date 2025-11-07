using System.Diagnostics;
using ASPNETDemo1.Authorization;
using ASPNETDemo1.Cors;
using ASPNETDemo1.Data;
using ASPNETDemo1.Endpoints;
using ASPNETDemo1.Entities;
using ASPNETDemo1.ErrorHandling;
using ASPNETDemo1.Middlewares;
using ASPNETDemo1.Repository;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc.Diagnostics;
using Microsoft.AspNetCore.Mvc.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRepository(builder.Configuration);
//store in user-secret

builder.Services.AddAuthentication().AddJwtBearer();
builder.Services.AddGameStoreAuthorization();
builder.Services.AddApiVersioning(options =>
{
    options.DefaultApiVersion = new(1.0);
    options.AssumeDefaultVersionWhenUnspecified = true;
});
builder.Logging.AddJsonConsole(options =>
{
    options.JsonWriterOptions = new()
    {
        Indented = true
    };
});

builder.Services.AddValidatorsFromAssemblyContaining<GameValidator>();

builder.Services.AddGameStoreCors(builder.Configuration);

var app = builder.Build();
//Migrate database
await app.Services.InitializeDb();


app.UseExceptionHandler(exceptionHandlerApp => exceptionHandlerApp.ConfigureExceptionHandler());

app.UseMiddleware<RequestTimingMiddleware>();
app.UseHttpLogging();

app.UseCors();

app.MapGameEndpoints();

app.Run();