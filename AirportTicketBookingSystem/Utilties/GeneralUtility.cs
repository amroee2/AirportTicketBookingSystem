using AirportTicketBookingSystem.Enums;
using AirportTicketBookingSystem.Models;
using AirportTicketBookingSystem.Utilties.PassengerOptionsHandling;

namespace AirportTicketBookingSystem.Utilties
{
    public class GeneralUtility
    {
        public static List<Flight> flights = new List<Flight>();

        private readonly ManagerUtilities _managerUtilities;
        private readonly PassengerUtilities _passengerUtilities;
        public GeneralUtility(ManagerUtilities managerUtilities, PassengerUtilities passengerUtilities)
        {
            _managerUtilities = managerUtilities;
            _passengerUtilities = passengerUtilities;
        }

        public void PrintMenu()
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
                            case UserType.Manager:
                                _managerUtilities.PrintMenu();
                                break;
                            case UserType.Passenger:
                                _passengerUtilities.PrintMenu();
                                break;
                            case UserType.Exit:
                                return;
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
    }
}
