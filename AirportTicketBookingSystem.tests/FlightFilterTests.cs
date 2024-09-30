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

        private readonly Fixture fixture;

        public FlightFilterTests()
        {
            ImportFromCsvAsync().Wait();
            flightChecker = new FlightChecker();
            flightsList = new List<IFlight>();
            fixture = new Fixture();
        }

        [Fact]
        public void FilterByDepartureAirport_ShouldFilterFlightByDepartureAirport()
        {
            //Arrange
            List<string> DepartureAirports = AllFlights.Select(flight => flight.DepartureAirport).Distinct().ToList();
            string randomAirport = GetRandomItemFromList(DepartureAirports);

            //Act
            flightsList = flightChecker.FilterByDepartureAirport(AllFlights, randomAirport);

            //Assert
            Assert.True(flightsList.All(flight => flight.DepartureAirport == randomAirport));
        }

        [Fact]
        public void FilterByArrivalAirport_ShouldFilterFlightByArrivalAirport()
        {
            //Arrange
            List<string> DestinationAirports = AllFlights.Select(flight => flight.ArrivalAirport).Distinct().ToList();
            string randomAirport = GetRandomItemFromList(DestinationAirports);

            //Act
            flightsList = flightChecker.FilterByArrivalAirport(AllFlights, randomAirport);

            //Assert
            Assert.True(flightsList.All(flight => flight.ArrivalAirport == randomAirport));
        }

        [Fact]
        public void FilterByDepartureDate_ShouldFilterFlightByDepartureDate()
        {
            //Arrange
            DateTime targetDate = DateTime.Parse("9/5/2024");

            //Act
            flightsList = flightChecker.FilterByDepartureDate(AllFlights, "9/5/2024");

            //Assert
            Assert.True(flightsList.All(flight => flight.DepartureDate == targetDate));
        }

        [Fact]
        public void FilterByDepartureCountry_ShouldFilterFlightByDepartureCountry()
        {
            //Arrange
            List<string> DepartureCountries = AllFlights.Select(flight => flight.DepartureCountry).Distinct().ToList(); ;
            string randomCountry = GetRandomItemFromList(DepartureCountries);

            //Act
            flightsList = flightChecker.FilterByDepartureCountry(AllFlights, randomCountry);

            //Assert
            Assert.True(flightsList.All(flight => flight.DepartureCountry == randomCountry));
        }

        [Fact]
        public void FilterByDestinationCountry_ShouldFilterFlightByDestinationCountry()
        {
            //Arrange
            List<string> DestinationCountries = AllFlights.Select(flight => flight.DestinationCountry).Distinct().ToList();
            string randomCountry = GetRandomItemFromList(DestinationCountries);

            //Act
            flightsList = flightChecker.FilterByDestinationCountry(AllFlights, randomCountry);

            //Assert
            Assert.True(flightsList.All(flight => flight.DestinationCountry == randomCountry));
        }

        private async Task ImportFromCsvAsync()
        {
            try
            {
                string baseDirectory = AppContext.BaseDirectory;
                string filePath = Path.Combine(baseDirectory, "IOTestFiles", "Testflights.csv");

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
    }
}
