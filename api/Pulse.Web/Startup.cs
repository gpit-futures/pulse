using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Autofac;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Pulse.Infrastructure;
using Pulse.Infrastructure.MessageQueue;
using Pulse.Infrastructure.MessageQueue.Handlers;
using Pulse.Infrastructure.MessageQueue.Messages;
using Pulse.Web.Messages;
using RawRabbit;
using RawRabbit.Attributes;
using RawRabbit.Common;
using RawRabbit.Serialization;
using RawRabbit.vNext;

namespace Pulse.Web
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            this.Configuration = configuration;
        }

        private IContainer _applicationContainer;

        private IBusClient _bus;

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();

            StartupTask.Register(services);

            services.AddRawRabbit(config => config.AddJsonFile("rabbitmq.json"),
                ioc =>
                {
                    ioc.AddSingleton<IConfigurationEvaluator, AttributeConfigEvaluator>();
                });

            this._applicationContainer = Bootstrapper.SetupContainer(services);
            this._bus = Bootstrapper.SetupMessageSubscriptions(services, this._applicationContainer);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, IApplicationLifetime lifetime)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseStaticFiles();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}");
            });

            lifetime.ApplicationStopped.Register(() =>
            {
                this._bus.ShutdownAsync();
                this._applicationContainer.Dispose();
            });
        }
    }
}
