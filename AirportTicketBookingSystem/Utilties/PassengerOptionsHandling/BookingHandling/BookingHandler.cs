﻿using AirportTicketBookingSystem.Airport_Repository;
using AirportTicketBookingSystem.Models;
using AirportTicketBookingSystem.Utilties.PassengerOptionsHandling.BookingHandling;

namespace AirportTicketBookingSystem.Utilties.ManagerOptionsHandling.FileHandling
{
    public class BookingHandler : IBookingHandler
    {
        public IPassengerBooking _passengerBooking { get; set; }
        public IFlightManager _flightManager { get; set; }

        public BookingHandler(IPassengerBooking passengerBooking, IFlightManager flightManager)
        {
            _passengerBooking = passengerBooking;
            _flightManager = flightManager;
        }

        public void BookFlight(Passenger passenger)
        {
            _passengerBooking.CollectFlightBookingDetails(passenger);
        }
        public void ManageBookings(Passenger passenger)
        {
            _flightManager.ManageBookings(passenger);
        }
    }

}
