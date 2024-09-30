using AirportTicketBookingSystem.Models;
using AirportTicketBookingSystem.Utilties.PassengerOptionsHandling.AccountHandling;
using Moq;

namespace AirportTicketBookingSystem.tests
{
    public class PassengerAccountTests
    {
        Mock<IManager> mockManager;

        public PassengerAccountTests()
        {
            mockManager = new Mock<IManager>();
            mockManager.Setup(x => x.AllPassengers).Returns(new List<IPassenger>());
        }

        [Fact]
        public void LogIn_ShouldCreateNewPassenger()
        {
            //Arrange
            var mockPassengerAccount = new Mock<IAccount>() { CallBase = true };
            mockPassengerAccount.Setup(x => x.CheckPassenger(It.IsAny<int>())).Returns((IPassenger)null);
            PassengerAccount passengerAccount = new PassengerAccount(mockManager.Object);

            //Act
            IPassenger passenger = passengerAccount.LogIn(1, "Amro", "Qadadha");

            //Assert
            Assert.Contains(passenger, mockManager.Object.AllPassengers!);
        }

        [Fact]
        public void LogIn_ShouldReturnExistingPassenger()
        {
            //Arrange
            var mockPassenger = new Passenger("Amro", "Qadadha", 1);
            var mockPassengerAccount = new Mock<IAccount>() { CallBase = true };
            mockPassengerAccount.Setup(x => x.CheckPassenger(It.IsAny<int>())).Returns(mockPassenger);
            PassengerAccount passengerAccount = new PassengerAccount(mockManager.Object);

            //Act
            IPassenger passenger = passengerAccount.LogIn(1, "Amro", "Qadadha");

            //Assert
            Assert.Equal("Amro", passenger.FirstName);
            Assert.Equal("Qadadha", passenger.LastName);
            Assert.Equal(1, passenger.PassengerId);
        }
    }
}
