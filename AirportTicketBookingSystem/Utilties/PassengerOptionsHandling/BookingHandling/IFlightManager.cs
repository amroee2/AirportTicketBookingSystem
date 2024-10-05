using AirportTicketBookingSystem.Models;

namespace AirportTicketBookingSystem.Utilties.PassengerOptionsHandling.BookingHandling
{
    public interface IFlightManager
    {
        public void ManageBookings(Passenger passenger);
        public void CancelPersonalBooking(Passenger passenger, int bookingId);
        public void ModifyPersonalBooking(Passenger passenger, int bookingId, ClassType classType);

    }
}
