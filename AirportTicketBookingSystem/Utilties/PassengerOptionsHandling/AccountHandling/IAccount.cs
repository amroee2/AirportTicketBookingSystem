using AirportTicketBookingSystem.Models;

namespace AirportTicketBookingSystem.Utilties.PassengerOptionsHandling.AccountHandling
{
    public interface IAccount
    {
        public Passenger RequestLogInDetails();
        public Passenger LogIn(int id, string? firstName = null, string? lastName = null);
        public Passenger CheckPassenger(int passengerId);

    }
}
