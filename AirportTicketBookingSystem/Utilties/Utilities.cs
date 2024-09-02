using AirportTicketBookingSystem.Airport_Repository;
using AirportTicketBookingSystem.Enums;
using AirportTicketBookingSystem.Models;

namespace AirportTicketBookingSystem.Utilties
{
    public class Utilities
    {
        public static List<Flight> flights = new List<Flight>();
        public static void PrintMenu()
        {
            while (true)
            {
                try
                {
                    Console.WriteLine("Welcome!\nYou Are?\n1-Manager\n2-Passenger\n3-Exit");

                    string input = Console.ReadLine();
                    if (int.TryParse(input, out int operation))
                    {
                        switch (operation)
                        {
                            default:
                                Console.WriteLine("Invalid Option");
                                PrintMenu();
                                break;
                            case (int)UserType.Manager:
                                ManagerUtilities.PrintMenu();
                                break;
                            case (int)UserType.Passenger:
                                PassengerUtilities.PrintMenu();
                                break;
                            case 3:
                                return;
                        }
                    }
                }
                catch (Exception exception)
                {
                    Console.WriteLine(exception.Message);
                }
            }
        }
        public async static Task GenerateFlights()
        {
            await FlightsRepository.ImportFromCsvAsync();
        }
    }

}
