using AirportTicketBookingSystem.Models;

namespace AirportTicketBookingSystem.Utilties.PassengerOptionsHandling.BookingHandling
{
    public interface IPassengerBooking
    {
        public void CollectFlightBookingDetails(IPassenger passenger);
        public void BookFlight(IPassenger passenger, int flightId, ClassType classType, List<IFlight> AllFlights);
    }
}
