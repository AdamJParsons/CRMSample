using Microsoft.Extensions.Hosting;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Microsoft.AspNetCore.Builder
{
    public static class IHostBuilderExtensions
    {
        public static IHostBuilder AddSerilog(this IHostBuilder builder)
        {
            builder.UseSerilog((context, config) =>
            {
                config
                .Enrich.FromLogContext()
                .WriteTo.Console(Serilog.Events.LogEventLevel.Debug)
                .Enrich.WithProperty("Environment", context.HostingEnvironment.EnvironmentName)
                .ReadFrom.Configuration(context.Configuration);
            });

            return builder;
        }
    }
}

