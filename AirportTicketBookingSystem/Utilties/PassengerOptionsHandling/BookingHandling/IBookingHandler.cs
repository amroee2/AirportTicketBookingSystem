using AirportTicketBookingSystem.Models;

namespace AirportTicketBookingSystem.Utilties.PassengerOptionsHandling.BookingHandling
{
    public interface IBookingHandler
    {
        void BookFlight(Passenger passenger);
        void ManageBookings(Passenger passenger);
    }
}
