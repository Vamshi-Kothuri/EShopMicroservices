
namespace Ordering.API
{
    public static class DependencyInjection
    {
        //Add services to the Container
        public static IServiceCollection AddApiServices(this IServiceCollection services)
        {
            services.AddCarter();
            return services;
        }

        //Register the service into HttpRequest pipeline(After Build the application)
        public static WebApplication UseApiServices(this WebApplication app)
        {
            app.MapCarter();
            return app;
        }
    }
}
