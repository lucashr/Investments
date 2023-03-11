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

            services.AddIdentityCore<User>(options =>
            {   
                options.Password.RequireDigit = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireUppercase = false;
                options.Password.RequiredLength = 4;
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

            
            services.AddControllers()
                    .AddJsonOptions(options => 
                        options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter())
                    )
                    .AddJsonOptions(options => 
                        options.JsonSerializerOptions.WriteIndented = true
                    )
                    .AddNewtonsoftJson(options => 
                        options.SerializerSettings.ReferenceLoopHandling =
                        Newtonsoft.Json.ReferenceLoopHandling.Ignore
                    );

            if(AppDomain.CurrentDomain.BaseDirectory.ToLower().Contains(".tests"))
            {
                // var assembly = typeof(Program).GetTypeInfo().Assembly;
                // services.AddAutoMapper(assembly);

                var mappingConfig = new MapperConfiguration(mc =>
                {
                    mc.AddProfile(new InvestmentsProfile());
                });

                IMapper mapper = mappingConfig.CreateMapper();

                services.AddSingleton(mapper);
            }
            else
            {
                services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            }

            services.AddScoped<IDetailedFundService, DetailedFundService>();
            services.AddScoped<IFundsService, FundsService>();
            services.AddScoped<IFundsYieldService, FundsYieldService>();
            services.AddScoped<IRankOfTheBestFundsService, RankOfTheBestFundsService>();
            services.AddScoped<IStocksService, StocksService>();
            services.AddScoped<IWebScrapingFundsAndYeldsService, WebScrapingFundsAndYeldsService>();
            services.AddScoped<IAccountService, AccountService>();
            services.AddScoped<ITokenService, TokenService>();

            services.AddScoped<IDetailedFundPersist, DetailedFundPersist>();
            services.AddScoped<IFundsPersist, FundsPersist>();
            services.AddScoped<IGeneralPersist, GeneralPersist>();
            services.AddScoped<IFundsYeldPersist, FundYeldsPersist>();
            services.AddScoped<IRankOfTheBestFundsPersist, RankOfTheBestFundsPersist>();
            services.AddScoped<IWebScrapingFundsAndYeldsPersist, WebScrapingFundsAndYeldsPersist>();
            services.AddScoped<IUserPersist, UserPersist>();
            
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
