using CleanApp.Core.Interfaces;
using CleanApp.Core.Interfaces.Repositories;
using CleanApp.Core.Interfaces.Services;
using CleanApp.Core.Services;
using CleanApp.Core.Services.Auth;
using CleanApp.Infrastructure.Data;
using CleanApp.Infrastructure.Interfaces;
using CleanApp.Infrastructure.Options;
using CleanApp.Infrastructure.Repositories;
using CleanApp.Infrastructure.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using System;
using System.IO;

namespace CleanApp.Infrastructure.Extensions
{
    public static class ServiceCollectionExtension
    {
        public static IServiceCollection AddDbContexts(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<CleanAppDDBBContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("CleanAppDDBB")));

            return services;
        }

        public static IServiceCollection AddConfigurations(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<PaginationOptions>(options => configuration.GetSection("Pagination").Bind(options));
            services.Configure<PasswordOptions>(options => configuration.GetSection("PasswordOptions").Bind(options));

            return services;
        }

        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            //Dependencias

            //Repositorio base
            services.AddScoped(typeof(IRepository<>), typeof(BaseRepository<>));

            //Servicios

            //Core
            services.AddTransient<ITenantService, TenantService>();
            services.AddTransient<IRoomService, RoomService>();
            services.AddTransient<IWeekService, WeekService>();
            services.AddTransient<IMonthService, MonthService>();
            services.AddTransient<IYearService, YearService>();
            services.AddTransient<IJobService, JobService>();
            services.AddTransient<IAuthenticationService, AuthenticationService>();
            services.AddTransient<IHomeService, HomeService>();
            services.AddTransient<IHomeTenantService, HomeTenantService>();
            services.AddTransient<ICleanlinessService, CleanlinessService>();
            //Infrastructure
            services.AddSingleton<IPassworService, PasswordService>();
            services.AddSingleton<IUriService>(provider =>
            {
                var accesor = provider.GetRequiredService<IHttpContextAccessor>();
                var request = accesor.HttpContext.Request;
                var absoluteUri = string.Concat(request.Scheme, "://", request.Host.ToUriComponent());

                return new UriService(absoluteUri);
            });

            //Repositorios
            services.AddTransient<IUnitOfWork, UnitOfWork>();

            return services;
        }

        public static IServiceCollection AddSwagger(this IServiceCollection services, string xmlFileName)
        {
            services.AddSwaggerGen(doc =>
            {
                doc.SwaggerDoc("v1", new OpenApiInfo { Title = "Cleap App API", Version = "v1" });

                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFileName);

                doc.IncludeXmlComments(xmlPath);
            });

            return services;
        }
    }
}
