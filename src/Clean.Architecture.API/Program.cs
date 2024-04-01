using Clean.Architecture.Business;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Clean.Architecture.API.Middleware;
using Clean.Architecture.DataAccess;
using Clean.Architecture.External;
using Clean.Architecture.API;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDataAccess(builder.Configuration);
builder.Services.AddExternalServices(builder.Configuration);
builder.Services.AddBusinessServices();
builder.Services.AddApiServices();

var app = builder.Build();
app.UseMiddleware<ExceptionHandlingMiddleware>();
// Configure the HTTP request pipeline.
var apiVersionDescriptor = app.Services.GetRequiredService<IApiVersionDescriptionProvider>();
if (!app.Environment.IsProduction())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        foreach (var description in apiVersionDescriptor.ApiVersionDescriptions)
        {
            options.SwaggerEndpoint($"/swagger/{description.GroupName}/swagger.json",
                description.GroupName.ToUpperInvariant());
        }
    });
}

app.UseHttpsRedirection();

//app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
