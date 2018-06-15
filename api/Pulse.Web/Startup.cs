using Autofac;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Pulse.Infrastructure;
using RawRabbit;
using RawRabbit.Attributes;
using RawRabbit.Common;
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

            var token = this.Configuration.GetSection("Jwk").Get<JsonWebKey>();

            services.AddAuthorization(conf =>
            {
                conf.AddPolicy("Read", pol => pol.RequireClaim("FOO_READ"));
                conf.AddPolicy("Write", pol => pol.RequireClaim("FOO_WRITE"));
            })
            .AddAuthentication(conf =>
            {
                conf.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                conf.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(conf =>
            {
                conf.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateAudience = false,
                    ValidateIssuer = false,
                    IssuerSigningKey = token
                };
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

            app.UseAuthentication();

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
