using AirportTicketBookingSystem.Models;
using AirportTicketBookingSystem.Utilties;
using System.ComponentModel.DataAnnotations;

namespace AirportTicketBookingSystem.Airport_Repository
{
    public class FlightValidator : IFlightValidator
    {
        public IErrorLogger _errorLogger { get; set; }

        public FlightValidator(IErrorLogger errorLogger)
        {
            _errorLogger = errorLogger;
        }
        public void ValidateFlight(IFlight flight)
        {
            var context = new ValidationContext(flight, serviceProvider: null, items: null);
            var results = new List<ValidationResult>();

            bool isValid = Validator.TryValidateObject(flight, context, results, true);

            if (isValid)
            {
                GeneralUtility.flights.Add(flight);
            }
            else
            {
                foreach (var validationResult in results)
                {
                    _errorLogger.ErrorMessages.Add($"Issue with flight {flight.FlightId}: {validationResult.ErrorMessage}");
                }
            }
        }

        public void PrintValidationResults()
        {
            if (!_errorLogger.ErrorMessages.Any())
            {
                Console.WriteLine("Flights imported successfully from flights.csv");
            }
            else
            {
                Console.WriteLine("The following errors were found:");
                foreach (var errorMessage in _errorLogger.ErrorMessages)
                {
                    Console.WriteLine(errorMessage);
                }
            }
        }
    }
}