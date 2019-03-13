using Microsoft.Extensions.DependencyInjection;
using WingsOn.Dal;
using WingsOn.Dal.Abstract;

namespace WingsOn.Application.Extensions
{
    public static class AppLayerServices
    {
        public static IServiceCollection AddAppLayerServices(this IServiceCollection services)
        {
            services.AddTransient<IAirlineRepository, AirlineRepository>();
            services.AddTransient<IAirportRepository, AirportRepository>();
            services.AddTransient<IBookingRepository, BookingRepository>();
            services.AddTransient<IFlightRepository, FlightRepository>();
            services.AddTransient<IPersonRepository, PersonRepository>();

            return services;
        }
    }
}