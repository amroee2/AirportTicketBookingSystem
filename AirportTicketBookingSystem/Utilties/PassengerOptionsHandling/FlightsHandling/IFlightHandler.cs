using AirportTicketBookingSystem.Models;

namespace AirportTicketBookingSystem.Utilties.PassengerOptionsHandling.FlightsHandling
{
    public interface IFlightHandler
    {
        void CheckAvailableFlights(List<IFlight> FlightsList);
        void CheckFlightConstraints();
    }
}
