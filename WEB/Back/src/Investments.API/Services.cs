using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using AspNetCore.Identity.MongoDbCore.Extensions;
using AspNetCore.Identity.MongoDbCore.Infrastructure;
using AutoMapper;
using Investments.Application;
using Investments.Application.Contracts;
using Investments.Application.Factory;
using Investments.Application.helpers;
using Investments.Domain;
using Investments.Domain.Identity;
using Investments.Persistence;
using Investments.Persistence.Contexts;
using Investments.Persistence.Contracts;
using Investments.VariablesManager;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;
using MongoDB.Driver;
using Serilog;
using StackExchange.Redis;
using Investments.API.Services.Cache;


namespace Investments.API
{
    public static class ServicesOfApplication
    {
        public static void AddServices(IServiceCollection services, IConfiguration Configuration)
        {

            AddCacheRedis(services, Configuration);
            AddDatabaseServices(services, Configuration);

            services.AddControllers()
                    .AddJsonOptions(options =>
                        options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter())
                        )
                    .AddNewtonsoftJson(options =>
                        options.SerializerSettings.ReferenceLoopHandling =
                        Newtonsoft.Json.ReferenceLoopHandling.Ignore
                    );

            AddAuthenticationAndAutorizationServices(services, Configuration);

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

            services.AddAutoMapper(typeof(InvestmentsProfile).Assembly);
            
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

            AddSwaggerServices(services, Configuration);

        }

        private static void AddCacheRedis(IServiceCollection services, IConfiguration Configuration)
        { 
            // Register Redis connection multiplexer (if needed elsewhere)
            services.AddSingleton<IConnectionMultiplexer>(sp =>
                ConnectionMultiplexer.Connect(Configuration.GetValue<string>("Redis:Configuration") ?? "localhost:6379"));

            // Register distributed Redis cache
            services.AddStackExchangeRedisCache(options =>
            {
                options.Configuration = Configuration.GetValue<string>("Redis:Configuration") ?? "localhost:6379";
                options.InstanceName = Configuration.GetValue<string>("Redis:InstanceName") ?? "MyApp_";
            });

            // Register in-memory cache
            services.AddMemoryCache();

            // Register concrete cache services
            services.AddSingleton<DistributedCacheService>();
            services.AddSingleton<MemoryCacheService>();

            // Register ICacheService selecting implementation from configuration (Cache:Provider = "Memory"|"Redis")
            services.AddSingleton<Investments.Application.Contracts.ICacheService>(sp =>
            {
                var provider = Configuration.GetValue<string>("Cache:Provider");
                if (!string.IsNullOrEmpty(provider) && provider.Equals("Memory", StringComparison.OrdinalIgnoreCase))
                    return sp.GetRequiredService<MemoryCacheService>();
                return sp.GetRequiredService<DistributedCacheService>();
            });

        }
        private static void AddSwaggerServices(IServiceCollection services, IConfiguration Configuration)
        {
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

        private static void AddAuthenticationAndAutorizationServices(IServiceCollection services, IConfiguration Configuration)
        {
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
        }

        private static void AddDatabaseServices(IServiceCollection services, IConfiguration Configuration)
        {

            // BsonSerializer.RegisterSerializer(new GuidSerializer(GuidRepresentation.Standard));

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
                .AddRoles<Domain.Identity.Role>()
                .AddRoleManager<RoleManager<Domain.Identity.Role>>()
                .AddSignInManager<SignInManager<Domain.Identity.User>>()
                .AddRoleValidator<RoleValidator<Domain.Identity.Role>>()
                .AddEntityFrameworkStores<InvestmentsContext>()
                .AddDefaultTokenProviders();

                services.AddScoped<IAccountServiceFactory, AccountServiceEntityFactory>();

            }

        }






    }
}