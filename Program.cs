using ASPNETDemo1.Data;
using ASPNETDemo1.Endpoints;
using ASPNETDemo1.Entities;
using ASPNETDemo1.Repository;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc.Diagnostics;
using Microsoft.AspNetCore.Mvc.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRepository(builder.Configuration);
//store in user-secret


builder.Services.AddValidatorsFromAssemblyContaining<GameValidator>();

var app = builder.Build();
//Migrate database
await app.Services.InitializeDb();

app.MapGameEndpoints();

app.Run();