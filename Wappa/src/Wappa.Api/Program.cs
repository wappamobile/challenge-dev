using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;

namespace Wappa.Api
{
    /// <summary>
    ///  Programa principal
    /// </summary>
    public class Program
    {
        /// <summary>
        /// Constructor
        /// </summary>
        protected Program()
        {
        }

        /// <summary>
        /// Método principal
        /// </summary>
        /// <param name="args"></param>
        public static void Main(string[] args)
        {
            CreateWebHostBuilder(args).Build().Run();
        }

        /// <summary>
        /// Construtor do WebHost
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>();
    }
}