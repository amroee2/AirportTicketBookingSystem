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
        IFixture fixture;
        IPassenger passenger;

        public PassengerBookingTests()
        {
            mockManager = new Mock<IManager>();
            mockFlightFilter = new Mock<IFlightFilter>();
            FlightImport flightImport = new FlightImport(new FlightValidator(new ErrorLogger()));
            flightImport.ImportFromCsvAsync().Wait();

            fixture = new Fixture().Customize(new AutoMoqCustomization { ConfigureMembers = true });
            passenger = fixture.Create<Passenger>();
            passenger.Bookings!.Clear();
            mockManager.Setup(m => m.AllBookings).Returns(new List<IBooking>());
        }

        [Fact]
        public void ShouldBookFlight()
        {
            var passengerBooking = new PassengerFlightBooker(mockManager.Object, mockFlightFilter.Object);
            ClassType randomClassType = fixture.Create<ClassType>();

            mockFlightFilter.Setup(f => f.PrintAllFlights(It.IsAny<List<IFlight>>()));

            passengerBooking.BookFlight(passenger, 1, randomClassType);

            var bookedFlight = passenger.Bookings!.First();
            Assert.Single(passenger.Bookings);
            Assert.Equal(1, bookedFlight.Flight.FlightId); // Ensure the correct flight is booked
            Assert.Equal(randomClassType, bookedFlight.ClassType); // Ensure the correct class type is booked
        }

        [Fact]
        public void ShouldNotBookFlight()
        {
            var passengerBooking = new PassengerFlightBooker(mockManager.Object, mockFlightFilter.Object);
            ClassType randomClassType = fixture.Create<ClassType>();

            mockFlightFilter.Setup(f => f.PrintAllFlights(It.IsAny<List<IFlight>>()));

            passengerBooking.BookFlight(passenger, 1, randomClassType);
            passengerBooking.BookFlight(passenger, 1, randomClassType);

            Assert.Single(passenger.Bookings!);
            Assert.Equal(1, passenger.Bookings.First().Flight.FlightId); // Verifying the correct flight is booked
        }

        [Fact]
        public void ShouldCancelBooking()
        {
            PassengerFlightManager passengerFlightManager = new PassengerFlightManager(mockManager.Object);
            Booking booking = fixture.Create<Booking>();
            passenger.Bookings!.Add(booking);

            passengerFlightManager.CancelPersonalBooking(passenger, booking.BookingId);

            Assert.Empty(passenger.Bookings!);
        }

        [Fact]
        public void ShouldNotCancelBooking()
        {
            PassengerFlightManager passengerFlightManager = new PassengerFlightManager(mockManager.Object);
            Booking booking = fixture.Create<Booking>();
            passenger.Bookings!.Add(booking);

            passengerFlightManager.CancelPersonalBooking(passenger, booking.BookingId + 1);

            Assert.Single(passenger.Bookings!); // Ensure no bookings were canceled
            Assert.Equal(booking.BookingId, passenger.Bookings.First().BookingId); // Ensure it's still the same booking
        }

        [Fact]
        public void ShouldModifyBooking()
        {
            PassengerFlightManager passengerFlightManager = new PassengerFlightManager(mockManager.Object);
            Booking booking = fixture.Create<Booking>();
            passenger.Bookings!.Add(booking);

            passengerFlightManager.ModifyPersonalBooking(passenger, booking.BookingId, ClassType.Business);

            var modifiedBooking = passenger.Bookings!.First();
            Assert.Equal(ClassType.Business, modifiedBooking.ClassType); // Ensure class type was updated
        }

        [Fact]
        public void ShouldNotModifyBooking()
        {
            PassengerFlightManager passengerFlightManager = new PassengerFlightManager(mockManager.Object);
            Booking booking = fixture.Create<Booking>();
            passenger.Bookings!.Add(booking);

            passengerFlightManager.ModifyPersonalBooking(passenger, booking.BookingId + 1, ClassType.Economy);

            var unmodifiedBooking = passenger.Bookings!.FirstOrDefault(b => b.BookingId == booking.BookingId);
            Assert.Equal(booking.ClassType, unmodifiedBooking?.ClassType); // Ensure the class type was not changed
        }
    }
}
