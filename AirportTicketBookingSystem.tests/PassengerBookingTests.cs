using AirportTicketBookingSystem.Airport_Repository;
using AirportTicketBookingSystem.Models;
using AirportTicketBookingSystem.Utilties.ManagerOptionsHandling.ErrorHandling;
using AirportTicketBookingSystem.Utilties.PassengerOptionsHandling.BookingHandling;
using AirportTicketBookingSystem.Utilties.PassengerOptionsHandling.FlightsHandling;
using AutoFixture;
using AutoFixture.AutoMoq;
using Moq;

namespace AirportTicketBookingSystem.tests
{
    public class PassengerBookingTests
    {
        Mock<IManager> mockManager;
        Mock<IFlightFilter> mockFlightFilter;

        public PassengerBookingTests()
        {
            mockManager = new Mock<IManager>();
            mockFlightFilter = new Mock<IFlightFilter>();
            FlightImport flightImport = new FlightImport(new FlightValidator(new ErrorLogger()));
            flightImport.ImportFromCsvAsync().Wait();
        }

        [Fact]
        public void ShouldBookFlight()
        {
            IFixture fixture = new Fixture().Customize(new AutoMoqCustomization { ConfigureMembers = true });

            IPassenger passenger = fixture.Create<Passenger>();

            passenger.Bookings!.Clear();
            mockManager.Setup(m=> m.AllBookings).Returns(new List<IBooking>());
            var passengerBooking = new PassengerFlightBooker(mockManager.Object, mockFlightFilter.Object);
            ClassType randomClassType = fixture.Create<ClassType>();

            mockFlightFilter.Setup(f => f.PrintAllFlights(It.IsAny<List<IFlight>>()));

            passengerBooking.BookFlight(passenger, 1, randomClassType);

            Assert.Single(passenger.Bookings!);
        }
        [Fact]
        public void ShouldNotBookFlight()
        {
            IFixture fixture = new Fixture().Customize(new AutoMoqCustomization { ConfigureMembers = true });

            IPassenger passenger = fixture.Create<Passenger>();

            passenger.Bookings!.Clear();
            mockManager.Setup(m => m.AllBookings).Returns(new List<IBooking>());
            var passengerBooking = new PassengerFlightBooker(mockManager.Object, mockFlightFilter.Object);
            ClassType randomClassType = fixture.Create<ClassType>();

            mockFlightFilter.Setup(f => f.PrintAllFlights(It.IsAny<List<IFlight>>()));

            passengerBooking.BookFlight(passenger, 1, randomClassType);
            passengerBooking.BookFlight(passenger, 1, randomClassType);

            Assert.Single(passenger.Bookings!);
        }
    }
}
