﻿using AirportTicketBookingSystem.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace AirportTicketBookingSystem.Models
{
    public class Flight
    {
        [Required(ErrorMessage = "FlightId is required.")]
        [UniqueFlightId(ErrorMessage = "Flight with this id already exists")]
        public int FlightId { get; set; }

        [Required(ErrorMessage = "Departure Date is required.")]
        [DataType(DataType.Date)]
        [FutureDate(ErrorMessage = "Departure Date must be a future date.")]
        public DateTime DepartureDate { get; set; }

        [Required(ErrorMessage = "Departure Country is required.")]
        public string DepartureCountry { get; set; }

        [Required(ErrorMessage = "Destination Country is required.")]
        public string DestinationCountry { get; set; }

        [Required(ErrorMessage = "Departure Airport is required.")]
        public string DepartureAirport { get; set; }

        [Required(ErrorMessage = "Arrival Airport is required.")]
        public string ArrivalAirport { get; set; }

        public Flight(int flightId, DateTime departureDate, string departureCountry,
            string destinationCountry, string departureAirport, string arrivalAirport)
        {
            FlightId = flightId;
            DepartureDate = departureDate;
            DepartureCountry = departureCountry;
            DestinationCountry = destinationCountry;
            DepartureAirport = departureAirport;
            ArrivalAirport = arrivalAirport;
        }
        public override string ToString()
        {
            return $"Flight ID: {FlightId}, Departure: {DepartureDate} from {DepartureCountry} ({DepartureAirport}) to {DestinationCountry} ({ArrivalAirport})";
        }
    }
}
