using AirportTicketBookingSystem.Models;

namespace AirportTicketBookingSystem.Utilties.PassengerOptionsHandling.BookingHandling
{
    public interface IFlightManager
    {
        public void ManageBookings(IPassenger passenger);
        public void CancelPersonalBooking(IPassenger passenger, int bookingId);
        public void ModifyPersonalBooking(IPassenger passenger, int bookingId, ClassType classType);

    }
}
