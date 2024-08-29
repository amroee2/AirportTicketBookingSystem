using AirportTicketBookingSystem.Airport_Repository;
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
                Console.WriteLine("1-Filter Bookings\n2-Export to CSV\n0-Go back");
                int op = Convert.ToInt32(Console.ReadLine());

                switch (op)
                {
                    case 0: return;
                    case 1:
                        FilterBookings();
                        break;
                    case 2:
                        FlightsRepository.ExportToCsv();
                        break;
                    default:
                        Console.WriteLine("Invalid Option");
                        break;
                }
            }
        }
        public static void FilterBookings()
        {
            while (true)
            {
                Console.WriteLine("Filter by\n1-Booking ID\n2-Flight ID\n3-Passenger ID\n4-Flight Information");
                int op = Convert.ToInt32(Console.ReadLine());
                switch (op)
                {
                    case 1:
                        Console.WriteLine("Enter Booking ID");
                        int bookingId = Convert.ToInt32(Console.ReadLine());
                        Booking? booking = Manager.AllBookings!.FirstOrDefault(b => b.BookingId == bookingId);
                        if (booking == null)
                        {
                            Console.WriteLine("Booking not found");
                        }
                        else
                        {
                            Console.WriteLine(booking);
                        }
                        break;
                    case 2:
                        Console.WriteLine("Enter flight id");
                        int flightId = Convert.ToInt32(Console.ReadLine());
                        List<Booking>? bookings = Manager.AllBookings!.Where(b => b.Flight.FlightId == flightId).ToList();
                        if (bookings.Count == 0)
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
                    case 3:
                        Console.WriteLine("Enter passenger id");
                        int passengerId = Convert.ToInt32(Console.ReadLine());
                        List<Booking>? bookingsByPassenger = Manager.AllBookings!.Where(b => b.PassengerId == passengerId).ToList();
                        if (bookingsByPassenger.Count == 0)
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
                    case 4:
                        PassengerUtilities.CheckAvailableFlights();
                        break;
                }
            }
        }
    }
}
