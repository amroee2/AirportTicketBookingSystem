﻿using AirportTicketBookingSystem.Models;
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
            Console.WriteLine("1-Book a Flight\n2-Check Available flights\n3-Manage flights\n4-Exit");
            Passenger passenger = new Passenger(firstName, lastName, id);
            int op = Convert.ToInt32(Console.ReadLine());
            switch (op)
            {
                case 0:
                    Console.WriteLine("Invalid Option");
                    PrintMenu();
                    break;
                case 1:
                    passenger.BookFlight();
                    break;
                case 2:
                    CheckAvailableFlights();
                    break;
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
