using AutoMapper;
using CleanApp.Core.Interfaces;
using CleanApp.Core.Services;
using CleanApp.Infrastructure.Data;
using CleanApp.Infrastructure.Filters;
using CleanApp.Infrastructure.Repositories;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;

namespace CleanApp.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            services.AddControllers(options =>
            {
                options.Filters.Add<GlobalExceptionFilter>();
            })
            .AddNewtonsoftJson(options =>
            {
                options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
            })
            .ConfigureApiBehaviorOptions(options =>
            {
                options.SuppressModelStateInvalidFilter = true;
            });

            services.AddDbContext<CleanAppDDBBContext>(options => options.UseSqlServer(Configuration.GetConnectionString("CleanAppDDBB")));

            //Dependencias

            //Repositorio base
            services.AddScoped(typeof(IRepository<>), typeof(BaseRepository<>));

            //Servicios
            services.AddTransient<ITenantService, TenantService>();
            services.AddTransient<IRoomService, RoomService>();
            services.AddTransient<IWeekService, WeekService>();
            services.AddTransient<IMonthService, MonthService>();
            services.AddTransient<IYearService, YearService>();
            services.AddTransient<IJobService, JobService>();
            //Repositorios
            services.AddTransient<IUnitOfWork, UnitOfWork>();

            services.AddMvc(setup =>
            {
                setup.Filters.Add<ValidationFilter>();
            }).AddFluentValidation(fv =>
            {
                fv.RegisterValidatorsFromAssemblies(AppDomain.CurrentDomain.GetAssemblies());
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
