using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using VerticalSliceTemplate.Common.Behaviours;
using VerticalSliceTemplate.Infrastructure.Persistence;

namespace VerticalSliceTemplate
{
    public static class DependencyInjectionExtension
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

            services.AddMediatR(options =>
            {
                options.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
                options.AddOpenBehavior(typeof(ValidationBehavior<,>));
            });


            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(LoggingBehavior<,>));


            return services;
        }

        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            bool isDevelopment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")
                        .Equals("Developments");

            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseNpgsql(isDevelopment ? configuration.GetConnectionString("ReadConnection")
                                    : configuration.GetConnectionString("WriteConnection")));

            return services;
        }
    }
}
