using AirportTicketBookingSystem.Models;

namespace AirportTicketBookingSystem.Utilties.PassengerOptionsHandling.AccountHandling
{
    public interface IAccount
    {
        public IPassenger RequestLogInDetails();
        public IPassenger LogIn(int id, string? firstName = null, string? lastName = null);
        public IPassenger CheckPassenger(int passengerId);

    }
}
