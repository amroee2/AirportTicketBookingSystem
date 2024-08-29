﻿using System;
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
        public int FlightId { get; set; }

        [Required(ErrorMessage = "Departure Date is required.")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Range(typeof(DateTime), "today", "9999-12-31", ErrorMessage = "Departure Date must be today or in the future.")]
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
