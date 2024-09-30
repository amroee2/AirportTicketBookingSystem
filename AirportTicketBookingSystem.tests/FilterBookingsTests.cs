using AirportTicketBookingSystem.Models;
using AirportTicketBookingSystem.Utilties;
using AirportTicketBookingSystem.Utilties.ManagerOptionsHandling.BookingHandling;
using AutoFixture;
using AutoFixture.AutoMoq;
using Moq;
using Xunit;

namespace AirportTicketBookingSystem.tests
{
    public class BookingCheckerTests
    {
        private readonly IFixture fixture;
        private readonly Mock<IManager> _manager;
        private readonly BookingChecker bookingChecker;

        public BookingCheckerTests()
        {
            fixture = new Fixture().Customize(new AutoMoqCustomization());

            _manager = fixture.Freeze<Mock<IManager>>();

            bookingChecker = new BookingChecker(_manager.Object);
        }

        [Fact]
        public void FilterByBookingId_ShouldReturnBookingById()
        {
            //Arrange
            var booking = fixture.Create<Booking>();

            _manager.Setup(m => m.AllBookings).Returns(new List<IBooking> { booking });

            //Act
            var result = bookingChecker.FilterByBookingId(booking.BookingId);

            //Assert
            Assert.Equal(booking, result.FirstOrDefault());
        }

        [Fact]
        public void FilterByFlightId_ShouldReturnBookingsByFlightId()
        {
            //Arrange
            var bookings = fixture.CreateMany<IBooking>(3).ToList();
            var flightId = bookings.First().Flight.FlightId;

            _manager.Setup(m => m.AllBookings).Returns(bookings);

            //Act
            var result = bookingChecker.FilterByFlightId(flightId);

            //Assert
            Assert.True(result.All(b => b.Flight.FlightId == flightId));
        }

        [Fact]
        public void FilterByPassengerId_ShouldReturnBookingsByPassengerId()
        {
            //Arrange
            var bookings = fixture.CreateMany<IBooking>(3).ToList();
            var passengerId = bookings.First().PassengerId;

            _manager.Setup(m => m.AllBookings).Returns(bookings);

            //Act
            var result = bookingChecker.FilterByPassengerId(passengerId);

            //Assert
            Assert.True(result.All(b => b.PassengerId == passengerId));
        }
    }
}
