using AirportTicketBookingSystem.Enums;
using AirportTicketBookingSystem.Models;
using AirportTicketBookingSystem.Utilties.PassengerOptionsHandling.FlightsHandling;

namespace AirportTicketBookingSystem.Utilties.ManagerOptionsHandling.BookingHandling
{
    public class BookingChecker : IBookingFilter
    {
        private Manager _manager;
        private IFlightFilter _flightFilter;
        public BookingChecker(Manager manager)
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
            List<Booking> bookings = new List<Booking>();
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
                    List<Flight>? flights = _manager.AllBookings!.Select(flights => flights.Flight).Distinct().ToList();
                    _flightFilter.CheckAvailableFlights(flights);
                    break;
            }
        }
        private void PrintBookings(List<Booking> bookings)
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

        public List<Booking> FilterByPassengerId(int passengerId)
        {
            List<Booking>? bookings = _manager.AllBookings!.Where(b => b.PassengerId == passengerId).ToList();
            return bookings;
        }

        public List<Booking> FilterByFlightId(int flightId)
        {
            List<Booking>? bookings = _manager.AllBookings!.Where(b => b.Flight.FlightId == flightId).ToList();

            return bookings;
        }

        public List<Booking> FilterByBookingId(int bookingId)
        {
            Booking? booking = _manager.AllBookings!.FirstOrDefault(b => b.BookingId == bookingId);
            if (booking == null)
            {
                return new List<Booking>();
            }
            return new List<Booking> { booking };
        }

    }
}
