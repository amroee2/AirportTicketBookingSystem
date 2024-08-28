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
            flights = GenerateFlights(20);
            Console.Clear();
            Console.WriteLine("Welcome!\nYou Are?\n1-Manager\n2-Passenger");

            int op = Convert.ToInt32(Console.ReadLine());

            switch (op)
            {
                case 0: Console.WriteLine("Invalid Option"); PrintMenu();
                    break;
                case 2:
                    PassengerUtilities.PrintMenu();
                    break;
            }
        }
        static List<Flight> GenerateFlights(int numberOfFlights)
        {
            List<Flight> flights = new List<Flight>();
            Random random = new Random();
            string[] countries = { "USA", "UK", "Germany", "France", "Japan" };
            string[] airports = { "JFK", "LHR", "FRA", "CDG", "NRT" };
            Array classTypes = Enum.GetValues(typeof(ClassType));

            for (int i = 0; i < numberOfFlights; i++)
            {
                int flightId = i + 1;
                string departureDate = DateTime.Now.AddDays(random.Next(1, 365)).ToString("yyyy-MM-dd");
                string departureCountry = countries[random.Next(countries.Length)];
                string destinationCountry = countries[random.Next(countries.Length)];
                string departureAirport = airports[random.Next(airports.Length)];
                string arrivalAirport = airports[random.Next(airports.Length)];
                ClassType classType = (ClassType)classTypes.GetValue(random.Next(classTypes.Length));

                Flight flight = new Flight(flightId, departureDate, departureCountry, destinationCountry, departureAirport, arrivalAirport, classType);
                flights.Add(flight);
            }

            return flights;
        }
    }

}
