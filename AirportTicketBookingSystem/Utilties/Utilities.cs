using AirportTicketBookingSystem.Airport_Repository;
using AirportTicketBookingSystem.Enums;
using AirportTicketBookingSystem.Models;

namespace AirportTicketBookingSystem.Utilties
{
    public class Utilities
    {
        public static List<IFlight> flights = new List<IFlight>();
        public static void PrintMenu()
        {
            while (true)
            {
                try
                {
                    Console.WriteLine("Welcome!\nYou Are?\n1-Manager\n2-Passenger\n3-Exit");
                    string input = Console.ReadLine();
                    if (Enum.TryParse(input, out UserType operation))
                    {
                        switch (operation)
                        {
                            default:
                                Console.WriteLine("Invalid Option");
                                PrintMenu();
                                break;
                            case UserType.Manager:
                                ManagerUtilities.PrintMenu();
                                break;
                            case UserType.Passenger:
                                PassengerUtilities.PrintMenu();
                                break;
                            case UserType.Exit:
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
        public async static Task GenerateFlightsAsync()
        {
            IFlightValidator flightValidator = new FlightValidator();
            FlightImport flightImport = new FlightImport(flightValidator);
            await flightImport.ImportFromCsvAsync();
        }
    }
}
