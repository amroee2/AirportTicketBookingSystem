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
        Mock<IErrorLogger> logger = new Mock<IErrorLogger>();
        public FileIOTests()
        {
            logger.Setup(x => x.ErrorMessages).Returns(new List<string>());
        }
        [Fact]
        public void ShouldValidateFlight()
        {
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
            var mockFlightValidator = new Mock<IFlightValidator>();

            mockFlightValidator.Setup(v => v.ValidateFlight(It.IsAny<IFlight>()))
                               .Callback<IFlight>(flight =>
                               {
                                   if (GeneralUtility.flights.Any(f => f.FlightId == flight.FlightId)
                                   || flight.DepartureDate < DateTime.Now)
                                   {
                                       logger.Object.ErrorMessages.Add($"Issue with flight {flight.FlightId}: Invalid data");
                                   }
                                   else
                                   {
                                       GeneralUtility.flights.Add(flight);
                                   }
                               });
            mockFlightValidator.Setup(v => v.PrintValidationResults());
            var flightImport = new FlightImport(mockFlightValidator.Object);
            await flightImport.ImportFromCsvAsync();
            Assert.Equal(18, GeneralUtility.flights.Count);
            Assert.Equal(2, logger.Object.ErrorMessages.Count);
            mockFlightValidator.Verify(v => v.ValidateFlight(It.IsAny<IFlight>()), Times.Exactly(20)); // Assuming 18 flights
            mockFlightValidator.Verify(v => v.PrintValidationResults(), Times.Once);

        }

    }
}