using AirportTicketBookingSystem.Models;

namespace AirportTicketBookingSystem.Utilties.PassengerOptionsHandling.BookingHandling
{
    public interface IFlightManager
    {
        void ManageBookings(IPassenger passenger);
    }
}
