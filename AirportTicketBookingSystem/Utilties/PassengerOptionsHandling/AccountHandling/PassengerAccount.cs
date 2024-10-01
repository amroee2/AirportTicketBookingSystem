using AirportTicketBookingSystem.Models;

namespace AirportTicketBookingSystem.Utilties.PassengerOptionsHandling.AccountHandling
{
    public class PassengerAccount : IAccount
    {
        public Manager _manager { get; set; }

        public PassengerAccount(Manager manager)
        {
            _manager = manager;
        }
        public Passenger LogIn(int id, string? firstName = null, string? lastName = null)
        {
            Passenger passenger = CheckPassenger(id);
            if (passenger is not null)
            {
                Console.WriteLine("Welcome back!");
                Console.WriteLine(passenger);
            }
            else
            {
                passenger = new Passenger(firstName, lastName, id);
                _manager.AllPassengers!.Add(passenger);
            }

            return passenger;
        }

        public Passenger RequestLogInDetails()
        {
            Console.WriteLine("Welcome Passenger!");
            Console.WriteLine("ID");
            string input = Console.ReadLine();
            _ = int.TryParse(input, out int id);
            Passenger passenger = CheckPassenger(id);
            if (passenger is null)
            {
                Console.WriteLine("First Name");
                string? firstName = Console.ReadLine();
                Console.WriteLine("Last Name");
                string? lastName = Console.ReadLine();
                return LogIn(id, firstName, lastName);
            }

            return LogIn(id);
        }

        public Passenger CheckPassenger(int passengerId)
        {
            var passenger = _manager.AllPassengers!.FirstOrDefault(p => p.PassengerId == passengerId);
            return passenger;
        }
    }
}
