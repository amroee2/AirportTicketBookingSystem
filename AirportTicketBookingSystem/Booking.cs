using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirportTicketBookingSystem
{
    public class Booking
    {
        public int BookingId { get; set; }
        public string? PassengerName { get; set; }
        public int PassengerId { get; set; }
        public Flight? Flight { get; set; }

        public Booking(int bookingId, string passengerName, int passengerId, Flight flight)
        {
            this.BookingId = bookingId;
            this.PassengerName = passengerName;
            this.PassengerId = passengerId;
            this.Flight = flight;
        }
    }
}
