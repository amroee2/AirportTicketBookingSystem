using System;
using System.Collections.Generic;
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
        public string? DeparetureCountry { get; set; }
        public string? DestinationCountry { get; set; }
        public string? DepartureAirport { get; set; }
        public string? ArrivalAirport { get; set; }
        public ClassType ClassType { get; set; }
        public int Price
        {
            get
            {
                switch (ClassType)
                {
                    case ClassType.Economy:
                        return 100;
                    case ClassType.Business:
                        return 200;
                    case ClassType.FirstClass:
                        return 300;
                }
                return 0;
            }
        }
        public Flight(int flightId, string departureDate, string departureCountry,
            string destinationCountry, string departureAirport, string arrivalAirport, ClassType classType)
        {
            FlightId = flightId;
            DepartureDate = departureDate;
            DeparetureCountry = departureCountry;
            DestinationCountry = destinationCountry;
            DepartureAirport = departureAirport;
            ArrivalAirport = arrivalAirport;
            ClassType = classType;
        }
    }
}
