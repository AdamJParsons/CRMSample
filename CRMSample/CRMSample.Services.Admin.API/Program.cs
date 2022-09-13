using CRMSample.Application.Admin.Data;
using CRMSample.Application.Admin.Services;
using CRMSample.Application.Common.Services;
using CRMSample.Infrastructure.Admin.Services;
using CRMSample.Infrastructure.Common.Faults;
using CRMSample.Infrastructure.Common.Services;
using MassTransit;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Prometheus;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Setup serilog logging
builder.Host.AddSerilog();

// Add health checks
builder.Services.AddCustomHealthCheck(builder.Configuration, "CRM Sample - Admin API");

// Add DbContext
builder.Services.AddCustomDbContext();

// Add Authentication
builder.Services.AddAuthentication(builder.Configuration);

// Add MediatR pipeline
builder.Services.AddMediator();

builder.Services.AddAutoMapper(typeof(MappingProfile).Assembly);

// Add services to the container.
builder.Services.AddTransient<IUserService, EFUserService>();
builder.Services.AddTransient<IDateTime, MachineDateTime>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Add the event bus (MassTransit using RabbitMQ or Azure Service Bus)
builder.Services.AddEventBus(builder.Configuration, cfg => { });

// Add the AppInsights telemetry to report app metrics to Azure
builder.Services.AddApplicationInsightsTelemetry();

// Add fault policy handler
builder.Services.TryAddSingleton<ClientPolicy>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    await app.UseSeedDataAsync();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.UseSerilogRequestLogging();

// setup prometheus
app.UseMetricServer();
app.UseHttpMetrics();

// setup healthchecks
app.UseCustomHealthChecks();

app.Run();
