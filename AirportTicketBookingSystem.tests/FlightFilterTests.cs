using AirportTicketBookingSystem.Models;
using AirportTicketBookingSystem.Utilties.PassengerOptionsHandling.FlightsHandling;
using AutoFixture;
using CsvHelper;
using CsvHelper.Configuration;
using System.Globalization;

namespace AirportTicketBookingSystem.tests
{
    public class FlightFilterTests
    {
        List<IFlight> flightsList;
        List<IFlight> AllFlights = new List<IFlight>();
        FlightChecker flightChecker;
        private readonly List<string> DepartureCountries;
        private readonly List<string> DepartureAirports;
        private readonly List<string> DestinationCountries;
        private readonly List<string> DestinationAirports;

        private readonly Fixture fixture;

        public FlightFilterTests()
        {
            ImportFromCsvAsync().Wait();
            flightChecker = new FlightChecker();
            flightsList = new List<IFlight>();
            fixture = new Fixture();
            DepartureCountries = AllFlights.Select(flight => flight.DepartureCountry).Distinct().ToList();
            DepartureAirports = AllFlights.Select(flight => flight.DepartureAirport).Distinct().ToList();
            DestinationCountries = AllFlights.Select(flight => flight.DestinationCountry).Distinct().ToList();
            DestinationAirports = AllFlights.Select(flight => flight.ArrivalAirport).Distinct().ToList();
        }

        private async Task ImportFromCsvAsync()
        {
            try
            {
                string baseDirectory = AppContext.BaseDirectory;
                string filePath = Path.Combine(baseDirectory, "Airport Repository", "flights.csv");

                var config = new CsvConfiguration(CultureInfo.InvariantCulture);
                using (var reader = new StreamReader(filePath))
                using (var csv = new CsvReader(reader, config))
                {
                    while (await csv.ReadAsync())
                    {
                        var flight = csv.GetRecord<Flight>();
                        AllFlights.Add(flight);
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"An error occurred while reading the file: {e.Message}");
            }
        }


        private string GetRandomItemFromList(List<string> list)
        {
            return fixture.CreateMany<string>(1).FirstOrDefault(list.Contains) ?? list.First();
        }

        [Fact]
        public void ShouldFilterFlightByDepartureAirport()
        {
            string randomAirport = GetRandomItemFromList(DepartureAirports);
            flightsList = flightChecker.FilterByDepartureAirport(AllFlights, randomAirport);
            Assert.True(flightsList.All(flight => flight.DepartureAirport == randomAirport));
        }

        [Fact]
        public void ShouldFilterFlightByArrivalAirport()
        {
            string randomAirport = GetRandomItemFromList(DestinationAirports);
            flightsList = flightChecker.FilterByArrivalAirport(AllFlights, randomAirport);
            Assert.True(flightsList.All(flight => flight.ArrivalAirport == randomAirport));
        }

        [Fact]
        public void ShouldFilterFlightByDepartureDate()
        {
            DateTime targetDate = DateTime.Parse("9/5/2024");
            flightsList = flightChecker.FilterByDepartureDate(AllFlights, "9/5/2024");
            Assert.True(flightsList.All(flight => flight.DepartureDate == targetDate));
        }

        [Fact]
        public void ShouldFilterFlightByDepartureCountry()
        {
            string randomCountry = GetRandomItemFromList(DepartureCountries);
            flightsList = flightChecker.FilterByDepartureCountry(AllFlights, randomCountry);
            Assert.True(flightsList.All(flight => flight.DepartureCountry == randomCountry));
        }

        [Fact]
        public void ShouldFilterFlightByDestinationCountry()
        {
            string randomCountry = GetRandomItemFromList(DestinationCountries);
            flightsList = flightChecker.FilterByDestinationCountry(AllFlights, randomCountry);
            Assert.True(flightsList.All(flight => flight.DestinationCountry == randomCountry));
        }
    }
}
