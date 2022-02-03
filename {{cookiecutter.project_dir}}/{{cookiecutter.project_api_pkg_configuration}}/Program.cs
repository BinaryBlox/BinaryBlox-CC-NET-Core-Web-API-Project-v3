using System.Collections.Generic;

using Microsoft.AspNetCore.Hosting;
 
using BinaryBlox.SDK.Utils;
    
using {{cookiecutter.project_api_pkg_configuration}}.Constants;

namespace {{cookiecutter.project_api_pkg_configuration}}
{   
    /// <summary>
    /// 
    /// </summary>
    public class Program
    {
        private static readonly Dictionary<string, string> defaults =
        new Dictionary<string, string> {
            { WebHostDefaults.EnvironmentKey, "development" }
        };

        /// <summary>
        /// 
        /// </summary>
        /// <param name="args"></param>
        public static void Main(string[] args)
        {  
            CoreUtilities.CreateDefaultBinaryBloxBuilder("{{cookiecutter.project_api_pkg_configuration}}", 
            ApiEndpointConstants.{{cookiecutter.project_api_const_pfx_configuration|upper}}_ENDPOINT_DEV, 
            defaults,
            args)
                .UseStartup<Startup>()
                .Build()
                .Run();
                
        }
    }
} 