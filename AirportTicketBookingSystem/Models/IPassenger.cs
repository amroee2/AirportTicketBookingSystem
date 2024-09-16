using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirportTicketBookingSystem.Models
{
    public interface IPassenger
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public int PassengerId { get; set; }
        public List<IBooking>? Bookings { get; set; }
    }
}
