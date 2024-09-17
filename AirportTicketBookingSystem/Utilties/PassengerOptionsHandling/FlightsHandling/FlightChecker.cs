using AirportTicketBookingSystem.Enums;
using AirportTicketBookingSystem.Models;

namespace AirportTicketBookingSystem.Utilties.PassengerOptionsHandling.FlightsHandling
{
    public class FlightChecker : IFlightFilter
    {
        public void CheckAvailableFlights(List<IFlight> FlightsList)
        {
            Console.WriteLine("Search by\n1-Departure country\n2-Destination country\n3-Departure date" +
                "\n4-Departure airport\n5-Arrival airport\n6-All\n0-Exit");
            try
            {
                if (Enum.TryParse(Console.ReadLine(), out FlightFilter operation))
                    CheckFilterType(FlightsList, operation);
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.Message);
            }
        }

        private void CheckFilterType(List<IFlight> FlightsList, FlightFilter operation)
        {
            switch (operation)
            {
                case FlightFilter.Exit:
                    Console.WriteLine("Exiting"); return;
                case FlightFilter.ByDepartureCountry:
                    FilterByDepartureCountry(FlightsList);
                    break;
                case FlightFilter.ByDestinationCountry:
                    FilterByDestinationCountry(FlightsList);
                    break;
                case FlightFilter.ByDepartureDate:
                    FilterByDepartureDate(FlightsList);
                    break;
                case FlightFilter.ByDepartureAirport:
                    FilterByDepartureAirport(FlightsList);
                    break;
                case FlightFilter.ByArrivalAirport:
                    FilterByArrivalAirport(FlightsList);
                    break;
                case FlightFilter.NoFilter:
                    PrintAllFlights(FlightsList);
                    break;
                default:
                    Console.WriteLine("Invalid Option");
                    break;
            }
        }

        public void PrintAllFlights(List<IFlight> FlightsList)
        {
            foreach (var flight in FlightsList)
            {
                Console.WriteLine(flight);
            }
        }

        private void FilterByArrivalAirport(List<IFlight> FlightsList)
        {
            List<IFlight> flights;
            Console.WriteLine("Enter the arrival airport");
            string arrivalAirport = Console.ReadLine();
            flights = FlightsList.Where(f => f.ArrivalAirport == arrivalAirport).ToList();
            foreach (var flight in flights)
            {
                Console.WriteLine(flight);
            }
        }

        private void FilterByDepartureAirport(List<IFlight> FlightsList)
        {
            List<IFlight> flights;
            Console.WriteLine("Enter the departure airport");
            string departureAirport = Console.ReadLine();
            flights = FlightsList.Where(f => f.DepartureAirport == departureAirport).ToList();
            foreach (var flight in flights)
            {
                Console.WriteLine(flight);
            }
        }

        private void FilterByDepartureDate(List<IFlight> FlightsList)
        {
            List<IFlight> flights;
            Console.WriteLine("Enter the departure date");
            string departureDate = Console.ReadLine();
            DateTime date = DateTime.Parse(departureDate);
            flights = FlightsList.Where(f => f.DepartureDate == date).ToList();
            foreach (var flight in flights)
            {
                Console.WriteLine(flight);
            }
        }

        private void FilterByDestinationCountry(List<IFlight> FlightsList)
        {
            List<IFlight> flights;
            Console.WriteLine("Enter the destination country");
            string destinationCountry = Console.ReadLine();
            flights = FlightsList.Where(f => f.DestinationCountry == destinationCountry).ToList();
            foreach (var flight in flights)
            {
                Console.WriteLine(flight);
            }
        }

        private void FilterByDepartureCountry(List<IFlight> FlightsList)
        {
            Console.WriteLine("Enter the departure country");
            string departureCountry = Console.ReadLine();
            var flights = FlightsList.Where(f => f.DepartureCountry == departureCountry).ToList();
            foreach (var flight in flights)
            {
                Console.WriteLine(flight);
            }
        }
    }
}
