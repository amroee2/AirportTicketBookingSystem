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
        private readonly Manager _manager;
        private readonly BookingChecker bookingChecker;

        public BookingCheckerTests()
        {
            fixture = new Fixture().Customize(new AutoMoqCustomization());

            _manager = fixture.Create<Manager>();

            bookingChecker = new BookingChecker(_manager);
        }

        [Fact]
        public void FilterByBookingId_ShouldReturnBookingById()
        {
            //Arrange
            var booking = fixture.Create<Booking>();
            _manager.AllBookings.Add(booking);
            //Act
            var result = bookingChecker.FilterByBookingId(booking.BookingId);

            //Assert
            Assert.Equal(booking, result.FirstOrDefault());
        }

        [Fact]
        public void FilterByFlightId_ShouldReturnBookingsByFlightId()
        {
            //Arrange
            var bookings = fixture.CreateMany<Booking>(3).ToList();
            var flightId = 1;
            _manager.AllBookings.AddRange(bookings);
            //Act
            var result = bookingChecker.FilterByFlightId(flightId);

            //Assert
            Assert.True(result.All(b => b.Flight.FlightId == flightId));
            Assert.True(result.Count == bookings.Count(b => b.Flight.FlightId == flightId));
        }

        [Fact]
        public void FilterByPassengerId_ShouldReturnBookingsByPassengerId()
        {
            //Arrange
            var bookings = fixture.CreateMany<Booking>(3).ToList();
            var passengerId = 1;

            //Act
            var result = bookingChecker.FilterByPassengerId(passengerId);

            //Assert
            Assert.True(result.All(b => b.PassengerId == passengerId));
            Assert.True(result.Count == bookings.Count(b => b.PassengerId == passengerId));
        }
    }
}
