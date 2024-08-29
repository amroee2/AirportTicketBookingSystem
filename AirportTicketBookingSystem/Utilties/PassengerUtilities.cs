using AirportTicketBookingSystem.Models;

namespace AirportTicketBookingSystem.Utilties
{
    public class PassengerUtilities
    {
        public static int incrementBookingId = 1;
        public static void PrintMenu()
        {
            Console.WriteLine("Welcome Passenger!");
            Console.WriteLine("ID");
            int id = Convert.ToInt32(Console.ReadLine());
            Passenger passenger = CheckPassenger(id);
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
                Console.WriteLine("1-Book a Flight\n2-Check Available flights\n3-Manage flights\n4-Exit");
                int op = Convert.ToInt32(Console.ReadLine());
                switch (op)
                {
                    case 4:
                        Console.WriteLine("Exiting");
                        return;
                    case 1:
                        BookFlight(passenger);
                        break;
                    case 2:
                        CheckAvailableFlights();
                        break;
                    case 3:
                        ManageBookings(passenger);
                        break;
                    default:
                        Console.WriteLine("Invalid Option");
                        break;
                }
            }
        }
        public static void BookFlight(Passenger passenger)
        {
            CheckAvailableFlightsByDate(DateTime.Now);
            Console.WriteLine("Enter the flight ID you want to book");
            int flightId = Convert.ToInt32(Console.ReadLine());
            Flight? selectedFlight = Utilities.flights.FirstOrDefault(f => f.FlightId == flightId);
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
                int classType = Convert.ToInt32(Console.ReadLine());
                Booking booking = new Booking(incrementBookingId, $"{passenger.FirstName} {passenger.LastName}", passenger.PassengerId, selectedFlight, (ClassType)classType);
                incrementBookingId++;
                passenger.Bookings.Add(booking);
                Manager.AllBookings!.Add(booking);
                Console.WriteLine("Booking Successful! Booking details:");
                Console.WriteLine(booking.ToString());
            }
        }
        public static void CheckAvailableFlightsByDate(DateTime time)
        {
            var availableFlights = Utilities.flights
                .Where(f => DateTime.TryParse(f.DepartureDate, out DateTime departureDate) && departureDate > time)
                .ToList();
            foreach (var flight in availableFlights)
            {
                Console.WriteLine(flight);
            }
        }
        public static void CheckAvailableFlights()
        {
            Console.WriteLine("Search by\n1-Departure country\n2-Destination country\n3-Departure date" +
                "\n4-Departure airport\n5-Arrival airport\n6-All\n0-Exit");
            int op = Convert.ToInt32(Console.ReadLine());
            switch (op)
            {
                case 0: Console.WriteLine("Exiting"); return;
                case 1:
                    Console.WriteLine("Enter the departure country");
                    string departureCountry = Console.ReadLine();
                    var flights = Utilities.flights.Where(f => f.DepartureCountry == departureCountry).ToList();
                    foreach (var flight in flights)
                    {
                        Console.WriteLine(flight);
                    }
                    break;
                case 2:
                    Console.WriteLine("Enter the destination country");
                    string destinationCountry = Console.ReadLine();
                    flights = Utilities.flights.Where(f => f.DestinationCountry == destinationCountry).ToList();
                    foreach (var flight in flights)
                    {
                        Console.WriteLine(flight);
                    }
                    break;
                case 3:
                    Console.WriteLine("Enter the departure date");
                    string departureDate = Console.ReadLine();
                    flights = Utilities.flights.Where(f => f.DepartureDate == departureDate).ToList();
                    foreach (var flight in flights)
                    {
                        Console.WriteLine(flight);
                    }
                    break;
                case 4:
                    Console.WriteLine("Enter the departure airport");
                    string departureAirport = Console.ReadLine();
                    flights = Utilities.flights.Where(f => f.DepartureAirport == departureAirport).ToList();
                    foreach (var flight in flights)
                    {
                        Console.WriteLine(flight);
                    }
                    break;
                case 5:
                    Console.WriteLine("Enter the arrival airport");
                    string arrivalAirport = Console.ReadLine();
                    flights = Utilities.flights.Where(f => f.ArrivalAirport == arrivalAirport).ToList();
                    foreach (var flight in flights)
                    {
                        Console.WriteLine(flight);
                    }
                    break;
                case 6:
                    foreach (var flight in Utilities.flights)
                    {
                        Console.WriteLine(flight);
                    }
                    break;
                default:
                    Console.WriteLine("Invalid Option");
                    break;
            }
        }
        public static void ManageBookings(Passenger passenger)
        {

            while (true)
            {
                Console.WriteLine("Manage bookings\n1-View personal bookings\n2-Cancel a booking\n3-Modify a booking\n0-Go Back");
                int op = Convert.ToInt32(Console.ReadLine());
                switch (op)
                {
                    case 0:
                        Console.WriteLine("Exiting");
                        return;
                    case 1:
                        foreach (var booking in passenger.Bookings)
                        {
                            Console.WriteLine(booking);
                        }
                        break;
                    case 2:
                        foreach (var booking in passenger.Bookings)
                        {
                            Console.WriteLine(booking);
                        }
                        Console.WriteLine("Enter the booking ID you want to cancel");
                        int bookingId = Convert.ToInt32(Console.ReadLine());
                        Booking? selectedBooking = passenger.Bookings.FirstOrDefault(b => b.BookingId == bookingId);
                        if (selectedBooking == null)
                        {
                            Console.WriteLine("Invalid Booking ID");
                            return;
                        }
                        else
                        {
                            passenger.Bookings.Remove(selectedBooking);
                            Console.WriteLine("Booking Cancelled");
                        }
                        break;
                    case 3:
                        foreach (var booking in passenger.Bookings)
                        {
                            Console.WriteLine(booking);
                        }
                        Console.WriteLine("Enter the booking ID you want to modify");
                        bookingId = Convert.ToInt32(Console.ReadLine());
                        selectedBooking = passenger.Bookings.FirstOrDefault(b => b.BookingId == bookingId);
                        if (selectedBooking == null)
                        {
                            Console.WriteLine("Invalid Booking ID");
                            return;
                        }
                        else
                        {
                            Console.WriteLine("1-Economy\n2-Business\n3-First Class");
                            int classType = Convert.ToInt32(Console.ReadLine());
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
        public static Passenger CheckPassenger(int passengerId)
        {
            var passenger = Manager.AllPassengers!.FirstOrDefault(p => p.PassengerId == passengerId);
            return passenger;
        }
    }
}
