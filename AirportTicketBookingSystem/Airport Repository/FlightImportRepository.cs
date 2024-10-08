﻿using AirportTicketBookingSystem.Models;
using CsvHelper;
using CsvHelper.Configuration;
using System.Globalization;

namespace AirportTicketBookingSystem.Airport_Repository
{
    public class FlightImportRepository : IFlightImportRepository
    {
        private readonly IFlightValidator _flightValidator;

        public FlightImportRepository(IFlightValidator flightValidator)
        {
            _flightValidator = flightValidator;
        }

        public async Task ImportFromCsvAsync(string directory, string file)
        {
            try
            {
                string filePath = Path.Combine(AppContext.BaseDirectory, directory, file);

                var config = new CsvConfiguration(CultureInfo.InvariantCulture);
                using (var reader = new StreamReader(filePath))
                using (var csv = new CsvReader(reader, config))
                {
                    while (await csv.ReadAsync())
                    {
                        var flight = csv.GetRecord<Flight>();
                        _flightValidator.ValidateFlight(flight);
                    }
                    _flightValidator.PrintValidationResults();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"An error occurred while reading the file: {e.Message}");
            }
        }
    }

}