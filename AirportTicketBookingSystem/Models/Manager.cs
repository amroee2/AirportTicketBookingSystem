using AirportTicketBookingSystem.Utilties;

namespace AirportTicketBookingSystem.Models
{
    public class Manager
    {
        public List<Booking>? AllBookings { get; set; } = new List<Booking>();
        public List<Passenger>? AllPassengers { get; set; } = new List<Passenger>();
    }
}
