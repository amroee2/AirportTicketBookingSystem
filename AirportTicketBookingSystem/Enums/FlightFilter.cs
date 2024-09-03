using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirportTicketBookingSystem.Enums
{
    public enum FlightFilter
    {
        Exit,
        ByDepartureCountry,
        ByDestinationCountry,
        ByDepartureDate,
        ByDepartureAirport,
        ByArrivalAirport,
        NoFilter
    }
}
