using DeloreanTesting.Einstein;
using Newtonsoft.Json;
using WebApi.Repositories;
using WebApi.Services;

namespace WebApi.IntegrationTests
{
    [EinsteinTestClass]
    public class UnitTest1 : BaseIntegrationTests
    {

        [ClassInitialize]
        public static async Task Initialize(TestContext context)
        {
            //overwrites configuration values from appsettings.test.json
            configValues = new Dictionary<string, string>() { { "DeloreanTestingSetup", "true" } };
            await Initialize();
        }

        [ClassCleanup]
        public static void CleanUp()
        {
            Dispose();
        }

        [EinsteinTestMethod]
        public async Task TestMethod1()
        {
            ///Assert WeatherForecast values
            var response = await client.GetAsync("/WeatherForecast");
            var stringResult = await response.Content.ReadAsStringAsync();
            
            var objectResult = JsonConvert.DeserializeObject<List<WeatherForecast>>(stringResult);
            Assert.AreEqual(5, objectResult.Count);

            ///---------------------------------------------------------------------------------------

            //Assert Dependencies registration
            Assert.That.DependencyHasBeenRegistered(nameof(WeatherForecastRepository));
        }

        [EinsteinTestMethod]
        [DependenciesUnderTest(
            nameof(WeatherForecastService),
            nameof(WeatherForecastRepository))]
        public void TestDependencyRegistration(string[] dependencies)
        {            
            Assert.That.DependenciesHaveBeenRegistered(dependencies);
        }
    }
}