using AirportTicketBookingSystem.Models;
using AirportTicketBookingSystem.Utilties;
using CsvHelper;
using CsvHelper.Configuration;
using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace AirportTicketBookingSystem.Airport_Repository
{
    public class FlightImport
    {
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
                        FlightValidator.ValidateFlight(flight);
                    }
                    FlightValidator.PrintValidationResults();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"An error occurred while reading the file: {e.Message}");
            }

        }
    }
}