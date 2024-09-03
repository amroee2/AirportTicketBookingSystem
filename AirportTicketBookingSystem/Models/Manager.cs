using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirportTicketBookingSystem.Models
{
    public class Manager
    {
        public static List<Booking>? AllBookings { get; set; } = new List<Booking>();
        public static List<Passenger>? AllPassengers { get; set; } = new List<Passenger>();

        public static List<string> errorMessages = new List<string>();

    }
}
