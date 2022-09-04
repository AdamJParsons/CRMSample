using Serilog;
using MediatR;
using MediatR.Pipeline;
using CRMSample.Application.Identity.Account.Commands;
using CRMSample.Infrastructure.Common.Mediatr;
using CRMSample.Application.Identity.Services;
using CRMSample.Infrastructure.Identity.Services;
using CRMSample.Domain.Identity.Entities.Account;
using Microsoft.AspNetCore.Identity;
using CRMSample.Infrastructure.Identity.Persistence;
using Microsoft.EntityFrameworkCore;
using CRMSample.Application.Identity.Data;
using k8s.KubeConfigModels;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Prometheus;
using HealthChecks.UI.Client;
using Microsoft.Extensions.Hosting;
using CRMSample.Infrastructure.Common.Settings;
using Microsoft.AspNetCore.Authentication.JwtBearer;

var builder = WebApplication.CreateBuilder(args);

// Setup serilog logging
builder.Host.AddSerilog();

// Add health checks
builder.Services.AddCustomHealthCheck(builder.Configuration, "CRM Sample - Identity API");

// Add DbContext
builder.Services.AddCustomDbContext();

// Add Identity
builder.Services.AddCustomIdentity();

// Add authentication and the token service
builder.Services.AddAuthentication(builder.Configuration);
builder.Services.AddTransient<ILoginService, EFLoginService>();
builder.Services.AddTransient<ITokenService, JwtTokenService>();

// Add MediatR pipeline
builder.Services.AddMediator();

builder.Services.AddAutoMapper(typeof(MappingProfile).Assembly);

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

await app.UseSeedDataAsync();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.UseSerilogRequestLogging();

// setup prometheus
app.UseMetricServer();
app.UseHttpMetrics();

// setup healthchecks
app.UseCustomHealthChecks();

app.Run();
