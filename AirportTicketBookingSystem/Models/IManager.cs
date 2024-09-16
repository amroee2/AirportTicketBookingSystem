using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirportTicketBookingSystem.Models
{
    public interface IManager
    {
        public List<IBooking>? AllBookings { get; set; }
        public List<IPassenger>? AllPassengers { get; set; }
    }
}
