using AirportTicketBookingSystem.Airport_Repository;

namespace AirportTicketBookingSystem.Utilties
{
    public class FileManager
    {
        private readonly IFlightImport _flightImport;
        private readonly IFlightExport _flightExport;

        public FileManager(IFlightImport flightImport, IFlightExport flightExport)
        {
            _flightImport = flightImport;
            _flightExport = flightExport;
        }

        public async Task ImportFromCsvAsync()
        {
            await _flightImport.ImportFromCsvAsync();
        }

        public async Task ExportToCsvAsync()
        {
            await _flightExport.ExportToCSVAsync();
        }
    }

}
