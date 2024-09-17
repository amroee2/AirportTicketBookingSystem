using AirportTicketBookingSystem.Models;

namespace AirportTicketBookingSystem.Utilties.PassengerOptionsHandling.AccountHandling
{
    public class PassengerAccount : IAccount
    {
        public IManager _manager { get; set; }

        public PassengerAccount(IManager manager)
        {
            _manager = manager;
        }
        public IPassenger LogIn()
        {
            Console.WriteLine("Welcome Passenger!");
            Console.WriteLine("ID");
            string input = Console.ReadLine();
            _ = int.TryParse(input, out int id);
            IPassenger passenger = CheckPassenger(id);
            if (passenger is not null)
            {
                Console.WriteLine("Welcome back!");
                Console.WriteLine(passenger);
            }
            else
            {
                Console.WriteLine("First Name");
                string? firstName = Console.ReadLine();
                Console.WriteLine("Last Name");
                string? lastName = Console.ReadLine();
                passenger = new Passenger(firstName, lastName, id);
                _manager.AllPassengers!.Add(passenger);
            }

            return passenger;
        }
        public IPassenger CheckPassenger(int passengerId)
        {
            var passenger = _manager.AllPassengers!.FirstOrDefault(p => p.PassengerId == passengerId);
            return passenger;
        }
    }
}
