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
using Microsoft.AspNetCore.Authentication;
using Investments.VariablesManager;
using Serilog;
using Investments.API.Middlewares;

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

            services.AddDbContext<InvestmentsContext>(options =>{
                options.EnableSensitiveDataLogging();
                options.UseSqlite(Configuration.GetConnectionString("Default"));
            });

            services.AddControllers()
                    .AddJsonOptions(options => 
                        options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter())
                        )
                    .AddNewtonsoftJson(options => 
                        options.SerializerSettings.ReferenceLoopHandling =
                        Newtonsoft.Json.ReferenceLoopHandling.Ignore
                    );

            services.AddIdentityCore<User>(options => 
            {
                options.Password.RequireDigit = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireUppercase = false;
                options.Password.RequiredLength = 0;
            })
            .AddRoles<Role>()
            .AddRoleManager<RoleManager<Role>>()
            .AddSignInManager<SignInManager<User>>()
            .AddRoleValidator<RoleValidator<Role>>()
            .AddEntityFrameworkStores<InvestmentsContext>()
            .AddDefaultTokenProviders();

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options => 
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["TokenKey"])),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });
            
            services.AddAuthorization(options =>
            {
                options.AddPolicy("Admin", policy =>
                {
                    policy.RequireRole("Admin");
                });

                options.AddPolicy("AdminOrUser", policy =>
                policy.RequireAssertion(context =>
                    context.User.IsInRole("Admin") || context.User.IsInRole("User")
                ));

            });
            
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

            services.AddLogging(loggingBuilder => loggingBuilder.AddSerilog());

            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            services.AddScoped<IDetailedFundService, DetailedFundService>();
            services.AddScoped<IDetailedStockService, DetailedStockService>();
            services.AddScoped<IRankOfTheBestStocksService, RankOfTheBestStocksService>();
            services.AddScoped<IStocksDividendService, StocksDividendService>();
            services.AddScoped<IStocksDividendService, StocksDividendService>();
            services.AddScoped<IFundDividendsService, FundsDividendsService>();
            services.AddScoped<IRankOfTheBestFundsService, RankOfTheBestFundsService>();
            services.AddScoped<IWebScrapingFundsAndDividendsService, WebScrapingFundsAndDividendsService>();
            services.AddScoped<IWebScrapingStocksAndDividendsService, WebScrapingStocksAndDividendsService>();
            services.AddScoped<IEnderecoUsuarioService, EnderecoUsuarioService>();
            services.AddScoped<IAccountService, AccountService>();
            services.AddScoped<ITokenService, TokenService>();

            services.AddScoped<IDetailedFundPersist, DetailedFundPersist>();
            services.AddScoped<IDetailedStocksPersist, DetailedStocksPersist>();
            services.AddScoped<IGeneralPersist, GeneralPersist>();
            services.AddScoped<IFundsYeldPersist, FundDividendsPersist>();
            services.AddScoped<IRankOfTheBestFundsPersist, RankOfTheBestFundsPersist>();
            services.AddScoped<IRankOfTheBestStocksPersist, RankOfTheBestStocksPersist>();
            services.AddScoped<IUserPersist, UserPersist>();
            services.AddScoped<IEnderecoUsuarioPersist, EnderecoUsuarioPersist>();
            services.AddScoped<IStocksYeldPersist, StocksDividendsPersist>();
            
            services.AddSingleton<WebScrapingSocketManager>();

            services.AddDataProtection();
            services.AddSingleton<ISystemClock, SystemClock>();
            
            services.AddCors(options =>
            {
                options.AddPolicy("AllowSpecificOrigin",
                    builder =>
                    {
                        builder.WithOrigins("http://localhost:4200") // Substitua pelo endereço do seu frontend
                            .AllowAnyHeader()
                            .AllowAnyMethod();
                    });
            });
            
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo { Title = "Investments.API", Version = "v1" });
                options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = @"JWT Authorization header usando Bearer.
                                    Entre com o 'Bearer ' [espaço] então coloque seu token.
                                    Exemplo: 'Bearer 123456abcd'",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer"
                });

                options.AddSecurityRequirement(new OpenApiSecurityRequirement()
                {
                    {
                        new OpenApiSecurityScheme
                        {
                                    Reference = new OpenApiReference
                                    {
                                        Type = ReferenceType.SecurityScheme,
                                        Id = "Bearer"
                                    },
                                    Scheme = "oauth2",
                                    Name = "Bearer",
                                    In = ParameterLocation.Header
                                },
                                new List<string>()
                            }
                        });
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
            app.UseSerilogRequestLogging();

            // Configurar CORS
            app.UseCors(options =>
            {
                if (env.IsDevelopment())
                {
                    // Permitir tudo em desenvolvimento
                    options.AllowAnyHeader()
                        .AllowAnyMethod()
                        .AllowAnyOrigin();
                }
                else
                {
                    // Restringir origens confiáveis em produção
                    options.WithOrigins("http://localhost:4200") // Substitua pelas URLs confiáveis
                        .AllowAnyHeader()
                        .AllowAnyMethod();
                }
            });

            // Middleware de autenticação e autorização
            app.UseAuthentication();
            app.UseAuthorization();

            // Middleware para WebSockets
            app.UseWebSockets();
            app.UseMiddleware<WebScrapingSocketMiddleware>();

            app.UseMiddleware<ErrorHandlerMiddleware>();

            // Mapear endpoints
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

    }
}
