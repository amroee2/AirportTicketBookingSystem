﻿using System;
using System.Collections.Generic;
using AirportTicketBookingSystem.Models;

namespace AirportTicketBookingSystem
{
    class Program
    {
        static void Main(string[] args)
        {
            _ = Utilties.Utilities.GenerateFlights();
            Utilties.Utilities.PrintMenu();
        }

       
    }
}
