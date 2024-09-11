using AirportTicketBookingSystem.Utilties;
using CsvHelper;
using CsvHelper.Configuration;
using System.Globalization;

namespace AirportTicketBookingSystem.Airport_Repository
{
    public class FlightExport
    {
        public async static Task ExportToCsvAsync()
        {
            try
            {
                await ExportingToCsv();
            }
            catch (Exception e)
            {
                Console.WriteLine($"An error occurred while writing to the file: {e.Message}");
            }
        }

        private static async Task ExportingToCsv()
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
    }
}