using AirportTicketBookingSystem.Airport_Repository;
using AirportTicketBookingSystem.Models;
using AirportTicketBookingSystem.Utilties.PassengerOptionsHandling.BookingHandling;
using AirportTicketBookingSystem.Utilties.PassengerOptionsHandling.FlightsHandling;

namespace AirportTicketBookingSystem.Utilties.ManagerOptionsHandling.FileHandling
{
    public class FlightHandler : IFlightHandler
    {
        public IFlightFilter _flightFilter { get; set; }
        public IFlightConstraints _flightConstraints { get; set; }

        public FlightHandler(IFlightFilter flightFilter, IFlightConstraints flightConstraints)
        {
            _flightFilter = flightFilter;
            _flightConstraints = flightConstraints;
        }

        public void CheckFlightConstraints()
        {
            _flightConstraints.CheckFlightConstraints();
        }
        public void CheckAvailableFlights(List<Flight> FlightsList)
        {
            _flightFilter.CheckAvailableFlights(FlightsList);
        }
    }

}
