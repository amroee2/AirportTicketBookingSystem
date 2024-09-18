using AirportTicketBookingSystem.Airport_Repository;
using AirportTicketBookingSystem.Models;
using AirportTicketBookingSystem.Utilties;
using AirportTicketBookingSystem.Utilties.ManagerOptionsHandling.ErrorHandling;
using AutoFixture;
using AutoFixture.AutoMoq;
using Moq;

namespace AirportTicketBookingSystem.tests
{
    public class FileIOTests
    {
        public FileIOTests()
        {
        }
        [Fact]
        public void ShouldValidateFlight()
        {
            Mock<IErrorLogger> logger = new Mock<IErrorLogger>();
            logger.Setup(x => x.ErrorMessages).Returns(new List<string>());
            FlightValidator flightValidator = new FlightValidator(logger.Object);
            IFixture fixture = new Fixture().Customize(new AutoMoqCustomization());
            var flight = fixture.Build<Flight>()
                                .With(f => f.FlightId, 100)
                                .With(f => f.DepartureDate, new DateTime(2025, 9, 5))
                                .Create();
            flightValidator.ValidateFlight(flight);
            Assert.Contains(GeneralUtility.flights, fligt => flight.FlightId == 100);
            GeneralUtility.flights.Remove(flight);
        }
        [Fact]
        public void ShouldNotValidateFlight()
        {
            Mock<IErrorLogger> logger = new Mock<IErrorLogger>();
            logger.Setup(x => x.ErrorMessages).Returns(new List<string>());
            FlightValidator flightValidator = new FlightValidator(logger.Object);
            IFixture fixture = new Fixture().Customize(new AutoMoqCustomization());
            var flight = fixture.Build<Flight>()
                                .With(f => f.FlightId, 100)
                                .With(f => f.DepartureDate, new DateTime(2025, 9, 5))
                                .Create();
            GeneralUtility.flights.Add(flight);
            int count = GeneralUtility.flights.Count;
            flightValidator.ValidateFlight(flight);
            Assert.Equal(count, GeneralUtility.flights.Count);
            Assert.Contains(logger.Object.ErrorMessages, message => message.Contains($"Issue with flight 100: Flight with ID {flight.FlightId} already exists."));
            GeneralUtility.flights.Remove(flight);

        }
        [Fact]
        public async Task ShouldImportAllValidFlights()
        {
            var mockErrorLogger = new Mock<IErrorLogger>();
            mockErrorLogger.Setup(x => x.ErrorMessages).Returns(new List<string>());

            var mockFlightValidator = new Mock<IFlightValidator>();
            var flightImport = new FlightImport(new FlightValidator(mockErrorLogger.Object));

            await flightImport.ImportFromCsvAsync();

            Assert.Equal(18, GeneralUtility.flights.Count);
            Assert.Equal(2, mockErrorLogger.Object.ErrorMessages.Count);
        }
    }
}