<<<<<<< HEAD
﻿using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Net.Http.Headers;

namespace Clean.Architecture.External
{
    public static class ConfigureServices
    {
        public static IServiceCollection AddExternalServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddHttpClient("SampleClient", client =>
            {
                client.BaseAddress = new Uri(configuration.GetValue<string>("BaseAddress:SampleClient")!);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            });

            return services;
        }
=======
﻿using Clean.Architecture.External.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Refit;

namespace Clean.Architecture.External;

public static class ConfigureServices
{
    public static IServiceCollection AddExternalServices(this IServiceCollection services, IConfiguration configuration)
    {
        services
            .AddRefitClient<ISampleService>()
            .ConfigureHttpClient(c => c.BaseAddress = new Uri(configuration["BaseAddress:SampleClient"]!));

        return services;
>>>>>>> update template
    }
}
