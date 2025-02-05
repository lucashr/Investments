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
using Investments.Domain.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authentication;
using Investments.VariablesManager;
using Serilog;
using Investments.API.Middlewares;
using Investments.Domain;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Bson.Serialization.Serializers;
using MongoDB.Bson.Serialization;
using System.Threading.Tasks;
using AspNetCore.Identity.MongoDbCore.Extensions;
using AspNetCore.Identity.MongoDbCore.Infrastructure;
using Investments.Application.Factory;
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

            BsonSerializer.RegisterSerializer(new GuidSerializer(GuidRepresentation.Standard));

            var config = Configuration.GetSection("DatabaseSettings");
            var useMongoDb = config.GetValue<bool>("UseMongoDb");

            if (useMongoDb)
            {

                var mongoConnectionString = config.GetSection("MongoDB:ConnectionString").Value;
                var mongoDatabaseName = config.GetSection("MongoDB:DatabaseName").Value;

                services.AddSingleton<IMongoClient>(sp =>
                {
                    return new MongoClient(mongoConnectionString);
                });

                services.AddSingleton<IMongoDatabase>(sp =>
                {
                    var mongoClient = sp.GetRequiredService<IMongoClient>();
                    return mongoClient.GetDatabase(mongoDatabaseName);
                });

                var mongodbIdentityConfig = new MongoDbIdentityConfiguration
                {
                    MongoDbSettings = new MongoDbSettings
                    {
                        ConnectionString = mongoConnectionString,
                        DatabaseName = mongoDatabaseName
                    },
                    IdentityOptionsAction = options =>
                    {
                        options.Password.RequireDigit = false;
                        options.Password.RequireNonAlphanumeric = false;
                        options.Password.RequireLowercase = false;
                        options.Password.RequireUppercase = false;
                        options.Password.RequiredLength = 0;
                    }
                };

                services.ConfigureMongoDbIdentity<ApplicationUser, ApplicationRole, string>(mongodbIdentityConfig)
                        .AddUserManager<UserManager<ApplicationUser>>()
                        .AddSignInManager<SignInManager<ApplicationUser>>()
                        .AddRoleManager<RoleManager<ApplicationRole>>()
                        .AddDefaultTokenProviders();
                
                services.AddScoped<IAccountServiceFactory, AccountServiceMongoDbFactory>();

            }
            else
            {
                // Configuração do SQLite

                var sqliteConnectionString = config.GetValue<string>("SQLiteConnectionString");

                services.AddDbContext<InvestmentsContext>(options =>
                {
                    options.EnableSensitiveDataLogging();
                    options.UseSqlite(sqliteConnectionString);
                });

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

                services.AddScoped<IAccountServiceFactory, AccountServiceEntityFactory>();

            }

            services.AddControllers()
                    .AddJsonOptions(options =>
                        options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter())
                        )
                    .AddNewtonsoftJson(options =>
                        options.SerializerSettings.ReferenceLoopHandling =
                        Newtonsoft.Json.ReferenceLoopHandling.Ignore
                    );


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
                // Configuração de eventos para rastreamento detalhado
                options.Events = new JwtBearerEvents
                {
                    OnAuthenticationFailed = context =>
                    {
                        Log.Error($"Token validation failed: {context.Exception.Message}");
                        return Task.CompletedTask;
                    },
                    OnTokenValidated = context =>
                    {
                        Log.Information($"Token validated successfully for user: {context.Principal.Identity.Name}");
                        return Task.CompletedTask;
                    },
                    OnChallenge = context =>
                    {
                        Log.Warning("Authentication challenge triggered.");
                        return Task.CompletedTask;
                    }
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
            services.AddScoped<IRankOfTheBestStocksService, BestStockRankService>();
            services.AddScoped<IStocksDividendService, StocksDividendService>();
            services.AddScoped<IFundDividendsService, FundsDividendsService>();
            services.AddScoped<IRankOfTheBestFundsService, BestFundRankService>();
            services.AddScoped<IWebScrapingFundsAndDividendsService, FundsAndDividendsWebScrapingService>();
            services.AddScoped<IWebScrapingStocksAndDividendsService, StocksAndDividendsWebScrapingService>();
            services.AddScoped<IEnderecoUsuarioService, EnderecoUsuarioService>();
            services.AddScoped<ITokenService, TokenService>();

            services.AddScoped<IRepositoryPersist, RepositoryPersist>();
            services.AddScoped<IDetailedFundPersist, DetailedFundPersist>();
            services.AddScoped<IDetailedStocksPersist, DetailedStocksPersist>();
            services.AddScoped<IRepositoryPersist, RepositoryPersist>();
            services.AddScoped<IFundDividendPersist, FundDividendPersist>();
            services.AddScoped<IRankOfTheBestFundsPersist, RankOfTheBestFundsPersist>();
            services.AddScoped<IRankOfTheBestStocksPersist, RankOfTheBestStocksPersist>();
            services.AddScoped<IUserPersist, UserPersist>();
            services.AddScoped<IUserPersistMongoDb, UserPersistMongoDb>();
            services.AddScoped<IUserAddressPersist, UserAddressPersist>();
            services.AddScoped<IStockDividendPersist, StocksDividendsPersist>();

            services.AddSingleton<WebScrapingSocketManager>();

            services.AddDataProtection();
            services.AddSingleton<ISystemClock, SystemClock>();

            services.AddSingleton<SessionContext>();
            // services.AddScoped<SessionService>();

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

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

            using (var scope = app.ApplicationServices.CreateScope())
            {
                var database = scope.ServiceProvider.GetService<IMongoDatabase>();
                var mongoClient = scope.ServiceProvider.GetService<IMongoClient>();
                if (database != null)
                    DatabaseSeeder.SeedAsync(database, mongoClient).GetAwaiter().GetResult();
            }

        }

    }
}
