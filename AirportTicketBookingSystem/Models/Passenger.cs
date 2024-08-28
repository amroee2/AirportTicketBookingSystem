using AirportTicketBookingSystem.Utilties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirportTicketBookingSystem.Models
{
    public class Passenger
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public int PassengerId { get; set; }
        public List<Booking>? Bookings { get; set; }

        public Passenger(string firstName, string lastName, int passengerId)
        {
            FirstName = firstName;
            LastName = lastName;
            PassengerId = passengerId;
            Bookings = new List<Booking>();
        }
    }
}
