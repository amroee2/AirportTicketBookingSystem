using AirportTicketBookingSystem.Enums;
using AirportTicketBookingSystem.Models;
using AirportTicketBookingSystem.Utilties.PassengerOptionsHandling.FlightsHandling;

namespace AirportTicketBookingSystem.Utilties.ManagerOptionsHandling.BookingHandling
{
    public class BookingChecker : IBookingFilter
    {
        private IManager _manager;
        private IFlightFilter _flightFilter;
        public BookingChecker(IManager manager)
        {
            _manager = manager;
            _flightFilter = new FlightChecker();
        }
        public void FilterBookings()
        {
            while (true)
            {
                Console.WriteLine("Filter by\n1-Booking ID\n2-Flight ID\n3-Passenger ID\n4-Flight Information\n0-Exit");
                if (Enum.TryParse(Console.ReadLine(), out BookingFilter operation))
                {
                    if (operation == BookingFilter.Exit)
                    {
                        break;
                    }
                }
                try
                {
                    CheckFilterType(operation);
                }
                catch (Exception exception)
                {
                    Console.WriteLine(exception.Message);
                }
            }
        }

        private void CheckFilterType(BookingFilter operation)
        {
            List<IBooking> bookings = new List<IBooking>();
            switch (operation)
            {
                case BookingFilter.ByBookingId:
                    Console.WriteLine("Enter Booking ID");
                    _ = int.TryParse(Console.ReadLine(), out int bookingId);
                    bookings = FilterByBookingId(bookingId);
                    PrintBookings(bookings);
                    break;
                case BookingFilter.ByFlightId:
                    Console.WriteLine("Enter flight id");
                    _ = int.TryParse(Console.ReadLine(), out int flightId);
                    bookings = FilterByFlightId(flightId);
                    PrintBookings(bookings);
                    break;
                case BookingFilter.ByPassengerId:
                    Console.WriteLine("Enter passenger id");
                    _ = int.TryParse(Console.ReadLine(), out int passengerId);
                    bookings = FilterByPassengerId(passengerId);
                    PrintBookings(bookings);
                    break;
                case BookingFilter.ByFlightInformation:
                    List<IFlight>? flights = _manager.AllBookings!.Select(flights => flights.Flight).Distinct().ToList();
                    _flightFilter.CheckAvailableFlights(flights);
                    break;
            }
        }
        private void PrintBookings(List<IBooking> bookings)
        {
            if (!bookings.Any())
            {
                Console.WriteLine("No bookings found");
                return;
            }

            foreach (var booking in bookings)
            {
                Console.WriteLine(booking);
            }
        }

        public List<IBooking> FilterByPassengerId(int passengerId)
        {
            List<IBooking>? bookings = _manager.AllBookings!.Where(b => b.PassengerId == passengerId).ToList();
            return bookings;
        }

        public List<IBooking> FilterByFlightId(int flightId)
        {
            List<IBooking>? bookings = _manager.AllBookings!.Where(b => b.Flight.FlightId == flightId).ToList();

            return bookings;
        }

        public List<IBooking> FilterByBookingId(int bookingId)
        {
            IBooking? booking = _manager.AllBookings!.FirstOrDefault(b => b.BookingId == bookingId);
            if (booking == null)
            {
                return new List<IBooking>();
            }
            return new List<IBooking> { booking };
        }

    }
}
