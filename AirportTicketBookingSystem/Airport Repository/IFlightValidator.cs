using AirportTicketBookingSystem.Models;

namespace AirportTicketBookingSystem.Airport_Repository
{
    public interface IFlightValidator
    {
        void ValidateFlight(Flight flight);
        void PrintValidationResults();
    }
}
