﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AirportTicketBookingSystem.Utilties;

namespace AirportTicketBookingSystem.Validation
{
    public class UniqueFlightIdAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            int id = (int)value;
            var flight = GeneralUtility.flights.Find(f => f.FlightId == id);
            if (flight == null)
            {
                return true;
            }
            ErrorMessage = $"Flight with ID {id} already exists.";

            return false;
        }
    }

}
