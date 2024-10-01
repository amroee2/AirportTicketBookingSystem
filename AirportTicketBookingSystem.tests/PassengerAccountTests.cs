using AirportTicketBookingSystem.Models;
using AirportTicketBookingSystem.Utilties.PassengerOptionsHandling.AccountHandling;
using Moq;
using Xunit;
using System;
using System.Collections.Generic;
using System.IO;
using AutoFixture;

namespace AirportTicketBookingSystem.tests
{
    public class PassengerAccountTests
    {
        private readonly Manager mockManager;

        public PassengerAccountTests()
        {
            Fixture fixture = new Fixture();
            mockManager = fixture.Create<Manager>();
        }

        [Fact]
        public void LogIn_ShouldCreateNewPassenger()
        {
            // Arrange
            PassengerAccount passengerAccount = new PassengerAccount(mockManager);

            // Act
            Passenger passenger = passengerAccount.LogIn(1, "Amro", "Qadadha");

            // Assert
            Assert.Contains(passenger, mockManager.AllPassengers!);
            Assert.Equal("Amro", passenger.FirstName);
            Assert.Equal("Qadadha", passenger.LastName);
            Assert.Equal(1, passenger.PassengerId);
        }

        [Fact]
        public void LogIn_ShouldReturnExistingPassenger()
        {
            // Arrange
            var stringWriter = new StringWriter();
            Console.SetOut(stringWriter);

            var mockPassenger = new Passenger("Amro", "Qadadha", 1);
            PassengerAccount passengerAccount = new PassengerAccount(mockManager);
            mockManager.AllPassengers.Add(mockPassenger);
            // Act
            Passenger passenger = passengerAccount.LogIn(1, "Amro", "Qadadha");
            var output = stringWriter.ToString().Trim().Split(Environment.NewLine);
            Console.SetOut(Console.Out);

            // Assert
            Assert.Equal("Amro", passenger.FirstName);
            Assert.Equal("Qadadha", passenger.LastName);
            Assert.Equal(1, passenger.PassengerId);
            Assert.Equal("Welcome back!", output[0]);
            Assert.Equal(mockPassenger.ToString(), output[1]);
        }
    }
}
