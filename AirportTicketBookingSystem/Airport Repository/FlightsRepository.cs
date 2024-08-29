using AirportTicketBookingSystem.Models;
using AirportTicketBookingSystem.Utilties;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
                string filePath = Path.Combine("./flights.csv");
                await File.WriteAllTextAsync(filePath, csvContent.ToString());
                Console.WriteLine("Flights exported to flights.csv");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
        public async static Task ImportFromCsvAsync()
        {
            try
            {
                string filePath = Path.Combine("./flights.csv");

                string[] lines = await File.ReadAllLinesAsync(filePath);

                for (int i = 1; i < lines.Length; i++)
                {
                    string line = lines[i];
                    string[] values = line.Split(',');

                    if (values.Length == 6)
                    {
                        Flight flight = new Flight(Convert.ToInt32(values[0]), DateTime.Parse(values[1]), values[2], values[3], values[4], values[5]);

                        // Validate the flight object
                        var context = new ValidationContext(flight, serviceProvider: null, items: null);
                        var results = new List<ValidationResult>();

                        bool isValid = Validator.TryValidateObject(flight, context, results, true);

                        if (isValid)
                        {
                            Utilities.flights.Add(flight);
                        }
                        else
                        {
                            foreach (var validationResult in results)
                            {
                                Manager.errorMessages.Add($"Line {i + 1}: {validationResult.ErrorMessage}");
                            }
                        }
                    }
                    else
                    {
                        Manager.errorMessages.Add($"Line {i + 1}: Invalid line format: {line}");
                    }
                }

                if (Manager.errorMessages.Any())
                {
                    Console.WriteLine("The following errors were found:");
                    foreach (var errorMessage in Manager.errorMessages)
                    {
                        Console.WriteLine(errorMessage);
                    }
                }
                else
                {
                    Console.WriteLine("Flights imported successfully from flights.csv");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"An error occurred while reading the file: {e.Message}");
            }
        }
    }
}
