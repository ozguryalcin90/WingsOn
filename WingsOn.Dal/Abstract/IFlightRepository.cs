﻿using WingsOn.Domain;

namespace WingsOn.Dal.Abstract
{
    public interface IFlightRepository : IRepository<Flight>
    {
        Flight GetByFlightNumber(string flightNumber);
    }
}