using AirportTicketBookingSystem.Airport_Repository;
using AirportTicketBookingSystem.Enums;
using AirportTicketBookingSystem.Models;

namespace AirportTicketBookingSystem.Utilties
{
    public class GeneralUtility
    {
        public static List<IFlight> flights = new List<IFlight>();

        public IFlightImport _flightImport { get; set; }
        public GeneralUtility(IFlightImport flightImport)
        {
            _flightImport = flightImport;
        }
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
                                var errorLogger = new ErrorLogger();
                                FileManager fileManager = new FileManager(new FlightImport(new FlightValidator(errorLogger)), new FlightExport());
                                BookingManager bookingManager = new BookingManager(new BookingChecker(new Manager()), errorLogger);
                                ManagerUtilities managerUtilities = new ManagerUtilities(fileManager, bookingManager);
                                managerUtilities.PrintMenu();
                                break;
                            case UserType.Passenger:
                                PassengerUtilities passengerUtilities = new PassengerUtilities();
                                passengerUtilities.PrintMenu();
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
    }
}
