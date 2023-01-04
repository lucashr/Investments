using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Investments.Application;
using Investments.Application.Contracts;
using Investments.Persistence.Contexts;
using System.Text.Json.Serialization;
using System;
using Investments.Persistence.Contracts;
using Investments.Persistence;
using Investments.VariablesManager;

namespace Investments.API
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

            services.AddDbContext<InvestmentsContext>(
                context => context.UseSqlite(Configuration.GetConnectionString("Default"))
            );
            
            services.AddControllers()
                    .AddJsonOptions(options => 
                        options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter())
                        )
                    .AddJsonOptions(options => 
                        options.JsonSerializerOptions.WriteIndented = true)
                    .AddNewtonsoftJson(options => 
                        options.SerializerSettings.ReferenceLoopHandling =
                        Newtonsoft.Json.ReferenceLoopHandling.Ignore
                    );

                    
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            
            services.AddScoped<IDetailedFundService, DetailedFundService>();
            services.AddScoped<IFundsService, FundsService>();
            services.AddScoped<IFundsYieldService, FundsYieldService>();
            services.AddScoped<IRankOfTheBestFundsService, RankOfTheBestFundsService>();
            services.AddScoped<IStocksService, StocksService>();
            services.AddScoped<IWebScrapingFundsAndYeldsService, WebScrapingFundsAndYeldsService>();
            
            services.AddScoped<IDetailedFundPersist, DetailedFundPersist>();
            services.AddScoped<IFundsPersist, FundsPersist>();
            services.AddScoped<IGeneralPersist, GeneralPersist>();
            services.AddScoped<IFundsYeldPersist, FundYeldsPersist>();
            services.AddScoped<IRankOfTheBestFundsPersist, RankOfTheBestFundsPersist>();
            services.AddScoped<IWebScrapingFundsAndYeldsPersist, WebScrapingFundsAndYeldsPersist>();
            
            services.AddSingleton<WebScrapingSocketManager>();
            
            services.AddCors();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Investments.API", Version = "v1" });
            });


        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Investments.API v1"));
            }

             
 
            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseCors(x => x.AllowAnyHeader()
                              .AllowAnyMethod()
                              .AllowAnyOrigin());

            // var serviceScopeFactory = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>();
            // var serviceProvider = serviceScopeFactory.CreateScope().ServiceProvider;

            app.UseStaticFiles();
            app.UseWebSockets();
            app.UseMiddleware<VariablesManager.WebScrapingSocketMiddleware>();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

    }
}
