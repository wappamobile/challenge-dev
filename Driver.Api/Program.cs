using Driver.Application;
using Driver.Application.Services;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;

namespace Driver.Api
{
    /// <summary>
    /// Inicialização do sistema
    /// </summary>
    public class Program
    {
        /// <summary>
        /// Main
        /// </summary>
        /// <param name="args"></param>
        public static void Main(string[] args)
        {
            CreateWebHostBuilder(args).Build().Run();
        }

        /// <summary>
        /// Método responsável responsável por criar um webhost
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseContentRoot(AppSettings.BasePath)
                .UseUrls(AppSettings.BindingUrl)
                .UseStartup<Startup>();
    }
}