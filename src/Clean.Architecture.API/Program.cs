<<<<<<< HEAD
using Clean.Architecture.Business;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
=======
>>>>>>> update template
using Clean.Architecture.API.Middleware;
using Clean.Architecture.DataAccess;
using Clean.Architecture.External;
using Clean.Architecture.API;
using Clean.Architecture.Business;
using Clean.Architecture.DataAccess;
using Clean.Architecture.External;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
<<<<<<< HEAD
builder.Services.AddDataAccess(builder.Configuration);
builder.Services.AddExternalServices(builder.Configuration);
=======
builder.Services.AddDataServices(builder.Configuration);
//builder.Services.AddExternalServices(builder.Configuration);
>>>>>>> update template
builder.Services.AddBusinessServices();
builder.Services.AddApiServices();

var app = builder.Build();
app.UseMiddleware<ExceptionHandlingMiddleware>();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseOpenApi();

    app.UseSwaggerUi(s => { s.AdditionalSettings.Add("displayRequestDuration", true); });

    app.UseReDoc(options =>
    {
        options.Path = "/developer/api";
        options.DocumentPath = "/swagger/v1/swagger.json";
    });
}

app.UseHttpsRedirection();

//app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
