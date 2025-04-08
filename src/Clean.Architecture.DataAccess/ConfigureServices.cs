<<<<<<< HEAD
﻿using Clean.Architecture.DataAccess.DataContext;
=======
﻿using Clean.Architecture.Business.Repositories;
using Clean.Architecture.DataAccess.Contexts;
using Clean.Architecture.DataAccess.Repositories;
>>>>>>> update template
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Clean.Architecture.DataAccess
{
    public static class ConfigureServices
    {
<<<<<<< HEAD
        public static IServiceCollection AddDataAccess(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<AppicationDbContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
            });

            services.AddScoped<IApplicationDbContext>(provider => provider.GetRequiredService<AppicationDbContext>());
=======
        public static IServiceCollection AddDataServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
            {
                var connectionString = configuration.GetConnectionString("DefaultConnection");
                options.UseSqlServer(connectionString);
            });

            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<ISampleRepository, SampleRepository>();
>>>>>>> update template

            return services;
        }
    }
}
