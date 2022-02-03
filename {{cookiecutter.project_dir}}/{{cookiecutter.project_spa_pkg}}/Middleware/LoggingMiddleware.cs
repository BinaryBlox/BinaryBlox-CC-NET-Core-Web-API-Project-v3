

using System; 
using System.Collections.Generic; 
using System.IO;
using System.Linq;
using System.Reflection;
using System.Security.Claims;
using System.Text; 
using System.Threading.Tasks;
using System.Web; 

using Serilog;
using Serilog.Events;
using Serilog.Sinks.SystemConsole.Themes;
using AutoMapper;

using IdentityModel;

using Microsoft.IdentityModel.Logging;
using Microsoft.AspNetCore.Authentication; 
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting; 
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Mvc.Versioning; 
using Microsoft.AspNetCore.Routing;
using Microsoft.AspNetCore.Routing.Patterns;
using Microsoft.AspNetCore.SpaServices.ReactDevelopmentServer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.PlatformAbstractions;
using Microsoft.OpenApi.Models;

#pragma warning disable 1591
namespace {{cookiecutter.project_spa_pkg}}.Middleware
{
    public static class LoggingMiddleware
    {
       
        public static IServiceCollection AddLoggingServices(this IServiceCollection services,  IConfiguration configuration)
        { 
            var showPII = configuration.GetValue<bool>("Logging:ShowPII");
            var logLevel = configuration.GetValue<string>("Logging:Level");
            var fileName = configuration.GetValue<string>("Logging:Filename");
            var filenameSuffixFormat = configuration.GetValue<string>("Logging:FilenameSuffixFormat");

            Log.Logger = BuildLogger(logLevel, fileName, filenameSuffixFormat, showPII).CreateLogger();

            // Indicate if Private information is being saved to logs.
            Log.Debug($"Show PII Information: {IdentityModelEventSource.ShowPII.ToString()}");

             return services;

        }


        public static LoggerConfiguration BuildLogger(string logLevel, string fileName, string suffixFormat, bool showPII)
        {


            logLevel = (logLevel != null) ? logLevel.ToLower() : "";
            fileName = $"{fileName}_{DateTime.Now.ToString(suffixFormat)}";

            // Show PII (Private) information in logging
            IdentityModelEventSource.ShowPII = showPII;

            LoggerConfiguration loggingConfig = new LoggerConfiguration();

            switch (logLevel.ToLower())
            {

                case "debug":
                    loggingConfig.MinimumLevel.Debug();
                    break;

                case "error":
                    loggingConfig.MinimumLevel.Error();
                    break;

                case "fatal":
                    loggingConfig.MinimumLevel.Fatal();
                    break;

                case "info":
                    loggingConfig.MinimumLevel.Information();
                    break;

                case "verbose":
                    loggingConfig.MinimumLevel.Verbose();
                    break;

                default:
                    loggingConfig.MinimumLevel.Debug();
                    break;

            }

            // Additional configuration
            loggingConfig.MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
                .MinimumLevel.Override("System", LogEventLevel.Warning)
                //.MinimumLevel.Override("Microsoft.AspNetCore.Authentication", LogEventLevel.Information)
                .Enrich.FromLogContext()
                .WriteTo.File($@"Logs/{fileName}.log", LogEventLevel.Verbose, @"[{Timestamp:HH:mm:ss} {Level}] {SourceContext}{NewLine}Message: {Message}{MemberDetail}{NewLine}{Exception}")
                .WriteTo.Console(outputTemplate: @"[{Timestamp:HH:mm:ss} {Level}] {SourceContext}{NewLine}Detail{NewLine}{MemberDetail}{NewLine}{Exception}{NewLine}", theme: AnsiConsoleTheme.Literate);
                //.WriteTo.Console(outputTemplate: "[{Timestamp:HH:mm:ss} {Level}] {SourceContext}{NewLine}{Message:lj}{NewLine}{Exception}{NewLine}", theme: AnsiConsoleTheme.Literate);

            return loggingConfig;

        } 
    }
}

