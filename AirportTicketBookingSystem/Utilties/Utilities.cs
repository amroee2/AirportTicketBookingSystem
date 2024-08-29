using AirportTicketBookingSystem.Airport_Repository;
using AirportTicketBookingSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirportTicketBookingSystem.Utilties
{
    public class Utilities
    {
        public static List<Flight> flights = new List<Flight>();
        public static void PrintMenu()
        {
            _ = GenerateFlights();
            Console.Clear();

            while (true)
            {
                Console.Clear();
                Console.WriteLine("Welcome!\nYou Are?\n1-Manager\n2-Passenger\n3-Exit");

                int op = Convert.ToInt32(Console.ReadLine());
                switch (op)
                {
                    default:
                        Console.WriteLine("Invalid Option"); PrintMenu();
                        break;
                    case 1:
                        ManagerUtilities.PrintMenu();
                        break;
                    case 2:
                        PassengerUtilities.PrintMenu();
                        break;
                    case 3:
                        return;
                }
            }
        }
        public async static Task GenerateFlights()
        {
            await Airport_Repository.FlightsRepository.ImportFromCsvAsync();
        }
    }

}
