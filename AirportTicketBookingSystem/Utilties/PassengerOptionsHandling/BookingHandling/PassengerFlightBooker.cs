using AirportTicketBookingSystem.Models;
using AirportTicketBookingSystem.Utilties.PassengerOptionsHandling.FlightsHandling;

namespace AirportTicketBookingSystem.Utilties.PassengerOptionsHandling.BookingHandling
{
    public class PassengerFlightBooker : IPassengerBooking
    {
        public static int incrementBookingId = 1;
        public IManager _manager { get; set; }
        public IFlightFilter _flightFilter { get; set; }

        public PassengerFlightBooker(IManager manager, IFlightFilter flightFilter)
        {
            _manager = manager;
            _flightFilter = flightFilter;
        }

        public void CollectFlightBookingDetails(IPassenger passenger)
        {
            _flightFilter.PrintAllFlights(GeneralUtility.flights);

            Console.WriteLine("Enter the flight ID you want to book:");
            if (!int.TryParse(Console.ReadLine(), out int flightId))
            {
                Console.WriteLine("Invalid Flight ID. Please enter a valid number.");
                return;
            }

            Console.WriteLine("Select class type:\n1-Economy\n2-Business\n3-First Class");
            if (!int.TryParse(Console.ReadLine(), out int classType) || !Enum.IsDefined(typeof(ClassType), classType))
            {
                Console.WriteLine("Invalid Class Type. Please select 1, 2, or 3.");
                return;
            }

            BookFlight(passenger, flightId, (ClassType)classType, GeneralUtility.flights);
        }

        public void BookFlight(IPassenger passenger, int flightId, ClassType classType, List<IFlight> AllFlights)
        {
            IFlight? selectedFlight = AllFlights.FirstOrDefault(f => f.FlightId == flightId);

            var existingBooking = passenger.Bookings.FirstOrDefault(b => b.Flight.FlightId == flightId);
            if (existingBooking != null)
            {
                Console.WriteLine("You have already booked this flight");
                return;
            }

            if (selectedFlight == null)
            {
                Console.WriteLine("Invalid Flight ID");
                return;
            }

            IBooking booking = new Booking(incrementBookingId, $"{passenger.FirstName} {passenger.LastName}", passenger.PassengerId, selectedFlight, classType);
            incrementBookingId++;

            passenger.Bookings.Add(booking);
            _manager.AllBookings!.Add(booking);

            Console.WriteLine("Booking Successful! Booking details:");
            Console.WriteLine(booking.ToString());
        }
    }
}
