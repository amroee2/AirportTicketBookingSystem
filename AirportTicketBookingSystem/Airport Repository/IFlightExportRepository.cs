﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirportTicketBookingSystem.Utilties
{
    public interface IFlightExportRepository
    {
        Task ExportToCSVAsync();
    }
}
