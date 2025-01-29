using Asp.Versioning.ApiExplorer;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace EcleticaBeerControl.Api.OpenApi
{
    public class ConfigureSwaggerGenOptions(IApiVersionDescriptionProvider provider) : IConfigureNamedOptions<SwaggerGenOptions>
    {
        private readonly IApiVersionDescriptionProvider _provider = provider;

        public void Configure(string? name, SwaggerGenOptions options)
        {
            Configure(options);
        }

        public void Configure(SwaggerGenOptions options)
        {
            foreach (ApiVersionDescription description in _provider.ApiVersionDescriptions)
            {
                var openApiInfo = new OpenApiInfo
                {
                    Title = $"EcleticaBeerControl.Api.v{description.ApiVersion}",
                    Version = description.ApiVersion.ToString(),
                    Description = "Ecletica Beer Control API",
                    Contact = new OpenApiContact
                    {
                        Name = "Victor Oliveira",
                        Email = "contact@ecletica.beer",
                        Url = new Uri("https://ecletica.beer")
                    }
                };

                options.SwaggerDoc(description.GroupName, openApiInfo);
            }
        }
    }
}
