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
using System.Linq;
using System.Runtime.Loader;
using System.Reflection;
using Investments.Application.helpers;
using AutoMapper;
using Investments.Domain.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Investments.API.Controllers;
using System.Collections.Generic;
using System.Net.WebSockets;
using System.Threading.Tasks;
using System.Threading;
using Microsoft.AspNetCore.Http;

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
        .AddNewtonsoftJson(options => 
            options.SerializerSettings.ReferenceLoopHandling =
            Newtonsoft.Json.ReferenceLoopHandling.Ignore
        );

            
            services.AddControllers()
                    .AddJsonOptions(options => 
                        options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter())
                    )
                    .AddJsonOptions(options => 
                        options.JsonSerializerOptions.WriteIndented = true
                    )
                    .AddNewtonsoftJson(options =>
                    {
                        options.SerializerSettings.ReferenceLoopHandling =
                            Newtonsoft.Json.ReferenceLoopHandling.Ignore;
                        options.SerializerSettings.ContractResolver = 
                            new Newtonsoft.Json.Serialization.DefaultContractResolver();
                    });

            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            services.AddScoped<IDetailedFundService, DetailedFundService>();
            services.AddScoped<IFundsService, FundsService>();
            services.AddScoped<IFundsYieldService, FundsYieldService>();
            services.AddScoped<IRankOfTheBestFundsService, RankOfTheBestFundsService>();
            services.AddScoped<IStocksService, StocksService>();
            services.AddScoped<IWebScrapingFundsAndYeldsService, WebScrapingFundsAndYeldsService>();
            // services.AddScoped<IAccountService, AccountService>();

            services.AddScoped<IDetailedFundPersist, DetailedFundPersist>();
            services.AddScoped<IFundsPersist, FundsPersist>();
            services.AddScoped<IGeneralPersist, GeneralPersist>();
            services.AddScoped<IFundsYeldPersist, FundYeldsPersist>();
            services.AddScoped<IRankOfTheBestFundsPersist, RankOfTheBestFundsPersist>();
            services.AddScoped<IWebScrapingFundsAndYeldsPersist, WebScrapingFundsAndYeldsPersist>();
            services.AddScoped<IUserPersist, UserPersist>();
            
            services.AddSingleton<WebScrapingSocketManager>();
            
            services.AddCors();
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo { Title = "ProEventos.API", Version = "v1" });
                // options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                // {
                //     Description = @"JWT Authorization header usando Bearer.
                //                     Entre com o 'Bearer ' [espaço] então coloque seu token.
                //                     Exemplo: 'Bearer 123456abcd'",
                //     Name = "Authorization",
                //     In = ParameterLocation.Header,
                //     Type = SecuritySchemeType.ApiKey,
                //     Scheme = "Bearer"
                // });

                // options.AddSecurityRequirement(new OpenApiSecurityRequirement()
                // {
                //     {
                //         new OpenApiSecurityScheme
                //         {
                //                     Reference = new OpenApiReference
                //                     {
                //                         Type = ReferenceType.SecurityScheme,
                //                         Id = "Bearer"
                //                     },
                //                     Scheme = "oauth2",
                //                     Name = "Bearer",
                //                     In = ParameterLocation.Header
                //                 },
                //                 new List<string>()
                //             }
                //         });
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

            app.UseCors(options => options.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin());

            // Middleware para WebSockets
            app.UseWebSockets();

            // app.UseEndpoints(endpoints =>
            // {
            //     endpoints.Map("/ws", async context =>
            //     {
            //         if (context.WebSockets.IsWebSocketRequest)
            //         {
            //             var webSocket = await context.WebSockets.AcceptWebSocketAsync();
            //             await WebSocketHandler.HandleWebSocketAsync(webSocket);
            //         }
            //         else
            //         {
            //             context.Response.StatusCode = 400;
            //         }
            //     });
            // });

            app.UseMiddleware<VariablesManager.WebScrapingSocketMiddleware>();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

    }
}
