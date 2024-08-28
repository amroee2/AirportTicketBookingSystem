using AirportTicketBookingSystem.Utilties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirportTicketBookingSystem.Models
{
    public class Passenger
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public int PassengerId { get; set; }
        public List<Booking>? Bookings { get; set; }

        public Passenger(string firstName, string lastName, int passengerId)
        {
            FirstName = firstName;
            LastName = lastName;
            PassengerId = passengerId;
            Bookings = new List<Booking>();
        }
        public void BookFlight()
        {
            PassengerUtilities.CheckAvailableFlights();
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
                Booking booking = new Booking(Bookings.Count + 1, $"{FirstName} {LastName}", PassengerId, selectedFlight, (ClassType)classType);
                Bookings.Add(booking);
                Console.WriteLine("Booking Successful! Booking details:");
                Console.WriteLine(booking.ToString());
            }
        }
    }

}
