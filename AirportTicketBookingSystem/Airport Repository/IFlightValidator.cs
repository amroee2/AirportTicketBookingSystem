using AirportTicketBookingSystem.Models;

namespace AirportTicketBookingSystem.Airport_Repository
{
    public interface IFlightValidator
    {
        void ValidateFlight(IFlight flight);
        void PrintValidationResults();
    }
}
