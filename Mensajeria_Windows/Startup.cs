using System.Reflection;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.AspNetCore.Mvc.Versioning.Conventions;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json.Converters;
using Mensajeria_Windows.Business;
using Mensajeria_Windows.Business.Interfaces;
using Mensajeria_Windows.EntityFramework.Data;
using Mensajeria_Windows.ExecutionContext.Infrastructure.ExecutionContext;
using Mensajeria_Windows.Infrastructure.ExecutionContext;
using Mensajeria_Windows.Infrastructure.Filters.AppInsights;
using Mensajeria_Windows.Infrastructure.Handlers;
using Mensajeria_Windows.Infrastructure.Routing;
using Mensajeria_Windows.Services;
using Mensajeria_Windows.Services.Interfaces;
using Microsoft.EntityFrameworkCore.InMemory;
using Mensajeria_Windows.Infrastructure.MiddleWares;

namespace Mensajeria_Windows
{
    /// <summary>
    /// Startup
    /// </summary>
    internal class Startup
    {
        /// <summary>
        /// Startup
        /// </summary>
        /// <param name="configuration">Configuration</param>
        /// <param name="environment">Web host environment</param>
        public Startup (IConfiguration configuration, IWebHostEnvironment environment)
        {
            Configuration = configuration;
            Environment = environment;
        }

        /// <summary>
        /// Configuration
        /// </summary>
        private IConfiguration Configuration { get; }

        /// <summary>
        /// Environment
        /// </summary>
        private IWebHostEnvironment Environment { get; }

        /// <summary>
        /// This method gets called by the runtime. Use this method to add services to the container.
        /// </summary>
        /// <param name="services">Service collection</param>
        public void ConfigureServices (IServiceCollection services)
        {
            services.AddApplicationInsightsTelemetry()
                    .AddHttpContextAccessor()
                    .AddHttpClient(Options.DefaultName)
                    .AddHttpMessageHandler<DependencyHandler>()
                    ;

            services.AddTransient<DependencyHandler>();
            
            services.AddApiVersioning(options =>
            {
                options.AssumeDefaultVersionWhenUnspecified = false;
                options.Conventions.Add(new VersionByNamespaceConvention());
                options.ReportApiVersions = true;
                options.ApiVersionReader = new UrlSegmentApiVersionReader();
                options.ApiVersionSelector = new CurrentImplementationApiVersionSelector(options);
            }).AddVersionedApiExplorer(options =>
            {
                options.GroupNameFormat = @"'v'V";
                options.AssumeDefaultVersionWhenUnspecified = false;
                options.SubstituteApiVersionInUrl = true;
            });

            services.AddControllers(options =>
            {
                options.Conventions.Add(new RouteTokenTransformerConvention(new SlugifyParameterTransformer()));
                options.SuppressAsyncSuffixInActionNames = true;
                options.RequireHttpsPermanent = true;
                options.Filters.Add(typeof(RequestLoggerFilter));
            })
            .AddNewtonsoftJson(options =>
            {
                options.SerializerSettings.Converters.Add(new StringEnumConverter());
            });

            services.AddSwaggerGen(config =>
            {
                var titleBase = "API Platform";
                var description = "This is the simple API architecture.";
                var license = new OpenApiLicense()
                {
                    Name = "Apache 2.0",
                    Url = new Uri(@"https://www.apache.org/licenses/LICENSE-2.0")
                };
                var contact = new OpenApiContact()
                {
                    Email = "you@your-company.com"
                };

                config.CustomOperationIds(e => $"{e.ActionDescriptor.RouteValues["controller"]}_{e.GroupName}_{e.ActionDescriptor.RouteValues["action"]}_{e.HttpMethod}");
                config.CustomSchemaIds(type => type.ToString());
                config.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = titleBase + " v1",
                    Description = description,
                    License = license,
                    Contact = contact
                });
                config.SwaggerDoc("v2", new OpenApiInfo
                {
                    Version = "v2",
                    Title = titleBase + " v2",
                    Description = description,
                    License = license,
                    Contact = contact
                });
                config.EnableAnnotations();
                config.DescribeAllParametersInCamelCase();

                var xmlFileApi = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPathApi = Path.Combine(AppContext.BaseDirectory, xmlFileApi);
                config.IncludeXmlComments(xmlPathApi);

                var xmlFileModels = $"{Assembly.GetAssembly(typeof(Startup)).GetName().Name}.xml";
                var xmlPathModels = Path.Combine(AppContext.BaseDirectory, xmlFileModels);
                config.IncludeXmlComments(xmlPathModels);
                
            });
            services.AddDbContext<NotificationContext>();
            AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
            services.AddCors();
            services.AddScoped<IExecutionContext, ApiExecutionContext>()
                    .AddScoped(typeof(IExecutionContext<>), typeof(ApiExecutionContext<>));


            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());


            services.AddScoped<IAgenciaBusiness, AgenciaBusiness>();
            services.AddScoped<IAgenciaService, AgenciaService>();

            services.AddScoped<IInfoTeamsProvider, InfoTeamsBusiness>();
            services.AddScoped<IInfoTeamService, InfoTeamsService>();

            services.AddScoped<IEmailBusiness, EmailBusiness>();
            services.AddScoped<IInfoEmailService, InfoEmailService>();

            services.AddScoped<IInfoWhatsAppBusiness, InfoWhatsAppBusiness>();
            services.AddScoped<IInfoWhatsAppService, InfoWhatsAppService>();

            services.AddScoped<IEmailBusiness, EmailBusiness>();

            services.AddScoped<ITeamsBusiness, TeamsBusiness>();
            services.AddScoped<ITeamsService, TeamsService>();

            services.AddScoped<IPlantillaBusiness, PlantillaBusiness>();
            services.AddScoped<IPlantillaService, PlantillaService>();
            services.AddScoped<IAdministradoresService, AdministradoresService>();
        }

        /// <summary>
        /// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        /// </summary>
        /// <param name="applicationBuilder">A builder of the application's request pipeline.</param>
        /// <param name="apiVersionDescriptionProvider">The current API version description provider.</param>
        /// <param name="hostEnvironment">The current host environment of this application.</param>
        /// <param name="logger">A logger to trace events during this configuration.</param>
        public void Configure (IApplicationBuilder applicationBuilder, IApiVersionDescriptionProvider apiVersionDescriptionProvider, IWebHostEnvironment hostEnvironment, ILogger<Startup> logger)
        {
            if (hostEnvironment.IsDevelopment())
            {
                applicationBuilder.UseDeveloperExceptionPage();
                applicationBuilder.UseSwagger()
                              .UseSwaggerUI(options =>
                              {
                                  foreach (var description in apiVersionDescriptionProvider.ApiVersionDescriptions.Select(desc => desc.GroupName))
                                  {
                                      options.SwaggerEndpoint($@"/swagger/{description}/swagger.json", description.ToUpperInvariant());
                                  }

                                  options.RoutePrefix = @"swagger";
                              });
            }


            const string defaultCulture = @"es";
            applicationBuilder.UseHttpsRedirection()
                              .UseDefaultFiles()
                              .UseStaticFiles()
                              .UseRouting()
                              .UseAuthentication()
                              .UseAuthorization()
                              .UseEndpoints(endpoints =>
                              {
                                  endpoints.MapControllers();
                              });



            applicationBuilder.UseCors(x => x
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader());
            applicationBuilder.UseMiddleware<GlobalErrorHandlingMiddleware>();
            logger.LogInformation(@"Request pipeline successfully configured for '{Environment}' environment...", hostEnvironment.EnvironmentName);
        }
    }
}
