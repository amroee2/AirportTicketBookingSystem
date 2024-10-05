using AirportTicketBookingSystem.Enums;
using AirportTicketBookingSystem.Models;
using AirportTicketBookingSystem.Utilties.PassengerOptionsHandling.AccountHandling;
using AirportTicketBookingSystem.Utilties.PassengerOptionsHandling.BookingHandling;
using AirportTicketBookingSystem.Utilties.PassengerOptionsHandling.FlightsHandling;

namespace AirportTicketBookingSystem.Utilties.PassengerOptionsHandling
{
    public class PassengerUtilities
    {
        public IAccount _account { get; set; }
        public IBookingHandler _passengerBooking { get; set; }

        public IFlightHandler _flightHandler { get; set; }
        public PassengerUtilities(IAccount account, IBookingHandler passengerBooking,IFlightHandler flightHandler)
        {
            _account = account;
            _passengerBooking = passengerBooking;
            _flightHandler = flightHandler;
        }

        public void PrintMenu()
        {
            Passenger passenger = _account.RequestLogInDetails();
            while (true)
            {
                try
                {
                    Console.WriteLine("1-Book a Flight\n2-Check Available flights\n3-Manage flights\n4-Check flight constraints\n0-Exit");
                    if (Enum.TryParse(Console.ReadLine(), out PassengerOption operation))
                    {
                        if (operation == PassengerOption.Exit)
                        {
                            break;
                        }
                        HandlePassengerRequest(passenger, operation);
                    }
                }
                catch (Exception exception)
                {
                    Console.WriteLine(exception.Message);
                }
            }
        }
        private void HandlePassengerRequest(Passenger passenger, PassengerOption operation)
        {
            switch (operation)
            {
                case PassengerOption.BookFlight:
                    _passengerBooking.BookFlight(passenger);
                    break;
                case PassengerOption.CheckAvailableFlights:
                    _flightHandler.CheckAvailableFlights(GeneralUtility.flights);
                    break;
                case PassengerOption.ManageFlight:
                    _passengerBooking.ManageBookings(passenger);
                    break;
                case PassengerOption.CheckFlightConstraints:
                    _flightHandler.CheckFlightConstraints();
                    break;
                default:
                    Console.WriteLine("Invalid Option");
                    break;
            }
        }
    }
}
