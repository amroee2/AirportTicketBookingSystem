using AirportTicketBookingSystem.Models;

namespace AirportTicketBookingSystem.Utilties.PassengerOptionsHandling.FlightsHandling
{
    public interface IFlightFilter
    {
        void CheckAvailableFlights(List<Flight> FlightsList);
        void PrintAllFlights(List<Flight> FlightsList);
    }
}
