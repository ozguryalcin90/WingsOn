using System.Collections.Generic;
using WingsOn.Domain;

namespace WingsOn.Application.Abstract
{
    public interface IBookingAppService
    {
        IEnumerable<Person> Get(string flightNumber);
    }
}