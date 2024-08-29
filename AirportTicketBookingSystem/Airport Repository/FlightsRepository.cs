using AirportTicketBookingSystem.Models;
using AirportTicketBookingSystem.Utilties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirportTicketBookingSystem.Airport_Repository
{
    public class FlightsRepository
    {
        public async static Task ExportToCsvAsync()
        {
            try
            {
                StringBuilder csvContent = new StringBuilder();
                csvContent.AppendLine("FlightId,DepartureDate,DeparetureCountry,DestinationCountry,DepartureAirport,ArrivalAirport");

                foreach (var flight in Utilities.flights)
                {
                    csvContent.AppendLine($"{flight.FlightId},{flight.DepartureDate},{flight.DepartureCountry},{flight.DestinationCountry},{flight.DepartureAirport},{flight.ArrivalAirport}");
                }
                string filePath = Path.Combine("C:\\Users\\amro qadadha\\source\\repos\\AirportTicketBookingSystem\\AirportTicketBookingSystem\\Airport Repository\\", "flights.csv");
                await File.WriteAllTextAsync(filePath, csvContent.ToString());
                Console.WriteLine("Flights exported to flights.csv");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
        public async static Task<List<Flight>> ImportFromCsvAsync()
        {
            List<Flight> flights = new List<Flight>();
            try
            {
                string filePath = Path.Combine("C:\\Users\\amro qadadha\\source\\repos\\AirportTicketBookingSystem\\AirportTicketBookingSystem\\Airport Repository\\", "flights.csv");

                 string[] lines = await File.ReadAllLinesAsync(filePath);

                for (int i = 1; i < lines.Length; i++)
                {
                    string line = lines[i];
                    string[] values = line.Split(',');

                    if (values.Length == 6)
                    {
                         Flight flight = new Flight(Convert.ToInt32(values[0]), values[1], values[2], values[3], values[4], values[5]);

                        flights.Add(flight);
                    }
                    else
                    {
                        Console.WriteLine($"Invalid line format: {line}");
                    }
                }

                Console.WriteLine("Flights imported from flights.csv");
            }
            catch (Exception e)
            {
                Console.WriteLine($"An error occurred while reading the file: {e.Message}");
            }

            return flights;
        }
    }
}
