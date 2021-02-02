using WorldDoomLeague.Application;
using WorldDoomLeague.Application.Common.Interfaces;
using WorldDoomLeague.Infrastructure;
using WorldDoomLeague.Infrastructure.Persistence;
using WorldDoomLeague.Infrastructure.Files;
using WorldDoomLeague.Application.ConfigModels;
using WorldDoomLeague.WebUI.Filters;
using WorldDoomLeague.WebUI.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NSwag;
using NSwag.Generation.Processors.Security;
using System.Linq;
using Microsoft.AspNetCore.SpaServices.ReactDevelopmentServer;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.FileProviders;

namespace WorldDoomLeague.WebUI
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
            services.AddApplication();
            services.AddInfrastructure(Configuration);

            services.AddDatabaseDeveloperPageExceptionFilter();

            services.AddSingleton<ICurrentUserService, CurrentUserService>();

            services.AddHttpContextAccessor();

            services.AddHealthChecks()
                .AddDbContextCheck<ApplicationDbContext>();

            services.Configure<DataDirectories>(Configuration.GetSection("DataDirectories"));

            services.AddControllersWithViews(options => options.Filters.Add(new ApiExceptionFilter()))
                .AddJsonOptions(options => options.JsonSerializerOptions.Converters.Add(new TimeSpanToStringConverter()));

            services.AddRazorPages();

            // Customise default API behaviour
            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.SuppressModelStateInvalidFilter = true;
            });

            services.AddSpaStaticFiles(configuration =>
            {
                configuration.RootPath = "ClientApp/build";
            });

            services.AddOpenApiDocument(configure =>
            {
                configure.Title = "WorldDoomLeague";
                configure.AddSecurity("JWT", Enumerable.Empty<string>(), new OpenApiSecurityScheme
                {
                    Type = OpenApiSecuritySchemeType.ApiKey,
                    Name = "Authorization",
                    In = OpenApiSecurityApiKeyLocation.Header,
                    Description = "Type into the textbox: Bearer {your JWT token}."
                });

                configure.OperationProcessors
                .Add(new AspNetCoreOperationSecurityScopeProcessor("JWT"));
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseMigrationsEndPoint();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHealthChecks("/health");
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseStaticFiles(new StaticFileOptions
            {
                FileProvider = new PhysicalFileProvider(
                Configuration.GetSection("DataDirectories").GetValue<string>("WadRepository")),
                RequestPath = "/WadFiles"
            });
            app.UseStaticFiles(new StaticFileOptions
            {
                FileProvider = new PhysicalFileProvider(
                Configuration.GetSection("DataDirectories").GetValue<string>("DemoRepository")),
                RequestPath = "/DemoFiles"
            });
            if (!env.IsDevelopment())
            {
                app.UseSpaStaticFiles();
            }

            app.UseSwaggerUi3(settings =>
            {
                settings.Path = "/api";
                settings.DocumentPath = "/api/specification.json";
            });

            app.UseRouting();

            app.UseAuthentication();
            app.UseIdentityServer();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                // Mapping of endpoints goes here:
                endpoints.MapControllers();
                endpoints.MapRazorPages();
            });

            app.UseSpa(spa =>
            {
                // To learn more about options for serving an React SPA from ASP.NET Core,
                // see https://go.microsoft.com/fwlink/?linkid=864501

                spa.Options.SourcePath = "ClientApp";

                if (env.IsDevelopment())
                {
                    spa.UseReactDevelopmentServer(npmScript: "start");
                }
            });
        }
    }
}
