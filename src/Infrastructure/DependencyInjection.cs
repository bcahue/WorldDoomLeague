using WorldDoomLeague.Application.Common.Interfaces;
using WorldDoomLeague.Infrastructure.Files;
using WorldDoomLeague.Infrastructure.Identity;
using WorldDoomLeague.Infrastructure.Persistence;
using WorldDoomLeague.Infrastructure.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Identity;
using System;
using IdentityServer4.Services;

namespace WorldDoomLeague.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            if (configuration.GetValue<bool>("UseInMemoryDatabase"))
            {
                services.AddDbContext<ApplicationDbContext>(options =>
                    options.UseInMemoryDatabase("WorldDoomLeagueDb"));
            }
            else
            {
                services.AddDbContext<ApplicationDbContext>(options =>
                    options.UseMySql(
                        configuration.GetConnectionString("DefaultConnection"),
                        new MariaDbServerVersion(new Version(10, 4, 13)),
                        b => b.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName)));
            }

            services.AddScoped<IApplicationDbContext>(provider => provider.GetService<ApplicationDbContext>());

            services
                .AddDefaultIdentity<ApplicationUser>()
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>();
            
            services.AddIdentityServer()
                .AddApiAuthorization<ApplicationUser, ApplicationDbContext>();

            services.AddTransient<IDateTime, DateTimeService>();
            services.AddTransient<IDateTimeOffset, DateTimeOffsetService>();
            services.AddTransient<IIdentityService, IdentityService>();
            services.AddTransient<ICsvFileBuilder, CsvFileBuilder>();
            services.AddTransient<IDirectoryBrowser, DirectoryBrowser>();
            services.AddTransient<IGetMatchJson, GetMatchJson>();
            services.AddTransient<IProfileService, ProfileService>();
            services.AddTransient<IWadFileHandler, WadFileHandler>();
            services.AddTransient<IImageFileHandler, ImageFileHandler>();

            services.AddAuthentication()
                .AddIdentityServerJwt()
                .AddSteam();

            services.AddAuthorization();

            return services;
        }
    }
}
