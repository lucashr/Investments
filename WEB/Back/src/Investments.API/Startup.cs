using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Investments.Persistence.Contexts;
using Serilog;
using Investments.API.Middlewares;
using MongoDB.Driver;

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
            ServicesOfApplication.AddServices(services, Configuration);
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

            // Middleware de WebSocket deve ser chamado antes do roteamento
            app.UseWebSockets();

            // Middleware de Log
            app.UseSerilogRequestLogging();

            // Middleware de autenticação deve vir antes do roteamento
            app.UseAuthentication();

            // Configuração de roteamento
            app.UseRouting();

            // Configurar CORS após o roteamento
            app.UseCors(options =>
            {
                if (env.IsDevelopment())
                {
                    options.AllowAnyHeader()
                        .AllowAnyMethod()
                        .AllowAnyOrigin();
                }
                else
                {
                    options.WithOrigins("http://localhost:4200") // Substitua pelas URLs confiáveis
                        .AllowAnyHeader()
                        .AllowAnyMethod();
                }
            });

            // Middleware de autorização (deve vir depois do CORS e autenticação)
            app.UseAuthorization();

            // Middleware do WebSocket (Customizado)
            app.UseMiddleware<WebScrapingSocketMiddleware>();

            // Middleware de tratamento de erros
            app.UseMiddleware<ErrorHandlerMiddleware>();

            // Mapear endpoints
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            // Seed do banco de dados
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
