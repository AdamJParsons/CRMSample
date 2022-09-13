using Serilog;
using CRMSample.Application.Identity.Services;
using CRMSample.Infrastructure.Identity.Services;
using CRMSample.Application.Identity.Data;
using Prometheus;
using MassTransit;
using CRMSample.Services.Identity.API.Consumers;
using CRMSample.Application.Common.Services;
using CRMSample.Infrastructure.Common.Services;

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
builder.Services.AddTransient<IDateTime, MachineDateTime>();

// Add MediatR pipeline
builder.Services.AddMediator();

builder.Services.AddAutoMapper(typeof(MappingProfile).Assembly);

builder.Services.AddEventBus(builder.Configuration, cfg =>
{
    cfg.AddConsumer<RegisterUserConsumer>();
});

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddApplicationInsightsTelemetry();

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
