using AirportTicketBookingSystem.Enums;
using AirportTicketBookingSystem.Models;

namespace AirportTicketBookingSystem.Utilties.PassengerOptionsHandling.FlightsHandling
{
    public class FlightChecker : IFlightFilter
    {
        public void CheckAvailableFlights(List<Flight> FlightsList)
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

        private void CheckFilterType(List<Flight> FlightsList, FlightFilter operation)
        {
            List<Flight> filteredFlights = new List<Flight>();
            switch (operation)
            {
                case FlightFilter.Exit:
                    Console.WriteLine("Exiting"); return;
                case FlightFilter.ByDepartureCountry:
                    Console.WriteLine("Enter the departure country");
                    string departureCountry = Console.ReadLine();
                    filteredFlights= FilterByDepartureCountry(FlightsList, departureCountry);
                    break;
                case FlightFilter.ByDestinationCountry:
                    Console.WriteLine("Enter the destination country");
                    string destinationCountry = Console.ReadLine();
                    filteredFlights= FilterByDestinationCountry(FlightsList, destinationCountry);
                    break;
                case FlightFilter.ByDepartureDate:
                    Console.WriteLine("Enter the departure date");
                    string departureDate = Console.ReadLine();
                    filteredFlights = FilterByDepartureDate(FlightsList, departureDate);
                    break;
                case FlightFilter.ByDepartureAirport:
                    Console.WriteLine("Enter the departure airport");
                    string departureAirport = Console.ReadLine();
                    filteredFlights = FilterByDepartureAirport(FlightsList, departureAirport);
                    break;
                case FlightFilter.ByArrivalAirport:
                    Console.WriteLine("Enter the arrival airport");
                    string arrivalAirport = Console.ReadLine();
                    filteredFlights = FilterByArrivalAirport(FlightsList, arrivalAirport);
                    break;
                case FlightFilter.NoFilter:
                    PrintAllFlights(FlightsList);
                    break;
                default:
                    Console.WriteLine("Invalid Option");
                    break;
            }
            PrintAllFlights(filteredFlights);
        }

        public void PrintAllFlights(List<Flight> FlightsList)
        {
            foreach (var flight in FlightsList)
            {
                Console.WriteLine(flight);
            }
        }

        public List<Flight> FilterByArrivalAirport(List<Flight> FlightsList, string arrivalAirport)
        {
            var flights = FlightsList.Where(f => f.ArrivalAirport == arrivalAirport).ToList();
            return flights;
        }

        public List<Flight> FilterByDepartureAirport(List<Flight> FlightsList, string departureAirport)
        {
            var flights = FlightsList.Where(f => f.DepartureAirport == departureAirport).ToList();
            return flights;
        }

        public List<Flight> FilterByDepartureDate(List<Flight> FlightsList, string departureDate)
        {
            DateTime date = DateTime.Parse(departureDate);
            var flights = FlightsList.Where(f => f.DepartureDate == date).ToList();
            return flights;
        }

        public List<Flight> FilterByDestinationCountry(List<Flight> FlightsList, string destinationCountry)
        {
            var flights = FlightsList.Where(f => f.DestinationCountry == destinationCountry).ToList();
            return flights;
        }

        public List<Flight> FilterByDepartureCountry(List<Flight> FlightsList, string departureCountry)
        {
            var flights = FlightsList.Where(f => f.DepartureCountry == departureCountry).ToList();
            return flights;
        }
    }
}
