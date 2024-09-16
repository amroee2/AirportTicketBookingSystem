using AirportTicketBookingSystem.Enums;
using AirportTicketBookingSystem.Models;
using System.Reflection;

namespace AirportTicketBookingSystem.Utilties
{
    public class PassengerUtilities
    {
        public static int incrementBookingId = 1;
        public static void PrintMenu()
        {
            Console.WriteLine("Welcome Passenger!");
            Console.WriteLine("ID");
            string input = Console.ReadLine();
            _ = int.TryParse(input, out int id);
            IPassenger passenger = CheckPassenger(id);
            if(passenger is not null)
            {
                Console.WriteLine("Welcome back!");
                Console.WriteLine(passenger);
            }
            else
            {
                Console.WriteLine("First Name");
                string? firstName = Console.ReadLine();
                Console.WriteLine("Last Name");
                string? lastName = Console.ReadLine();
                passenger = new Passenger(firstName, lastName, id);
                Manager.AllPassengers!.Add(passenger);
            }        
            while (true)
            {
                try
                {
                    Console.WriteLine("1-Book a Flight\n2-Check Available flights\n3-Manage flights\n4-Check flight constraints\n0-Exit");
                    if (Enum.TryParse(Console.ReadLine(), out PassengerOption operation))
                    {
                        switch (operation)
                        {
                            case PassengerOption.Exit:
                                Console.WriteLine("Exiting");
                                return;
                            case PassengerOption.BookFlight:
                                BookFlight(passenger);
                                break;
                            case PassengerOption.CheckAvailableFlights:
                                CheckAvailableFlights(GeneralUtility.flights);
                                break;
                            case PassengerOption.ManageFlight:
                                ManageBookings(passenger);
                                break;
                            case PassengerOption.CheckFlightConstraints:
                                CheckFlightConstraints();
                                break;
                            default:
                                Console.WriteLine("Invalid Option");
                                break;
                        }
                    }
                }
                catch (Exception exception)
                {
                    Console.WriteLine(exception.Message);
                }
            }
        }
        public static void BookFlight(IPassenger passenger)
        {
            CheckAvailableFlightsByDate(DateTime.Now);
            Console.WriteLine("Enter the flight ID you want to book");
            _ = int.TryParse(Console.ReadLine(), out int flightId);
            IFlight? selectedFlight = GeneralUtility.flights.FirstOrDefault(f => f.FlightId == flightId);
            var existingBooking = passenger.Bookings.FirstOrDefault(b => b.Flight.FlightId == flightId);
            if (existingBooking != null)
            {
                Console.WriteLine("You have already booked this flight");
                return;
            }
            if (selectedFlight == null)
            {
                Console.WriteLine("Invalid Flight ID");
                return;
            }
            else
            {
                Console.WriteLine("1-Economy\n2-Business\n3-First Class");
                _ = int.TryParse(Console.ReadLine(), out int classType);
                IBooking booking = new Booking(incrementBookingId, $"{passenger.FirstName} {passenger.LastName}", passenger.PassengerId, selectedFlight, (ClassType)classType);
                incrementBookingId++;
                passenger.Bookings.Add(booking);
                Manager.AllBookings!.Add(booking);
                Console.WriteLine("Booking Successful! Booking details:");
                Console.WriteLine(booking.ToString());
            }
        }
        public static void CheckAvailableFlightsByDate(DateTime time)
        {
            var availableFlights = GeneralUtility.flights.Where(f => f.DepartureDate > time).ToList();
            foreach (var flight in availableFlights)
            {
                Console.WriteLine(flight);
            }
        }
        public static void CheckAvailableFlights(List<IFlight> FlightsList)
        {
            Console.WriteLine("Search by\n1-Departure country\n2-Destination country\n3-Departure date" +
                "\n4-Departure airport\n5-Arrival airport\n6-All\n0-Exit");
            try
            {
                if (Enum.TryParse(Console.ReadLine(), out FlightFilter operation))
                {
                    switch (operation)
                    {
                        case FlightFilter.Exit:
                            Console.WriteLine("Exiting"); return;
                        case FlightFilter.ByDepartureCountry:
                            Console.WriteLine("Enter the departure country");
                            string departureCountry = Console.ReadLine();
                            var flights = FlightsList.Where(f => f.DepartureCountry == departureCountry).ToList();
                            foreach (var flight in flights)
                            {
                                Console.WriteLine(flight);
                            }
                            break;
                        case FlightFilter.ByDestinationCountry:
                            Console.WriteLine("Enter the destination country");
                            string destinationCountry = Console.ReadLine();
                            flights = FlightsList.Where(f => f.DestinationCountry == destinationCountry).ToList();
                            foreach (var flight in flights)
                            {
                                Console.WriteLine(flight);
                            }
                            break;
                        case FlightFilter.ByDepartureDate:
                            Console.WriteLine("Enter the departure date");
                            string departureDate = Console.ReadLine();
                            DateTime date = DateTime.Parse(departureDate);
                            flights = FlightsList.Where(f => f.DepartureDate == date).ToList();
                            foreach (var flight in flights)
                            {
                                Console.WriteLine(flight);
                            }
                            break;
                        case FlightFilter.ByDepartureAirport:
                            Console.WriteLine("Enter the departure airport");
                            string departureAirport = Console.ReadLine();
                            flights = FlightsList.Where(f => f.DepartureAirport == departureAirport).ToList();
                            foreach (var flight in flights)
                            {
                                Console.WriteLine(flight);
                            }
                            break;
                        case FlightFilter.ByArrivalAirport:
                            Console.WriteLine("Enter the arrival airport");
                            string arrivalAirport = Console.ReadLine();
                            flights = FlightsList.Where(f => f.ArrivalAirport == arrivalAirport).ToList();
                            foreach (var flight in flights)
                            {
                                Console.WriteLine(flight);
                            }
                            break;
                        case FlightFilter.NoFilter:
                            foreach (var flight in FlightsList)
                            {
                                Console.WriteLine(flight);
                            }
                            break;
                        default:
                            Console.WriteLine("Invalid Option");
                            break;
                    }
                }
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.Message);
            }
        }
        public static void ManageBookings(IPassenger passenger)
        {

            while (true)
            {
                Console.WriteLine("Manage bookings\n1-View personal bookings\n2-Cancel a booking\n3-Modify a booking\n0-Go Back");
                try
                {
                    if (Enum.TryParse(Console.ReadLine(), out ManageBooking operation))
                    {
                        switch (operation)
                        {
                            case ManageBooking.Exit:
                                Console.WriteLine("Exiting");
                                return;
                            case ManageBooking.ViewPersonalBooking:
                                foreach (var booking in passenger.Bookings)
                                {
                                    Console.WriteLine(booking);
                                }
                                break;
                            case ManageBooking.CancelBooking:
                                if (!passenger.Bookings.Any())
                                {
                                    Console.WriteLine("No bookings found");
                                    return;
                                }
                                foreach (var booking in passenger.Bookings)
                                {
                                    Console.WriteLine(booking);
                                }
                                Console.WriteLine("Enter the booking ID you want to cancel");
                                _ = int.TryParse(Console.ReadLine(), out int bookingId);
                                IBooking? selectedBooking = passenger.Bookings.FirstOrDefault(b => b.BookingId == bookingId);
                                if (selectedBooking == null)
                                {
                                    Console.WriteLine("Invalid Booking ID");
                                    return;
                                }
                                else
                                {
                                    Manager.AllBookings!.Remove(selectedBooking);
                                    passenger.Bookings.Remove(selectedBooking);
                                    Console.WriteLine("Booking Cancelled");
                                }
                                break;
                            case ManageBooking.ModifyBooking:
                                if (!passenger.Bookings.Any())
                                {
                                    Console.WriteLine("No bookings found");
                                    return;
                                }
                                foreach (var booking in passenger.Bookings)
                                {
                                    Console.WriteLine(booking);
                                }
                                Console.WriteLine("Enter the booking ID you want to modify");
                                _ = int.TryParse(Console.ReadLine(), out bookingId);
                                selectedBooking = passenger.Bookings.FirstOrDefault(b => b.BookingId == bookingId);
                                if (selectedBooking == null)
                                {
                                    Console.WriteLine("Invalid Booking ID");
                                    return;
                                }
                                else
                                {
                                    Console.WriteLine("1-Economy\n2-Business\n3-First Class");
                                    _ = int.TryParse(Console.ReadLine(), out int classType);
                                    selectedBooking.ClassType = (ClassType)classType;
                                    Console.WriteLine("Booking Modified");
                                }
                                break;
                            default:
                                Console.WriteLine("Invalid Option");
                                break;
                        }
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
        }
        public static IPassenger CheckPassenger(int passengerId)
        {
            var passenger = Manager.AllPassengers!.FirstOrDefault(p => p.PassengerId == passengerId);
            return passenger;
        }
        public static void CheckFlightConstraints()
        {
            var assembly = Assembly.GetExecutingAssembly();
            var type = assembly.GetType("AirportTicketBookingSystem.Models.Flight");
            var properties = type.GetProperties();
            foreach (var property in properties)
            {
                Console.WriteLine($"Property: {property.Name}");
                Console.WriteLine($"- Type: {property.PropertyType.Name}");
                var attributes = property.GetCustomAttributes();
                foreach (var attribute in attributes)
                {
                    Console.WriteLine($"- Constraint: {attribute.GetType().Name}");
                    var errorMessageProperty = attribute.GetType().GetProperty("ErrorMessage");
                    if (errorMessageProperty != null)
                    {
                        var errorMessage = errorMessageProperty.GetValue(attribute) as string;
                        if (!string.IsNullOrEmpty(errorMessage))
                        {
                            Console.WriteLine($"  - Error Message: {errorMessage}");
                        }
                    }
                }
            }
        }
    }
}
