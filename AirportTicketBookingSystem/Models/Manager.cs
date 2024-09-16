using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirportTicketBookingSystem.Models
{
    public class Manager
    {
        public static List<IBooking>? AllBookings { get; set; } = new List<IBooking>();
        public static List<IPassenger>? AllPassengers { get; set; } = new List<IPassenger>();

        public static List<string> errorMessages = new List<string>();

    }
}
