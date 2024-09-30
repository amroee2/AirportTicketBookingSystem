using AirportTicketBookingSystem.Models;
using AirportTicketBookingSystem.Utilties.PassengerOptionsHandling.AccountHandling;
using Moq;
using Xunit;
using System;
using System.Collections.Generic;
using System.IO;

namespace AirportTicketBookingSystem.tests
{
    public class PassengerAccountTests
    {
        private readonly Mock<IManager> mockManager;

        public PassengerAccountTests()
        {
            mockManager = new Mock<IManager>();
            mockManager.Setup(x => x.AllPassengers).Returns(new List<IPassenger>());
        }

        [Fact]
        public void LogIn_ShouldCreateNewPassenger()
        {
            // Arrange
            PassengerAccount passengerAccount = new PassengerAccount(mockManager.Object);

            // Act
            IPassenger passenger = passengerAccount.LogIn(1, "Amro", "Qadadha");

            // Assert
            Assert.Contains(passenger, mockManager.Object.AllPassengers!);
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
            mockManager.Setup(x => x.AllPassengers).Returns(new List<IPassenger> { mockPassenger });
            PassengerAccount passengerAccount = new PassengerAccount(mockManager.Object);

            // Act
            IPassenger passenger = passengerAccount.LogIn(1, "Amro", "Qadadha");
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
