using System;
using System.Collections.Generic;
using AirportTicketBookingSystem.Models;

namespace AirportTicketBookingSystem
{
    class Program
    {
        static void Main(string[] args)
        {
            List<Flight> flights = GenerateFlights(20);
            foreach (var flight in flights)
            {
                Console.WriteLine($"Flight ID: {flight.FlightId}, Departure: {flight.DepartureDate} from {flight.DeparetureCountry} ({flight.DepartureAirport}) to {flight.DestinationCountry} ({flight.ArrivalAirport}), Class: {flight.ClassType}, Price: {flight.Price}");
            }
            Utilties.Utilities.PrintMenu();
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
