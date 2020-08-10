using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace ProcedimientosAlmacenadosAPInetCore
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureAppConfiguration((env, config) =>
                {
                    //env => nombre del ambiente
                    //config => variable sobre la cual se realiza la configuracion de proveedores
                    //aqui se coloca la configuracion de proveedores
                    var ambiente = env.HostingEnvironment.EnvironmentName;
                    config.AddJsonFile($"appsettings.{ambiente}.json", optional: true, reloadOnChange: true);
                    config.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
                    // configurar variables de entorno
                    config.AddEnvironmentVariables();

                    if (args != null)
                    {
                        config.AddCommandLine(args);
                    }

                    //provedor azure key Vault
                    var currentConfig = config.Build();

                    config.AddAzureKeyVault(
                        currentConfig["Vault"],
                        currentConfig["ClientId"],
                        currentConfig["ClientSecret"]);

                })
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
