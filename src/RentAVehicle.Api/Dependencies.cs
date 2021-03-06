using RentAVehicle.Domain.Features.Rentals;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using System;
using BuildingBlocks.EventStore;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Hosting;
using Hellang.Middleware.ProblemDetails;
using BuildingBlocks.Core;

namespace RentAVehicle.Api
    
{
    public static class Dependencies
    {
        public static void Configure(IServiceCollection services, IConfiguration configuration, IWebHostEnvironment environment)
        {
            services.AddProblemDetails();

            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));

            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "",
                    Description = "",
                    TermsOfService = new Uri("https://example.com/terms"),
                    Contact = new OpenApiContact
                    {
                        Name = "",
                        Email = ""
                    },
                    License = new OpenApiLicense
                    {
                        Name = "Use under MIT",
                        Url = new Uri("https://opensource.org/licenses/MIT"),
                    }
                });

                options.CustomSchemaIds(x => x.FullName);
            });

            services.AddCors(options => options.AddPolicy("CorsPolicy",
                builder => builder
                .WithOrigins("http://localhost:4200")
                .AllowAnyMethod()
                .AllowAnyHeader()
                .SetIsOriginAllowed(isOriginAllowed: _ => true)
                .AllowCredentials()));

            services.AddHttpContextAccessor();

            services.AddEventStore(options =>
            {
                options.UseSqlServer(configuration["Data:DefaultConnection:ConnectionString"],
                    builder => builder.MigrationsAssembly("RentAVehicle.Api")
                        .EnableRetryOnFailure())
                .UseLoggerFactory(EventStoreDbContext.ConsoleLoggerFactory)
                .EnableSensitiveDataLogging();
            });

            services.AddMediatR(typeof(GetRentals));

            services.AddControllers();
        }
    }
}
