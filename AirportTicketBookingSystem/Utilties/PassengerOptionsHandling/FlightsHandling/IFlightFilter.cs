using AirportTicketBookingSystem.Models;

namespace AirportTicketBookingSystem.Utilties.PassengerOptionsHandling.FlightsHandling
{
    public interface IFlightFilter
    {
        void CheckAvailableFlights(List<IFlight> FlightsList);
        void PrintAllFlights(List<IFlight> FlightsList);
    }
}
