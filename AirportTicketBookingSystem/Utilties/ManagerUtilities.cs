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
                    if (Enum.TryParse(Console.ReadLine(), out ManagerOption operation))
                    {
                        switch (operation)
                        {
                            case ManagerOption.Exit:
                                return;
                            case ManagerOption.FilterBookings:
                                FilterBookings();
                                break;
                            case ManagerOption.ExportBookingsToCsv:
                                _ = FlightExport.ExportToCsvAsync();
                                break;
                            case ManagerOption.ImportBookingsToCsv:
                                _ = Utilities.GenerateFlightsAsync();
                                break;
                            case ManagerOption.ViewErrorMessages:
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
                    if (Enum.TryParse(Console.ReadLine(), out BookingFilter operation))
                    {
                        switch (operation)
                        {
                            case BookingFilter.Exit:
                                Console.WriteLine("Exiting");
                                return;
                            case BookingFilter.ByBookingId:
                                Console.WriteLine("Enter Booking ID");
                                _ = int.TryParse(Console.ReadLine(), out int bookingId);
                                Booking? booking = Manager.AllBookings!.FirstOrDefault(b => b.BookingId == bookingId);
                                Console.WriteLine(booking == null ? "Booking not found" : booking);
                                break;
                            case BookingFilter.ByFlightId:
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
                            case BookingFilter.ByPassengerId:
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
                            case BookingFilter.ByFlightInformation:
                                List<IFlight>? flights = Manager.AllBookings!.Select(flights => flights.Flight).Distinct().ToList();
                                PassengerUtilities.CheckAvailableFlights(flights);
                                break;
                        }
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
