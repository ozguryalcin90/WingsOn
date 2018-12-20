using System.Collections.Generic;
using WingsOn.Domain;

namespace WingsOn.Dal.Abstract
{
    public interface IBookingRepository : IRepository<Booking>
    {
        IEnumerable<Booking> GetBookings(string flightNumber);
    }
}