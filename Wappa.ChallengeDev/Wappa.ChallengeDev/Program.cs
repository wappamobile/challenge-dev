using System;
using System.IO;
using System.Net;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;

namespace Wappa.ChallengeDev
{
    public class Program
    {
        public static string Port { get; set; }

        public static void Main(string[] args)
        {
            var builder = new ConfigurationBuilder()
                       .SetBasePath(Directory.GetCurrentDirectory())
                       .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);


            var config = builder.Build();

            Port = config["Host:Port"];

            BuildWebHost(args).Run();
        }

        public static IWebHost BuildWebHost(string[] args) =>
           WebHost.CreateDefaultBuilder(args)
                 .UseKestrel(options =>
                 {
                     options.Listen(IPAddress.Loopback, Convert.ToInt32(Port));
                 })
                 .UseStartup<Startup>()
                   .Build();
    }
}

