using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace AirportTicketBookingSystem
{
    public class Flight
    {
        public int FlightId { get; set; }
        public int price;
        public string? DepartureDate { get; set; }
        public string? DeparetureCountr { get; set; }
        public string? DestinationCountry { get; set; }
        public string? DepartureAirport { get; set; }
        public string? ArrivalAirport { get; set; }
        public ClassType ClassType { get; set; }
        public int Price
        {
            get
            {
                switch (this.ClassType)
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

    }
}
