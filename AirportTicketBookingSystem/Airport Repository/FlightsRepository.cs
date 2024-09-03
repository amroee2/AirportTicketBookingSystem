using AirportTicketBookingSystem.Models;
using AirportTicketBookingSystem.Utilties;
using CsvHelper;
using CsvHelper.Configuration;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
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
                string baseDirectory = AppContext.BaseDirectory;
                string filePath = Path.Combine(baseDirectory, "Airport Repository", "flights.csv");
                Directory.CreateDirectory(Path.GetDirectoryName(filePath));
                var config = new CsvConfiguration(CultureInfo.InvariantCulture);

                using (var writer = new StreamWriter(filePath))
                using (var csv = new CsvWriter(writer, config))
                {
                    await csv.WriteRecordsAsync(Utilities.flights);
                }

                Console.WriteLine("Flights exported to flights.csv");
            }
            catch (Exception e)
            {
                Console.WriteLine($"An error occurred while writing to the file: {e.Message}");
            }
        }

        public async static Task ImportFromCsvAsync()
        {
            try
            {
                string baseDirectory = AppContext.BaseDirectory;

                string filePath = Path.Combine(baseDirectory, "Airport Repository", "flights.csv");

                var config = new CsvConfiguration(CultureInfo.InvariantCulture);
                using (var reader = new StreamReader(filePath))
                using (var csv = new CsvReader(reader, config))
                {
                    while (await csv.ReadAsync())
                    {
                        var flight = csv.GetRecord<Flight>();

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
                                Manager.errorMessages.Add($"Issue with flight {flight.FlightId}: {validationResult.ErrorMessage}");
                            }
                        }
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