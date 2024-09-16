using AirportTicketBookingSystem.Enums;
using AirportTicketBookingSystem.Models;

namespace AirportTicketBookingSystem.Utilties
{
    public class BookingChecker : IBookingFilter
    {
        private IManager _manager;
        public BookingChecker(IManager manager)
        {
            _manager = manager;
        }
        public void FilterBookings()
        {
            while (true)
            {
                Console.WriteLine("Filter by\n1-Booking ID\n2-Flight ID\n3-Passenger ID\n4-Flight Information\n0-Exit");
                string input = Console.ReadLine();
                if (input == "0")
                {
                    return;
                }
                try
                {
                    CheckFilterType(input);
                }
                catch (Exception exception)
                {
                    Console.WriteLine(exception.Message);
                }
            }
        }

        private void CheckFilterType(string input)
        {
            if (!Enum.TryParse(input, out BookingFilter operation))
            {
                return;
            }
            switch (operation)
            {
                case BookingFilter.Exit:
                    Console.WriteLine("Exiting");
                    return;
                case BookingFilter.ByBookingId:
                    FilterByBookingId();
                    break;
                case BookingFilter.ByFlightId:
                    FilterByFlightId();
                    break;
                case BookingFilter.ByPassengerId:
                    FilterByPassengerId();
                    break;
                case BookingFilter.ByFlightInformation:
                    List<IFlight>? flights = _manager.AllBookings!.Select(flights => flights.Flight).Distinct().ToList();
                    PassengerUtilities.CheckAvailableFlights(flights);
                    break;
            }
        }

        private void FilterByPassengerId()
        {
            Console.WriteLine("Enter passenger id");
            _ = int.TryParse(Console.ReadLine(), out int passengerId);
            List<IBooking>? bookingsByPassenger = _manager.AllBookings!.Where(b => b.PassengerId == passengerId).ToList();
            if (!bookingsByPassenger.Any())
            {
                Console.WriteLine("No bookings found");
            }
            else
            {
                foreach (var book in bookingsByPassenger)
                {
                    Console.WriteLine(book);
                }
            }
        }

        private void FilterByFlightId()
        {
            Console.WriteLine("Enter flight id");
            _ = int.TryParse(Console.ReadLine(), out int flightId);
            List<IBooking>? bookings = _manager.AllBookings!.Where(b => b.Flight.FlightId == flightId).ToList();
            if (!bookings.Any())
            {
                Console.WriteLine("No bookings found");
            }
            else
            {
                foreach (var book in bookings)
                {
                    Console.WriteLine(book);
                }
            }
        }

        private void FilterByBookingId()
        {
            Console.WriteLine("Enter Booking ID");
            _ = int.TryParse(Console.ReadLine(), out int bookingId);
            IBooking? booking = _manager.AllBookings!.FirstOrDefault(b => b.BookingId == bookingId);
            Console.WriteLine(booking == null ? "Booking not found" : booking);
        }
    }
}
