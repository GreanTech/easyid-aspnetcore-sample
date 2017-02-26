using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;

namespace aspnet_core_oidc
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Add framework services.
            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseBrowserLink();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();

            app.UseCookieAuthentication(new CookieAuthenticationOptions {
                AuthenticationScheme = "Cookies",
                AutomaticAuthenticate = true
            });

            var options = new OpenIdConnectOptions() {
                AuthenticationScheme = "oidc", // callback will be on /signin-oidc
                SignInScheme = "Cookies",
                Authority = "https://acme-corp.grean.id",
                ResponseType = "code",
                ClientId = "urn:easyid:aspnet-core-demo",
                ClientSecret = "0m4bGC+LO7QSBk7zf4d2Uhhlq48IRHbUC/D5yM4EROU=",
                GetClaimsFromUserInfoEndpoint = false
            };

            // This may be modified to get the choice of authentication method from
            // some other source, e.g. a dropdown in the UI
            // Not needed for most OIDC identity proivders, such as Google, etc.
            options.Events = new OpenIdConnectEvents() {
                OnRedirectToIdentityProvider = context => {
                    context.ProtocolMessage.AcrValues = "urn:grn:authn:se:bankid:same-device";
                    return Task.FromResult(0);
                }
            };

            // Wire in OIDC middelware
            app.UseOpenIdConnectAuthentication(options);

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
