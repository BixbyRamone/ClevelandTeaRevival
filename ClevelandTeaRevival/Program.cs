using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using ClevelandTeaRevival.Data;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace ClevelandTeaRevival
{
    public class Program
    {
        public static void Main(string[] args)
        {
            // CreateWebHostBuilder(args).Build().Run();
            var host = CreateWebHostBuilder(args).Build();

            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                //var context = services.GetRequiredService<ApplicationDbContext>();
                //DbInitializer.Initialize(services, context).Wait();

                try
                {
                    var context = services.GetRequiredService<ApplicationDbContext>();
                    DbInitializer.Initialize(services, context).Wait();
                }
                catch (Exception ex)
                {
                    var logger = services.GetRequiredService<ILogger<Program>>();
                    logger.LogError(ex, "An error occurred while seeding the database.");
                }
            }

            host.Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>();
    }
}
