using AirportTicketBookingSystem.Models;

namespace AirportTicketBookingSystem.Utilties.PassengerOptionsHandling.FlightsHandling
{
    public interface IFlightHandler
    {
        void CheckAvailableFlights(List<Flight> FlightsList);
        void CheckFlightConstraints();
    }
}
