using Microsoft.Extensions.DependencyInjection;
using WingsOn.Dal;
using WingsOn.Dal.Abstract;

namespace WingsOn.Application.Extensions
{
    public static class AppLayerServices
    {
        public static IServiceCollection AddAppLayerServices(this IServiceCollection services)
        {
            services.AddSingleton<IAirlineRepository, AirlineRepository>();
            services.AddSingleton<IAirportRepository, AirportRepository>();
            services.AddSingleton<IBookingRepository, BookingRepository>();
            services.AddSingleton<IFlightRepository, FlightRepository>();
            services.AddSingleton<IPersonRepository, PersonRepository>();

            return services;
        }
    }
}