using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace AirportTicketBookingSystem.Utilties.PassengerOptionsHandling.FlightsHandling
{
    public class FlightConstraints : IFlightConstraints
    {
        public void CheckFlightConstraints()
        {
            var assembly = Assembly.GetExecutingAssembly();
            var type = assembly.GetType("AirportTicketBookingSystem.Models.Flight");
            var properties = type.GetProperties();
            foreach (var property in properties)
            {
                Console.WriteLine($"Property: {property.Name}");
                Console.WriteLine($"- Type: {property.PropertyType.Name}");
                var attributes = property.GetCustomAttributes();
                PrintConstraints(attributes);
            }
        }

        private void PrintConstraints(IEnumerable<Attribute> attributes)
        {
            foreach (var attribute in attributes)
            {
                Console.WriteLine($"- Constraint: {attribute.GetType().Name}");
                var errorMessageProperty = attribute.GetType().GetProperty("ErrorMessage");
                if (errorMessageProperty != null)
                {
                    var errorMessage = errorMessageProperty.GetValue(attribute) as string;
                    if (!string.IsNullOrEmpty(errorMessage))
                    {
                        Console.WriteLine($"  - Error Message: {errorMessage}");
                    }
                }
            }
        }

    }
}
