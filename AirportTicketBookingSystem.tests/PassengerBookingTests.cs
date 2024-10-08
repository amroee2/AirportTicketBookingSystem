﻿using AirportTicketBookingSystem.Airport_Repository;
using AirportTicketBookingSystem.Models;
using AirportTicketBookingSystem.Utilties;
using AirportTicketBookingSystem.Utilties.ManagerOptionsHandling.ErrorHandling;
using AirportTicketBookingSystem.Utilties.PassengerOptionsHandling.BookingHandling;
using AirportTicketBookingSystem.Utilties.PassengerOptionsHandling.FlightsHandling;
using AutoFixture;
using AutoFixture.AutoMoq;
using CsvHelper.Configuration;
using CsvHelper;
using Moq;
using System.Globalization;

namespace AirportTicketBookingSystem.tests
{
    public class PassengerBookingTests
    {
        List<Flight> AllFlights = new List<Flight>();
        Manager mockManager;
        Mock<IFlightFilter> mockFlightFilter;
        IFixture fixture;
        Passenger passenger;

        public PassengerBookingTests()
        {
            ImportFromCsvAsync().Wait();
            mockFlightFilter = new Mock<IFlightFilter>();

            fixture = new Fixture().Customize(new AutoMoqCustomization { ConfigureMembers = true });
            mockManager = fixture.Create<Manager>();
            passenger = fixture.Create<Passenger>();
            passenger.Bookings!.Clear();
        }

        [Fact]
        public void BookFlight_ShouldBookFlight()
        {
            //Arrange
            var passengerBooking = new PassengerFlightBooker(mockManager, mockFlightFilter.Object);
            ClassType randomClassType = fixture.Create<ClassType>();

            mockFlightFilter.Setup(f => f.PrintAllFlights(It.IsAny<List<Flight>>()));

            //Act
            passengerBooking.BookFlight(passenger, 1, randomClassType, AllFlights);
            var bookedFlight = passenger.Bookings!.First();

            //Assert
            Assert.Single(passenger.Bookings);
            Assert.Equal(1, bookedFlight.Flight.FlightId);
            Assert.Equal(randomClassType, bookedFlight.ClassType);
        }

        [Fact]
        public void BookFlight_ShouldNotBookFlight()
        {
            //Arrange
            var passengerBooking = new PassengerFlightBooker(mockManager, mockFlightFilter.Object);
            ClassType randomClassType = fixture.Create<ClassType>();

            mockFlightFilter.Setup(f => f.PrintAllFlights(It.IsAny<List<Flight>>()));

            //Act
            passengerBooking.BookFlight(passenger, 1, randomClassType, AllFlights);
            passengerBooking.BookFlight(passenger, 1, randomClassType, AllFlights);

            //Assert
            Assert.Single(passenger.Bookings!);
            Assert.Equal(1, passenger.Bookings.First().Flight.FlightId);
        }

        [Fact]
        public void CancelPersonalBooking_ShouldCancelBooking()
        {
            //Arrange
            PassengerFlightManager passengerFlightManager = new PassengerFlightManager(mockManager);
            Booking booking = fixture.Create<Booking>();
            passenger.Bookings!.Add(booking);

            //Act
            passengerFlightManager.CancelPersonalBooking(passenger, booking.BookingId);

            //Assert
            Assert.Empty(passenger.Bookings!);
        }

        [Fact]
        public void CancelPersonalBooking_ShouldNotCancelBooking()
        {
            //Arrange
            PassengerFlightManager passengerFlightManager = new PassengerFlightManager(mockManager);
            Booking booking = fixture.Create<Booking>();
            passenger.Bookings!.Add(booking);

            //Act
            passengerFlightManager.CancelPersonalBooking(passenger, booking.BookingId + 1);

            //Assert
            Assert.Single(passenger.Bookings!);
            Assert.Equal(booking.BookingId, passenger.Bookings.First().BookingId);
        }

        [Fact]
        public void ModifyPersonalBooking_ShouldModifyBooking()
        {
            //Arrange
            var stringWriter = new StringWriter();
            Console.SetOut(stringWriter);
            PassengerFlightManager passengerFlightManager = new PassengerFlightManager(mockManager);
            Booking booking = fixture.Create<Booking>();
            booking.ClassType = ClassType.Economy;
            passenger.Bookings!.Add(booking);

            //Act
            passengerFlightManager.ModifyPersonalBooking(passenger, booking.BookingId, ClassType.Business);
            var output = stringWriter.ToString().Trim();
            Console.SetOut(Console.Out);

            //Assert
            var modifiedBooking = passenger.Bookings!.First();
            Assert.Equal(ClassType.Business, modifiedBooking.ClassType);
            Assert.Equal("Booking Modified", output);
        }

        [Fact]
        public void ModifyPersonalBooking_ShouldNotModifyBooking()
        {
            //Arrange
            var stringWriter = new StringWriter();
            Console.SetOut(stringWriter);
            PassengerFlightManager passengerFlightManager = new PassengerFlightManager(mockManager);
            Booking booking = fixture.Create<Booking>();
            booking.ClassType = ClassType.Economy;
            passenger.Bookings!.Add(booking);

            //Act
            passengerFlightManager.ModifyPersonalBooking(passenger, booking.BookingId + 1, ClassType.Business);
            var output = stringWriter.ToString().Trim();
            Console.SetOut(Console.Out);

            //Assert
            var unmodifiedBooking = passenger.Bookings!.FirstOrDefault(b => b.BookingId == booking.BookingId);
            Assert.Equal(booking.ClassType, unmodifiedBooking?.ClassType);
            Assert.Equal("Invalid Booking ID", output);
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
    }
}
