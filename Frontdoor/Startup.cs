using System.Collections.Generic;
using System.Linq;
using System.Security.Authentication;
using Frontdoor.Configuration;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.ReverseProxy.Abstractions;

namespace Frontdoor
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            // Reverse Proxy
            AddReverseProxy(services);
        }


        private static void AddReverseProxy(IServiceCollection services)
        {
            var routes = new Routes();
            var webApplications = routes.GetWebApplications();

            var proxyRoutes = webApplications.Select(app => new ProxyRoute
            {
                RouteId = $"{app.Name}Route",
                ClusterId = app.Name,
                Match = { Path = $"{app.Prefix.TrimEnd('/')}/{{**catch-all}}" },
            }).ToList();

            // Only in development
            if (true)
            {
                var uiLiveReloadRoutes = webApplications.Select(app => new ProxyRoute
                {
                    RouteId = $"{app.Name}LiveReloadRoute",
                    ClusterId = app.Name,
                    Match = { Path = $"{app.Prefix.TrimEnd('/')}/sockjs-node/{{*remainder}}" },
                    Transforms = new List<IDictionary<string, string>> {
                    new Dictionary<string, string> {{"PathPattern", "/sockjs-node/{remainder}"}}
                }
                }).ToList();
                proxyRoutes.AddRange(uiLiveReloadRoutes);
            }

            var clusters = webApplications.Select(app => new Cluster
            {
                Id = app.Name,
                Destinations = {
                    {"destination1", new Destination {Address = app.Destination}}
                }
            });

            services.AddReverseProxy().LoadFromMemory(proxyRoutes, clusters.ToList());
        }


        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseWebSockets();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapReverseProxy();
            });
        }
    }
}
