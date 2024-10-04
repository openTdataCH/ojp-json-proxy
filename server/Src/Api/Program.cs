using Api;
using Api.Middleware;
using Asp.Versioning.ApiExplorer;
using Serilog;

var myAllowSpecificOrigins = "_myAllowSpecificOrigins";
var builder = WebApplication.CreateBuilder(args);

var allowedHosts = builder.Configuration["AllowedHosts"] ?? "";

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: myAllowSpecificOrigins,
        policy  =>
        {
            policy.WithOrigins(allowedHosts);
            policy.AllowAnyMethod();
            policy.AllowAnyHeader();
        });
});

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
//builder.Services.AddSwaggerGen();

builder.Host.UseSerilog((context, configuration) =>
    configuration.ReadFrom.Configuration(context.Configuration));

builder.Services.AddControllers();

builder.Services.AddServices(builder.Configuration);

var app = builder.Build();

app.UseCors(myAllowSpecificOrigins);

app.Use((context, next) =>
{
    if (context.Request.Path.StartsWithSegments("/swagger"))
    {
        context.Response.Headers["Access-Control-Allow-Origin"] = "*";
    }

    context.Response.Headers["Access-Control-Expose-Headers"] = "*";
    
    return next.Invoke();
});


var provider = app.Services.GetRequiredService<IApiVersionDescriptionProvider>();
app.UseSwagger();
app.UseSwaggerUI(options =>
{
    foreach (var description in provider.ApiVersionDescriptions)
    {
        options.SwaggerEndpoint($"/swagger/{description.GroupName}/swagger.json", description.GroupName.ToUpperInvariant());
    }

    // Uncomment this if you want to serve the Swagger UI at the app's root (http://localhost:<port>/)
    // options.RoutePrefix = string.Empty;
});

app.UseSerilogRequestLogging();
app.UseOpenTelemetryPrometheusScrapingEndpoint(context => context.Request.Path == "/metrics");

app.UseHttpsRedirection();
app.MapControllers();

// add OJP-Proxy ResponseTime to ResponseHeaders
app.UseMiddleware<ResponseTimeMiddleware>();

app.Run();

