namespace Expenzio.Api.Extensions;

public static class WebApplicationExtensions
{
    public static WebApplication UseSwaggerUIWithVersioning(this WebApplication app)
    {
        app.UseSwaggerUI(options =>
        {
            var descriptions = app.DescribeApiVersions();
            foreach (var description in descriptions)
            {
                var url = $"/swagger/{description.GroupName}/swagger.json";
                var name = description.GroupName.ToUpperInvariant();
                options.SwaggerEndpoint(url, name);
            }
        });
        return app;
    }
}
