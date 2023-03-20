using DeloreanTesting.Einstein;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using System.Reflection;

namespace WebApi.IntegrationTests
{
    [TestClass]
    public abstract class BaseIntegrationTests
    {
        protected static IHost host { get; private set; }
        protected static HttpClient client { get; private set; }
        protected static TestServer server { get; private set; }

        protected static Dictionary<string, string> configValues { get; set; }

        protected static async Task Initialize()
        {
            var _configuration = new ConfigurationBuilder()
           .AddJsonFile("appsettings.test.json", true)
           .AddInMemoryCollection(configValues)
           .Build();

            var hostBuilder = new HostBuilder()
                .ConfigureWebHost(webHost =>
                {

                    webHost.UseTestServer()
                    .UseEnvironment("Test")
                    .UseConfiguration(_configuration)
                    .UseStartup<StartupDecorator<Startup>>()
                    // Ignore the startup class assembly as the "entry point" and instead point it to this app
                    .UseSetting(WebHostDefaults.ApplicationKey, typeof(Program).GetTypeInfo().Assembly.FullName);
                });

            host = await hostBuilder.StartAsync();

            client = host.GetTestClient();

            server = host.GetTestServer();
        }

        protected static void Dispose()
        {

            host?.Dispose();
            host = null;

            client?.Dispose();
            client = null;

            server?.Dispose();
            server = null;


        }
    }
}
