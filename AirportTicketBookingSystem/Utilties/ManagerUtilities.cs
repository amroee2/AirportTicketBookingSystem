using AirportTicketBookingSystem.Airport_Repository;
using AirportTicketBookingSystem.Enums;
using AirportTicketBookingSystem.Models;

namespace AirportTicketBookingSystem.Utilties
{
    public class ManagerUtilities
    {
        public static void PrintMenu()
        {
            Console.WriteLine("Welcome Manager!");
            while (true)
            {
                try
                {
                    Console.WriteLine("1-Filter Bookings\n2-Export to CSV\n3-Import from CSV\n4-View error messages from last import\n0-Go back");
                    _ = int.TryParse(Console.ReadLine(), out int operation);

                    switch (operation)
                    {
                        case 0:
                            return;
                        case (int) ManagerOption.FilterBookings:
                            FilterBookings();
                            break;
                        case (int) ManagerOption.ExportBookingsToCsv:
                            _ = FlightsRepository.ExportToCsvAsync();
                            break;
                        case (int) ManagerOption.ImportBookingsToCsv:
                            _ = Utilities.GenerateFlights();
                            break;
                        case (int) ManagerOption.ViewErrorMessages:
                            foreach (var error in Manager.errorMessages)
                            {
                                Console.WriteLine(error);
                            }
                            break;
                        default:
                            Console.WriteLine("Invalid Option");
                            break;
                    }
                }
                catch (Exception exception)
                {
                    Console.WriteLine(exception.Message);
                }
            }
        }
        public static void FilterBookings()
        {
            while (true)
            {
                Console.WriteLine("Filter by\n1-Booking ID\n2-Flight ID\n3-Passenger ID\n4-Flight Information\n0-Exit");
                try
                {
                    _ = int.TryParse(Console.ReadLine(), out int operation);
                    switch (operation)
                    {
                        case 0:
                            Console.WriteLine("Exiting");
                            return;
                        case (int) BookingFilter.ByBookingId:
                            Console.WriteLine("Enter Booking ID");
                            _ = int.TryParse(Console.ReadLine(), out int bookingId);
                            Booking? booking = Manager.AllBookings!.FirstOrDefault(b => b.BookingId == bookingId);
                            Console.WriteLine(booking == null ? "Booking not found" : booking);
                            break;
                        case (int) BookingFilter.ByFlightId:
                            Console.WriteLine("Enter flight id");
                            _ = int.TryParse(Console.ReadLine(), out int flightId);
                            List<Booking>? bookings = Manager.AllBookings!.Where(b => b.Flight.FlightId == flightId).ToList();
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
                            break;
                        case (int) BookingFilter.ByPassengerId:
                            Console.WriteLine("Enter passenger id");
                            _ = int.TryParse(Console.ReadLine(), out int passengerId);
                            List<Booking>? bookingsByPassenger = Manager.AllBookings!.Where(b => b.PassengerId == passengerId).ToList();
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
                            break;
                        case (int) BookingFilter.ByFlightInformation:
                            List<Flight>? flights = Manager.AllBookings!.Select(flights => flights.Flight).Distinct().ToList();
                            PassengerUtilities.CheckAvailableFlights(flights);
                            break;
                    }
                }
                catch (Exception exception)
                {
                    Console.WriteLine(exception.Message);
                }
            }
        }
    }
}
