using System;
using System.Collections.Generic;
using AirportTicketBookingSystem.Airport_Repository;
using AirportTicketBookingSystem.Models;
using AirportTicketBookingSystem.Utilties;

namespace AirportTicketBookingSystem
{
    class Program
    {
        static void Main(string[] args)
        {
            IFlightValidator flightValidator = new FlightValidator();
            IFlightImport flightImport = new FlightImport(flightValidator);
            GeneralUtility utilities = new GeneralUtility(flightImport);
            _ = utilities.GenerateFlightsAsync();
            Utilties.GeneralUtility.PrintMenu();
        }

       
    }
}
