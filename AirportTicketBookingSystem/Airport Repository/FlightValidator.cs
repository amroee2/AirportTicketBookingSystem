using AirportTicketBookingSystem.Models;
using AirportTicketBookingSystem.Utilties;
using System.ComponentModel.DataAnnotations;

namespace AirportTicketBookingSystem.Airport_Repository
{
    public class FlightValidator
    {
        public static void ValidateFlight(Flight flight)
        {
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

        public static void PrintValidationResults()
        {
            if (!Manager.errorMessages.Any())
            {
                Console.WriteLine("Flights imported successfully from flights.csv");
            }
            else
            {
                Console.WriteLine("The following errors were found:");
                foreach (var errorMessage in Manager.errorMessages)
                {
                    Console.WriteLine(errorMessage);
                }
            }
        }
    }
}