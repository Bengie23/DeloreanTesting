
namespace WebApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
            //var builder = WebApplication.CreateBuilder(args);
            //var startup = new Startup(builder.Configuration);
            //startup.ConfigureServices(builder.Services);

            //var app = builder.Build();

            //startup.Configure(app, builder.Environment);

            //app.MapControllers();
            //app.Run();

        }
        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    bool shouldTriggerStartup = true;
                    webBuilder.ConfigureAppConfiguration(config =>
                    {
                        var configValues = config.Build();
                        var test = configValues["IsDeloreanIntegrationTestingSetup"];
                        shouldTriggerStartup = test is null;
                    });

                    if (shouldTriggerStartup)
                    {
                        webBuilder.UseStartup<Startup>();
                    }
                });
    }
}