using AirportTicketBookingSystem.Airport_Repository;
using AirportTicketBookingSystem.Models;
using AirportTicketBookingSystem.Utilties;
using AirportTicketBookingSystem.Utilties.ManagerOptionsHandling.ErrorHandling;
using AutoFixture;
using Moq;

namespace AirportTicketBookingSystem.tests
{
    public class FileIOTests
    {
        public FileIOTests()
        {
            GeneralUtility.flights.Clear();
        }
        [Fact]
        public async void ShouldValidateFlights()
        {
            var mockFlightValidator = new Mock<IFlightValidator>();
            FlightImport flightImport = new FlightImport(mockFlightValidator.Object);
            await flightImport.ImportFromCsvAsync();
            mockFlightValidator.Verify(x => x.ValidateFlight(It.IsAny<IFlight>()), Times.Exactly(20));
            mockFlightValidator.Verify(x => x.PrintValidationResults(), Times.Once);
        }
        [Fact]
        public async void ShouldImportAllValidFlights()
        {
            var mockErrorLogger = new Mock<IErrorLogger>();
            mockErrorLogger.Setup(x => x.ErrorMessages).Returns(new List<string>());
            FlightValidator flightValidator = new FlightValidator(mockErrorLogger.Object);
            FlightImport flightImport = new FlightImport(flightValidator);

            await flightImport.ImportFromCsvAsync();
            Assert.Equal(18, GeneralUtility.flights.Count);
            Assert.Equal(2, mockErrorLogger.Object.ErrorMessages.Count);
        }
    }
}