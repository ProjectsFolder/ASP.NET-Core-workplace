using Asp.Versioning.ApiExplorer;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Api.Build.Documentation.Options;

public class SwaggerDocsOptions(IApiVersionDescriptionProvider provider)
    : IConfigureOptions<SwaggerGenOptions>
{
    public void Configure(SwaggerGenOptions options)
    {
        foreach (var description in provider.ApiVersionDescriptions)
        {
            options.SwaggerDoc(description.GroupName, CreateInfoForApiVersion(description));
        }
    }

    private static OpenApiInfo CreateInfoForApiVersion(ApiVersionDescription description)
    {
        var version = description.ApiVersion.ToString();
        var info = new OpenApiInfo()
        {
            Title = string.Concat("Api v", version),
            Version = version,
        };

        if (description.IsDeprecated)
        {
            info.Description += " Deprecated.";
        }

        return info;
    }
}
