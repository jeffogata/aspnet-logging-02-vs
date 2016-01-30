namespace aspnet_logging_02_vs
{
    using System.IO;
    using Microsoft.AspNet.Builder;
    using Microsoft.AspNet.Http;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Logging;
    using Microsoft.Extensions.PlatformAbstractions;
    using Serilog;
    using Serilog.Events;
    using Serilog.Sinks.RollingFile;

    public class Startup
    {
        public Startup(IApplicationEnvironment appEnv)
        {
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .WriteTo.RollingFile(
                    Path.Combine(appEnv.ApplicationBasePath, "log-{Date}.txt"), 
                    LogEventLevel.Debug)
                .WriteTo.LiterateConsole(LogEventLevel.Information)
                .CreateLogger();
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddScoped<MyClass>();
        }

        public void Configure(IApplicationBuilder app, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddSerilog();

            app.UseIISPlatformHandler();

            app.Run(async context =>
            {
                var myClass = context.RequestServices.GetService<MyClass>();

                myClass.DoSomething(1);
                myClass.DoSomething(20);
                myClass.DoSomething(-20);

                await context.Response.WriteAsync("Hello World!");
            });
        }
    }
}