using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace AirportTicketBookingSystem.Models
{
    public class Flight
    {
        public int FlightId { get; set; }
        public string? DepartureDate { get; set; }
        public string? DepartureCountry { get; set; }
        public string? DestinationCountry { get; set; }
        public string? DepartureAirport { get; set; }
        public string? ArrivalAirport { get; set; }
       
        public Flight(int flightId, string departureDate, string departureCountry,
            string destinationCountry, string departureAirport, string arrivalAirport)
        {
            FlightId = flightId;
            DepartureDate = departureDate;
            DepartureCountry = departureCountry;
            DestinationCountry = destinationCountry;
            DepartureAirport = departureAirport;
            ArrivalAirport = arrivalAirport;
        }
        public override string ToString()
        {
            return $"Flight ID: {FlightId}, Departure: {DepartureDate} from {DepartureCountry} ({DepartureAirport}) to {DestinationCountry} ({ArrivalAirport})";
        }
    }
}
