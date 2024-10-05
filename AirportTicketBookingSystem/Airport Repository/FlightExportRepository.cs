using AirportTicketBookingSystem.Utilties;
using CsvHelper;
using CsvHelper.Configuration;
using System.Globalization;

namespace AirportTicketBookingSystem.Airport_Repository
{
    public class FlightExportRepository : IFlightExportRepository
    {
        public async Task ExportToCSVAsync()
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
                    await csv.WriteRecordsAsync(GeneralUtility.flights);
                }
                Console.WriteLine("Flights exported to flights.csv");
            }
            catch (Exception e)
            {
                Console.WriteLine($"An error occurred while writing the file: {e.Message}");
            }
        }
    }
}