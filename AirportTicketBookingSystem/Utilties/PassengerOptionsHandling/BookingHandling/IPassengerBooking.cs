using AirportTicketBookingSystem.Models;

namespace AirportTicketBookingSystem.Utilties.PassengerOptionsHandling.BookingHandling
{
    public interface IPassengerBooking
    {
        public void CollectFlightBookingDetails(Passenger passenger);
        public void BookFlight(Passenger passenger, int flightId, ClassType classType, List<Flight> AllFlights);
    }
}
