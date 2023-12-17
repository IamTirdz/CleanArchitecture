using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Clean.Architecture.API;

public class SwaggerUIOptions : IConfigureNamedOptions<SwaggerGenOptions>
{
    private readonly IApiVersionDescriptionProvider _provider;

    public SwaggerUIOptions(IApiVersionDescriptionProvider provider)
    {
        _provider = provider;
    }

    public void Configure(SwaggerGenOptions options)
    {
        foreach (var description in _provider.ApiVersionDescriptions)
        {
            options.SwaggerDoc(description.GroupName, CreateVersionInfo(description));
        }
    }

    public void Configure(string name, SwaggerGenOptions options)
    {
        Configure(options);
    }

    private OpenApiInfo CreateVersionInfo(ApiVersionDescription description)
    {
        var info = new OpenApiInfo
        {
            Title = "Clean.Architecture API",
            Version = description.ApiVersion.ToString(),
            //Contact = new OpenApiContact { Email = "developers@mail.com", Name = "Developers" },
            Description = "No description"
        };

        if (description.IsDeprecated)
        {
            info.Description += " This API version has been deprecated. Please use one of the new API's available from the explorer.";
        }

        return info;
    }
}
