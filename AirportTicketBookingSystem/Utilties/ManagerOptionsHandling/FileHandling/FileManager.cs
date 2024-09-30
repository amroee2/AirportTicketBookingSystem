﻿using AirportTicketBookingSystem.Airport_Repository;

namespace AirportTicketBookingSystem.Utilties.ManagerOptionsHandling.FileHandling
{
    public class FileManager : IFileManager
    {
        private readonly IFlightImportRepository _flightImport;
        private readonly IFlightExportRepository _flightExport;

        public FileManager(IFlightImportRepository flightImport, IFlightExportRepository flightExport)
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
