using Clean.Architecture.API.Middleware;
<<<<<<< HEAD
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.AspNetCore.Mvc;
using Clean.Architecture.API.Documentation;
=======
using Microsoft.AspNetCore.HttpLogging;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.ResponseCompression;
using System.Text.Json.Serialization;
using System.Text.Json;
using ZymLabs.NSwag.FluentValidation;
using Asp.Versioning;
using NSwag.Generation.AspNetCore;
using NSwag;
using NSwag.Generation.Processors.Security;
using OpenApiInfo = NSwag.OpenApiInfo;
using OpenApiContact = NSwag.OpenApiContact;
using Microsoft.AspNetCore.Authentication.JwtBearer;
>>>>>>> update template

namespace Clean.Architecture.API
{
    public static class ConfigureServices
    {
        public static IServiceCollection AddApiServices(this IServiceCollection services)
        {
<<<<<<< HEAD
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            services.AddControllers();
            services.AddTransient<ExceptionHandlingMiddleware>();
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();
            services.ConfigureOptions<SwaggerUIOptions>();
=======
            services.AddHttpClient();

            services.AddResponseCompression(options =>
            {
                options.EnableForHttps = true;
                options.Providers.Add<BrotliCompressionProvider>();
                options.Providers.Add<GzipCompressionProvider>();
            });

            services.AddHttpContextAccessor();
            services.AddControllers()
                .AddJsonOptions(options =>
                {
                    options.JsonSerializerOptions.WriteIndented = true;
                    options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
                    options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
                    options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
                });
            services.AddScoped<ExceptionHandlingMiddleware>();

            services.AddScoped(provider =>
            {
                var validationRules = provider.GetService<IEnumerable<FluentValidationRule>>();
                var loggerFactory = provider.GetService<ILoggerFactory>();

                return new FluentValidationSchemaProcessor(provider, validationRules, loggerFactory);
            });

            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.SuppressModelStateInvalidFilter = true;
                options.InvalidModelStateResponseFactory = context =>
                {
                    return new ObjectResult(new { error = "Invalid request media type." })
                    {
                        StatusCode = StatusCodes.Status415UnsupportedMediaType
                    };
                };
            });

            services.AddOpenApiDocument((configure, provider) =>
            {
                configure.SwaggerDocument(provider, "v1");
            });

            services.AddOpenApiDocument((configure, provider) =>
            {
                configure.SwaggerDocument(provider, "v2");
            });
>>>>>>> update template

            services.AddApiVersioning(options =>
            {
                options.DefaultApiVersion = new ApiVersion(1, 0);
<<<<<<< HEAD
                options.AssumeDefaultVersionWhenUnspecified = true;
                options.ReportApiVersions = false;
                options.ApiVersionReader = ApiVersionReader.Combine(new UrlSegmentApiVersionReader());
            });

            services.AddVersionedApiExplorer(setup =>
            {
                setup.GroupNameFormat = "'v'VVV";
                setup.SubstituteApiVersionInUrl = true;
            });

            return services;
        }
=======
                options.ReportApiVersions = true;
                options.AssumeDefaultVersionWhenUnspecified = true;

                options.ApiVersionReader = ApiVersionReader.Combine(new UrlSegmentApiVersionReader(),
                    new HeaderApiVersionReader("x-api-version"),
                    new MediaTypeApiVersionReader("x-api-version"));
            })
            .AddApiExplorer(options =>
            {
                options.GroupNameFormat = "'v'VVV";
                options.SubstituteApiVersionInUrl = true;
            });

            services.AddHttpLogging(logs =>
            {
                logs.LoggingFields = HttpLoggingFields.All;
                logs.RequestBodyLogLimit = 4096;
                logs.ResponseBodyLogLimit = 4096;
                logs.CombineLogs = true;
            });

            services.AddSignalR(options =>
            {
                options.EnableDetailedErrors = true;
            })
            .AddJsonProtocol(options =>
            {
                options.PayloadSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
            });
                        
            return services;
        }

        private static void SwaggerDocument(this AspNetCoreOpenApiDocumentGeneratorSettings options, IServiceProvider provider, string version)
        {
            var fluentValidationSchemaProcessor = provider.CreateScope().ServiceProvider.GetService<FluentValidationSchemaProcessor>();
            options.SchemaSettings.SchemaProcessors.Add(fluentValidationSchemaProcessor!);

            options.DocumentName = version;
            options.ApiGroupNames = new[] { version };

            options.PostProcess = document =>
            {
                document.Info = new OpenApiInfo
                {
                    Title = "Clean.Architecture API",
                    Description = "No description",
                    Version = version,
                    Contact = new OpenApiContact
                    {
                        Name = "Contact",
                        Url = "https://example.com/contact"
                    }
                };
            };

            options.AddSecurity("JWT", Enumerable.Empty<string>(), new NSwag.OpenApiSecurityScheme
            {
                Name = "Authorization",
                Description = "Bearer {token}",
                In = OpenApiSecurityApiKeyLocation.Header,
                Type = OpenApiSecuritySchemeType.ApiKey,
                Scheme = JwtBearerDefaults.AuthenticationScheme
            });

            options.OperationProcessors.Add(new AspNetCoreOperationSecurityScopeProcessor("JWT"));
        }
>>>>>>> update template
    }
}
