using AirportTicketBookingSystem.Utilties;

namespace AirportTicketBookingSystem.Models
{
    public class Manager : IManager
    {
        public List<IBooking>? AllBookings { get; set; } = new List<IBooking>();
        public List<IPassenger>? AllPassengers { get; set; } = new List<IPassenger>();
    }
}
