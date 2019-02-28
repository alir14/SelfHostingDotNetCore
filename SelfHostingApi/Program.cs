using System;
using System.Threading.Tasks;
using HosingConsole.Config;
using HosingConsole.MyTask;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;

namespace SelfHostingApi
{
    class Program
    {
        private static IConfiguration Configuration { get; set; }
        private static IServiceProvider serviceProvider;

        static void Main(string[] args)
        {
            LoadConfiguration();

            serviceProvider = ConfigureService();

            Task.Run(()=> serviceProvider.GetService<IBGTask>().Run());

            CreateWebHostBuilder(args).Build().Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
        WebHost.CreateDefaultBuilder(args)
            .UseStartup<Program>();

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            var routeBuiledr = new RouteBuilder(app);

            routeBuiledr.MapGet("api/getState", HandleStateGetCall);

            routeBuiledr.MapGet("api/init/{day:int}/{month:int}/{year:int}/{offset:int}/{fetch:int}", HandleRuninitialLoadingCall);

            app.UseRouter(routeBuiledr.Build());
        }

        private async Task HandleStateGetCall(HttpRequest requet, HttpResponse response, RouteData data)
        {
            var status = serviceProvider.GetService<IStateManager>().ServiceState;
            
            await response.WriteAsync($"data: {JsonConvert.SerializeObject(requet.QueryString)} " +
                $"and Service Status is = {JsonConvert.SerializeObject(status)}");
        }

        private async Task HandleRuninitialLoadingCall(HttpRequest requet, HttpResponse response, RouteData data)
        {
            var status = serviceProvider.GetService<IStateManager>().ServiceState;

            await response.WriteAsync($"data: {JsonConvert.SerializeObject(data.Values.Values)} " +
                $"and Service Status is = {JsonConvert.SerializeObject(status)}");
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddRouting();
        }

        private static IServiceProvider ConfigureService()
        {
            var serviceCollection = new ServiceCollection();

            serviceCollection.AddSingleton<IStateManager, StateManager>();

            serviceCollection.AddSingleton<IBGTask, BGTask>();

            serviceCollection.AddOptions<CompilerOptions>();

            return serviceCollection.BuildServiceProvider();
        }

        private static void LoadConfiguration()
        {
            Configuration = new ConfigurationBuilder()
                .SetBasePath(AppContext.BaseDirectory)
                .AddJsonFile("appsettings.json", false, false)
                .AddEnvironmentVariables()
                .Build();
        }
    }
}
