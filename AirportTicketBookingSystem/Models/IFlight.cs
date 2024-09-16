using AirportTicketBookingSystem.Validation;
using System.ComponentModel.DataAnnotations;

namespace AirportTicketBookingSystem.Models
{
    public interface IFlight
    {
        public int FlightId { get; set; }

        public DateTime DepartureDate { get; set; }

        public string DepartureCountry { get; set; }

        public string DestinationCountry { get; set; }

        public string DepartureAirport { get; set; }

        public string ArrivalAirport { get; set; }
    }
}
