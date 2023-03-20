using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DeloreanTesting.Einstein
{
    /// <summary>
    /// work in progress
    /// </summary>
    /// <typeparam name="TStartup"></typeparam>
    public class StartupDecorator<TStartup>
    {
        public dynamic InnerStartup { get; }
        public IConfiguration Configuration { get; }

        private IServiceCollection TrackedServices { get; set; } = new ServiceCollection();

        public StartupDecorator(IConfiguration configuration)
        {
            InnerStartup = GetStartup(configuration);
            Configuration = configuration;
        }


        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //capture dependencies
            InnerStartup.ConfigureServices(TrackedServices);
            TrackeableDependenciesDictionary.StoreRegisteredServices(TrackedServices);

            //regisiter dependency tracker
            services.AddSingleton<IDependencyTracker, DependencyTracker>();

            //register middleware
            services.AddScoped<DependencyTrackerMiddleware>();

            //decorate dependencies with dependency tracker
            var servicesToIgnore = services.Select(s => s.ServiceType).ToList();
            InnerStartup.ConfigureServices(services);

            //var clonnedServices = services.Where(service => !servicesToIgnore.Contains(service.ServiceType)).Select(serviceDescriptor => serviceDescriptor).ToList();
            var depsUnderTest = TrackeableDependenciesDictionary.GetDependenciesUnderTest();

            var servicesToAddDecorator = services.FilterServicesBy(depsUnderTest);

            foreach (var item in servicesToAddDecorator)
            {
                services.AddDecorator(item.ServiceType, item.Lifetime);
            }
            var sp = services.BuildServiceProvider();
            var test = sp.GetService(servicesToAddDecorator.First().ServiceType);
            var tracker = sp.GetRequiredService<IDependencyTracker>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            InnerStartup.Configure(app, env);

            app.UseDependencyTrackerMiddleware();
        }

        protected TStartup GetStartup(params object[] args)
        {
            return (TStartup)Activator.CreateInstance(typeof(TStartup), args);
        }
    }
}
