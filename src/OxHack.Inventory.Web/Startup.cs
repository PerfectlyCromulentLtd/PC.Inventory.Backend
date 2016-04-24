using Microsoft.AspNet.Builder;
using Microsoft.AspNet.Hosting;
using Microsoft.AspNet.Mvc.Infrastructure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using OxHack.Inventory.Cqrs;
using OxHack.Inventory.EventStore;
using OxHack.Inventory.Query;
using OxHack.Inventory.Query.Sqlite;
using OxHack.Inventory.Services;
using OxHack.Inventory.Web.Services;

namespace OxHack.Inventory.Web
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            // Set up configuration sources.
            var builder = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration
        {
            get; set;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Add framework services.
            services.AddMvc();

            services.AddSingleton<IConfiguration>(sp => this.Configuration);
            services.AddSingleton<EncryptionService>();

            services.AddEventStore(this.Configuration);

            var provider = services.BuildServiceProvider();

            var eventStore = provider.GetService<IEventStore>();
            var bus = new InMemoryBus(eventStore);
            services.AddSingleton<IBus, InMemoryBus>(sp => bus);

            services.RegisterRepositories(this.Configuration);
            services.AddDomainServices();
            
            services.RegisterCommandHandlers();
            services.RegisterQueryModelEventHandlers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            if (env.IsDevelopment())
            {
                app.UseBrowserLink();
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseIISPlatformHandler();

            app.UseStaticFiles();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }

        // Entry point for the application.
        public static void Main(string[] args) => WebApplication.Run<Startup>(args);
    }
}
