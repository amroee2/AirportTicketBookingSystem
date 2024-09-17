using AirportTicketBookingSystem.Models;

namespace AirportTicketBookingSystem.Utilties.PassengerOptionsHandling.BookingHandling
{
    public interface IPassengerBooking
    {
        void BookFlight(IPassenger passenger);
    }
}
