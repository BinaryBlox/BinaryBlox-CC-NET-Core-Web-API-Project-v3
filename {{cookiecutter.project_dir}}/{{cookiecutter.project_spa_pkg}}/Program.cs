using System.Collections.Generic;

using Microsoft.AspNetCore.Hosting;

using BinaryBlox.SDK.Utils;

using {{cookiecutter.project_spa_pkg}}.Constants;


namespace {{cookiecutter.project_spa_pkg}}
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
            //CreateWebHostBuilder(args).Build().Run();    
            CoreUtilities.CreateDefaultBinaryBloxBuilder("{{cookiecutter.project_spa_pkg}}", 
            ApiEndpointConstants.{{cookiecutter.project_spa_const_pfx|upper}}_ENDPOINT_DEV, 
            defaults,
            args)
                .UseStartup<Startup>()
                .Build()
                .Run();
                
        }
    }
} 