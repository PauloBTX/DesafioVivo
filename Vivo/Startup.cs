using Infra.Data.Context;
using Infra.IoC;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Vivo
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services) {
            services.Configure<RequestLocalizationOptions>(options =>
            {
                options.DefaultRequestCulture = new Microsoft.AspNetCore.Localization.RequestCulture("pt-BR");
            });

            services.Configure<RequestLocalizationOptions>(options =>
            {
                options.DefaultRequestCulture = new RequestCulture("pt-BR");
            });

            services.AddControllers().AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
                options.JsonSerializerOptions.WriteIndented = false;
                options.JsonSerializerOptions.IncludeFields = true;
                options.JsonSerializerOptions.AllowTrailingCommas = false;
                options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
                options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;

            });

            services.AddApiVersioning(options =>
            {
                options.ReportApiVersions = true;

                options.AssumeDefaultVersionWhenUnspecified = true;
                options.DefaultApiVersion = new ApiVersion(1, 0);
            });

            services.AddVersionedApiExplorer(options =>
            {
                // Agrupar por número de versão
                // add the versioned api explorer, which also adds IApiVersionDescriptionProvider service
                // note: the specified format code will format the version as "'v'major[.minor][-status]"
                options.GroupNameFormat = "'v'VVV";

                // Necessário para o correto funcionamento das rotas
                // note: this option is only necessary when versioning by url segment. the SubstitutionFormat
                // can also be used to control the format of the API version in route templates
                options.SubstituteApiVersionInUrl = true;
            });

            services.AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigureSwaggerOptions>();

            services.AddSwaggerGen(options =>
            {
                // add a custom operation filter which sets default values
                options.OperationFilter<SwaggerDefaultValues>();
            });

            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy",
                    builder => builder
                    .SetIsOriginAllowed((host) => true)
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .AllowCredentials());
            });


            // Docker Sql Config
            /*
            var server = Configuration["DbServer"] ?? "localhost";
            var port = Configuration["DbPort"] ?? "1433"; 
            var user = Configuration["DbUser"] ?? "SA"; 
            var password = Configuration["Password"] ?? "desafioVivo123";
            var database = Configuration["Database"] ?? "DesafioVivo";
            var connectionString = $"Server={server}, {port};Initial Catalog={database};User ID={user};Password={password}";

            services.AddDbContext<VivoContext>(options => options.UseSqlServer(connectionString));
            services.AddTransient<VivoContext>();*/
            

            services.AddControllers();
            services.AddEndpointsApiExplorer();
            RegisterServices(services);
        }

        private static void RegisterServices(IServiceCollection services)
        {
            NativeInjectorBootStrapper.RegisterServices(services);
        }

        public void Configure(WebApplication app, IWebHostEnvironment env, IApiVersionDescriptionProvider provider, VivoContext context)
        {
            if (app.Environment.IsDevelopment())
            {
                
                app.UseSwagger();
                //app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "APIVivo v1"));
                app.UseSwaggerUI(options =>
                {
                    // Geração de um endpoint do Swagger para cada versão descoberta
                    foreach (var description in provider.ApiVersionDescriptions)
                    {
                        options.SwaggerEndpoint($"/swagger/{description.GroupName}/swagger.json",
                            description.GroupName.ToUpperInvariant());
                    }
                });
                context.Database.EnsureCreated();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapControllers();

        }

        private void InitializeDatabase(IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope())
            {
                serviceScope.ServiceProvider.GetRequiredService<VivoContext>().Database.Migrate();

                var context = serviceScope.ServiceProvider.GetRequiredService<VivoContext>();
                context.Database.Migrate();
            }
        }
    }
}
