using System.Net.Http.Headers;
using Api.Metrics;
using Api.Models;
using Api.Swagger;
using Asp.Versioning.Conventions;
using OpenTelemetry.Metrics;
using OpenTelemetry.Resources;
using TransportProxy.Application.LocationInformation;
using TransportProxy.Application.LocationInformation.Metrics;
using TransportProxy.Application.LocationInformation.V1;
using TransportProxy.Application.StopEvent;
using TransportProxy.Application.StopEvent.Metrics;

namespace Api;

public static class ServiceBuilder
{
    public static IServiceCollection AddServices(this IServiceCollection services, IConfiguration configuration)
    {
        var ojpServiceUrlV1 = configuration["OjpServiceUrlV1"] ?? "";
        var ojpServiceUrlV2 = configuration["OjpServiceUrlV2"] ?? "";

        // add services
        services.AddHttpClient("OjpServiceV1", client =>
        {
            client.BaseAddress = new Uri(ojpServiceUrlV1);

            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/xml"));
        });
        services.AddHttpClient("OjpServiceV2", client =>
        {
            client.BaseAddress = new Uri(ojpServiceUrlV2);

            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/xml"));
        });

        services.AddAutoMapper(cfg =>
        {
            StationBoardDto.ConfigureMapping(cfg);
            LocationInformationDto.ConfigureMapping(cfg);
            ExceptionDto.ConfigureMapping(cfg);
        });

        //v1
        services.AddScoped<StopEventService>();
        services.AddScoped<LocationInformationService>();
        
        //v2
        services.AddScoped<TransportProxy.Application.LocationInformation.V2.LocationInformationService>();

        services.AddHttpContextAccessor();
        
        services.AddApiVersioning(options =>
            {
                options.ReportApiVersions = true;
                options.AssumeDefaultVersionWhenUnspecified = true;
            })
            .AddMvc(options =>
            {
                options.Conventions.Add(new VersionByNamespaceConvention());
            })
            .AddApiExplorer(options =>
            {
                options.GroupNameFormat = "'v'VVV";
                options.SubstituteApiVersionInUrl = true;
            });
        
        services.AddSwager();

        services.AddSingleton<RequestMetrics>();
        services.AddSingleton<StopEventMetrics>();
        services.AddSingleton<LocationInformationMetrics>();
        
        services.AddOpenTelemetry()
            .WithMetrics(opts => opts
                .SetResourceBuilder(ResourceBuilder.CreateDefault().AddService("OjpProxy"))
                .AddMeter(nameof(RequestMetrics))
                .AddMeter(nameof(StopEventMetrics))
                .AddMeter(nameof(LocationInformationMetrics))
                .AddPrometheusExporter()
            );
        
        return services;
    }
}