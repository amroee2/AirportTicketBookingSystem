﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirportTicketBookingSystem
{
    public class Booking
    {
        public int BookingId { get; set; }
        public Flight? Flight { get; set; }
    }
}
