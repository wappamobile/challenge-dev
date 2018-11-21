using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;

namespace ChallengeDev
{
    /// <summary>
    /// Application Execution Class
    /// </summary>
    public class Program
    {
        /// <summary>
        /// Application Execution Method
        /// </summary>
        public static void Main(string[] args)
        {
            CreateWebHostBuilder(args).Build().Run();
        }

        /// <summary>
        /// Using the Startup option
        /// </summary>
        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>();
    }
}
