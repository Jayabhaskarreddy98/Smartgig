using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DMS.Api.AuthServices;
using DMS.BusinessAccessLayer.Interfaces;
using DMS.BusinessAccessLayer.Services;
using DMS.Components.Entities;
using DMS.DataAccessLayer.Interfaces;
using DMS.DataAccessLayer.Repos;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Hangfire;
using Hangfire.MemoryStorage;
using Hangfire.Common;
using Hangfire.Server;
using DMS.Api.Controllers;

namespace GCDental
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
            services.AddDbContext<MyDbContext>(
             options => options.UseSqlServer(Configuration.GetConnectionString("DBConnectionString")));
            services.AddJWTTokenServices(Configuration);
            services.AddControllers();
            services.AddCors();
            services.AddScoped<ICommonRepo, CommonRepo>();
            services.AddScoped<IUserMgmtRepo, UserMgmtRepo>();
            services.AddScoped<IOtherMasterRepo, OtherMasterRepo>();
            services.AddScoped<IDealerRepo, DealerRepo>();
            services.AddScoped<IMaterialRepo,MaterialRepo>();
            services.AddScoped<IPromotionsRepo, PromotionsRepo>();
            services.AddScoped<IOrdersRepo, OrdersRepo>();
            services.AddScoped<ISaleRepo, SaleRepo>();
            services.AddScoped<IBackgroundJobsRepo, BackgroundJobsRepo>();

            services.AddScoped<ICommonService, CommonService>();
            services.AddScoped<IUserMgmtService, UserMgmtService>();
            services.AddScoped<IOtherMasterService, OtherMasterService>();
            services.AddScoped<IDealerService, DealerService>();
            services.AddScoped<IMaterialService, MaterialService>();
            services.AddScoped<IPromotionsService, PromotionsService>();
            services.AddScoped<IOrdersService, OrdersService>();
            services.AddScoped<ISaleService, SaleService>();
            services.AddScoped<IBackgroundJobsService, BackgroundJobsService>();
            services.AddMvc().AddNewtonsoftJson(o =>
            {
                o.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
            });


            //services.AddSingleton<QuartzConfig>(); // Add QuartzConfig as a singleton
            //services.AddHostedService(provider => provider.GetRequiredService<QuartzConfig>());

            //services.AddHangfire(configuration => configuration
            //.SetDataCompatibilityLevel(CompatibilityLevel.Version_170)
            //.UseSimpleAssemblyNameTypeSerializer()
            //.UseRecommendedSerializerSettings()
            //.UseMemoryStorage());


            var jobSchedules = Configuration.GetSection("JobSchedules");

            services.AddHangfire(config => config
                .SetDataCompatibilityLevel(CompatibilityLevel.Version_170)
                .UseSimpleAssemblyNameTypeSerializer()
                .UseRecommendedSerializerSettings()
                .UseMemoryStorage());

            services.AddHangfireServer();

            // Schedule jobs based on appsettings values
            ////////using (var serviceProvider = services.BuildServiceProvider())
            ////////{
            ////////    var recurringJobManager = serviceProvider.GetService<IRecurringJobManager>();

            ////////    RecurringJob.AddOrUpdate("MinutelyJob", () => TriggerJob(), jobSchedules["MinutelyJobInterval"]);
            ////////    // Add other recurring jobs here...
            ////////}


            //services.AddHangfireServer();

            // Schedule jobs based on appsettings values
            //RecurringJob.AddOrUpdate("MinutelyJob", () => RunMinutelyJob(), jobSchedules["MinutelyJobInterval"]);
            //RecurringJob.AddOrUpdate("HourlyJob", () => RunHourlyJob(), jobSchedules["HourlyJobInterval"]);
            //RecurringJob.AddOrUpdate("DailyJob", () => RunDailyJob(), jobSchedules["DailyJobInterval"]);
            //RecurringJob.AddOrUpdate("CustomJob", () => RunCustomJob(), jobSchedules["CustomJobInterval"]);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();
            app.UseCors(x => x
         .AllowAnyOrigin()
         .AllowAnyMethod()
         .AllowAnyHeader());
            app.UseAuthorization();

            // Configure Hangfire
            app.UseHangfireServer();
            app.UseHangfireDashboard();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            var midnightJobSchedule = Configuration.GetSection("JobSchedules")["DailyJobInterval"];

            RecurringJob.AddOrUpdate("MidnightJob", () => EnqueueJob(), midnightJobSchedule);
        }

        // Methods to be executed by Hangfire jobs
        //public void RunMinutelyJob()
        //{
        //    // Logic for hourly job
        //}

        //public void RunHourlyJob()
        //{
        //    // Logic for hourly job
        //}

        //public void RunDailyJob()
        //{
        //    // Logic for daily job
        //}
        
        //public void RunCustomJob()
        //{
        //    // Logic for custom job
        //}
        //// Method to enqueue the JobController's RunMinutelyJob method
        //public void TriggerJob()
        //{
        //    // Use Hangfire to enqueue a job that calls the JobController's RunMinutelyJob endpoint
        //    BackgroundJob.Enqueue<JobController>(x => x.RunMinutelyJob());
        //}
        public void EnqueueJob()
        {
            // Use Hangfire to enqueue a job that calls the JobController's RunMinutelyJob endpoint
            BackgroundJob.Enqueue<JobController>(x => x.RunMinutelyJob());
        }
    }
}
