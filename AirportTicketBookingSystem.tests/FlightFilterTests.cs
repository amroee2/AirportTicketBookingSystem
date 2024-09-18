using System.Linq;
using AutoFixture;
using AirportTicketBookingSystem.Airport_Repository;
using AirportTicketBookingSystem.Models;
using AirportTicketBookingSystem.Utilties.ManagerOptionsHandling.ErrorHandling;
using AirportTicketBookingSystem.Utilties.PassengerOptionsHandling.FlightsHandling;
using AirportTicketBookingSystem.Utilties;

namespace AirportTicketBookingSystem.tests
{
    public class FlightFilterTests
    {
        List<IFlight> flightsList;
        FlightChecker flightChecker;
        private readonly List<string> DepartureCountries;
        private readonly List<string> DepartureAirports;
        private readonly List<string> DestinationCountries;
        private readonly List<string> DestinationAirports;

        private readonly Fixture fixture;

        public FlightFilterTests()
        {
            FlightImport flightImport = new FlightImport(new FlightValidator(new ErrorLogger()));
            flightImport.ImportFromCsvAsync().Wait();
            flightChecker = new FlightChecker();
            flightsList = new List<IFlight>();
            fixture = new Fixture();

            DepartureCountries = GeneralUtility.flights.Select(flight => flight.DepartureCountry).Distinct().ToList();
            DepartureAirports = GeneralUtility.flights.Select(flight => flight.DepartureAirport).Distinct().ToList();
            DestinationCountries = GeneralUtility.flights.Select(flight => flight.DestinationCountry).Distinct().ToList();
            DestinationAirports = GeneralUtility.flights.Select(flight => flight.ArrivalAirport).Distinct().ToList();
        }

        private string GetRandomItemFromList(List<string> list)
        {
            return fixture.CreateMany<string>(1).FirstOrDefault(list.Contains) ?? list.First();
        }

        [Fact]
        public void ShouldFilterFlightByDepartureAirport()
        {
            string randomAirport = GetRandomItemFromList(DepartureAirports);
            flightsList = flightChecker.FilterByDepartureAirport(GeneralUtility.flights, randomAirport);
            Assert.True(flightsList.All(flight => flight.DepartureAirport == randomAirport));
        }

        [Fact]
        public void ShouldFilterFlightByArrivalAirport()
        {
            string randomAirport = GetRandomItemFromList(DestinationAirports);
            flightsList = flightChecker.FilterByArrivalAirport(GeneralUtility.flights, randomAirport);
            Assert.True(flightsList.All(flight => flight.ArrivalAirport == randomAirport));
        }

        [Fact]
        public void ShouldFilterFlightByDepartureDate()
        {
            DateTime targetDate = DateTime.Parse("9/5/2024");
            flightsList = flightChecker.FilterByDepartureDate(GeneralUtility.flights, "9/5/2024");
            Assert.True(flightsList.All(flight => flight.DepartureDate == targetDate));
        }

        [Fact]
        public void ShouldFilterFlightByDepartureCountry()
        {
            string randomCountry = GetRandomItemFromList(DepartureCountries);
            flightsList = flightChecker.FilterByDepartureCountry(GeneralUtility.flights, randomCountry);
            Assert.True(flightsList.All(flight => flight.DepartureCountry == randomCountry));
        }

        [Fact]
        public void ShouldFilterFlightByDestinationCountry()
        {
            string randomCountry = GetRandomItemFromList(DestinationCountries);
            flightsList = flightChecker.FilterByDestinationCountry(GeneralUtility.flights, randomCountry);
            Assert.True(flightsList.All(flight => flight.DestinationCountry == randomCountry));
        }
    }
}
