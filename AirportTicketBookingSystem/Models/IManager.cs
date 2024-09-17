namespace AirportTicketBookingSystem.Models
{
    public interface IManager
    {
        public List<IBooking>? AllBookings { get; set; }
        public List<IPassenger>? AllPassengers { get; set; }
    }
}
