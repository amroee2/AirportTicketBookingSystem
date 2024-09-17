using AirportTicketBookingSystem.Models;

namespace AirportTicketBookingSystem.Utilties.PassengerOptionsHandling.BookingHandling
{
    public interface IBookingHandler
    {
        void BookFlight(IPassenger passenger);
        void ManageBookings(IPassenger passenger);
    }
}
