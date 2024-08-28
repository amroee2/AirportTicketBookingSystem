using AirportTicketBookingSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirportTicketBookingSystem.Utilties
{
    public class PassengerUtilities
    {
        public static void PrintMenu()
        {
            Console.WriteLine("Welcome Passenger!");
            Console.WriteLine("First Name");
            string? firstName = Console.ReadLine();
            Console.WriteLine("Last Name");
            string? lastName = Console.ReadLine();
            Console.WriteLine("ID");
            int id = Convert.ToInt32(Console.ReadLine());

            Passenger passenger = new Passenger(firstName, lastName, id);
            while (true)
            {
                Console.WriteLine("1-Book a Flight\n2-Check Available flights\n3-Manage flights\n4-Exit");
                int op = Convert.ToInt32(Console.ReadLine());
                switch (op)
                {
                    case 4:
                        Console.WriteLine("Exiting");
                        return;
                    case 1:
                        BookFlight(passenger);
                        break;
                    case 2:
                        CheckAvailableFlights();
                        break;
                    default:
                        Console.WriteLine("Invalid Option");
                        break;
                }
            }
        }
        public static void BookFlight(Passenger passenger)
        {
            CheckAvailableFlights();
            Console.WriteLine("Enter the flight ID you want to book");
            int flightId = Convert.ToInt32(Console.ReadLine());
            Flight? selectedFlight = Utilities.flights.FirstOrDefault(f => f.FlightId == flightId);
            if (selectedFlight == null)
            {
                Console.WriteLine("Invalid Flight ID");
                return;
            }
            else
            {
                Console.WriteLine("1-Economy\n2-Business\n3-First Class");
                int classType = Convert.ToInt32(Console.ReadLine());
                Booking booking = new Booking(passenger.Bookings.Count + 1, $"{passenger.FirstName} {passenger.LastName}", passenger.PassengerId, selectedFlight, (ClassType)classType);
                passenger.Bookings.Add(booking);
                Console.WriteLine("Booking Successful! Booking details:");
                Console.WriteLine(booking.ToString());
            }
        }
        public static void CheckAvailableFlights()
        {
            var availableFlights = Utilities.flights
                .Where(f => DateTime.TryParse(f.DepartureDate, out DateTime departureDate) && departureDate > DateTime.Now)
                .ToList();
            foreach (var flight in availableFlights)
            {
                Console.WriteLine(flight);
            }
        }
    }
}
