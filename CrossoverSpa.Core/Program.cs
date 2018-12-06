
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;


namespace CrossoverSpa.Core
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateWebHostBuilder(args).Build().Run();

            //BuildWebHost(args).Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>();

        //public static IWebHost BuildWebHost(string[] args)
        //{
        //    return WebHost.CreateDefaultBuilder(args)
        //        .UseStartup<Startup>()
        //        .UseDefaultServiceProvider(options =>
        //            options.ValidateScopes = false)
        //        .Build();
        //}
    }
}
