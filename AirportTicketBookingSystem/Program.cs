﻿using AirportTicketBookingSystem.Airport_Repository;
using AirportTicketBookingSystem.Models;
using AirportTicketBookingSystem.Utilties;
using AirportTicketBookingSystem.Utilties.ManagerOptionsHandling.BookingHandling;
using AirportTicketBookingSystem.Utilties.ManagerOptionsHandling.ErrorHandling;
using AirportTicketBookingSystem.Utilties.ManagerOptionsHandling.FileHandling;
using AirportTicketBookingSystem.Utilties.PassengerOptionsHandling;
using AirportTicketBookingSystem.Utilties.PassengerOptionsHandling.AccountHandling;
using AirportTicketBookingSystem.Utilties.PassengerOptionsHandling.BookingHandling;
using AirportTicketBookingSystem.Utilties.PassengerOptionsHandling.FlightsHandling;
using Microsoft.Extensions.DependencyInjection;

class Program
{
    static void Main(string[] args)
    {
        var serviceProvider = new ServiceCollection()
            .AddScoped<IFlightExportRepository, FlightExportRepository>()
            .AddScoped<IFlightImportRepository, FlightImportRepository>()
            .AddScoped<IFlightValidator, FlightValidator>()
            .AddScoped<Booking, Booking>()
            .AddScoped<Passenger, Passenger>()
            .AddScoped<Flight, Flight>()
            .AddScoped<Passenger, Passenger>()
            .AddScoped<Manager, Manager>()
            .AddScoped<IErrorLogger, ErrorLogger>()
            .AddScoped<IFileManager, FileManager>()
            .AddScoped<IBookingFilter, BookingChecker>()
            .AddScoped<IBookingManager, BookingManager>()
            .AddScoped<IFileManager, FileManager>()
            .AddScoped<IAccount, PassengerAccount>()
            .AddScoped<IBookingHandler, BookingHandler>()
            .AddScoped<IFlightManager, PassengerFlightManager>()
            .AddScoped<IPassengerBooking, PassengerFlightBooker>()
            .AddScoped<IFlightFilter, FlightChecker>()
            .AddScoped<IFlightConstraints, FlightConstraints>()
            .AddScoped<IFlightHandler, FlightHandler>()
            .AddScoped<ManagerUtilities>()
            .AddScoped<PassengerUtilities>()
            .AddScoped<GeneralUtility>()
            .BuildServiceProvider();
        var generalUtility = serviceProvider.GetRequiredService<GeneralUtility>();
        FlightImportRepository flightImport = new FlightImportRepository(new FlightValidator(new ErrorLogger()));
        _= flightImport.ImportFromCsvAsync("Airport Repository", "flights.csv");
        generalUtility.PrintMenu();
    }
}
