using AirportTicketBookingSystem.Enums;
using AirportTicketBookingSystem.Models;

namespace AirportTicketBookingSystem.Utilties.PassengerOptionsHandling.BookingHandling
{
    public class PassengerFlightManager : IFlightManager
    {
        public Manager _manager;

        public PassengerFlightManager(Manager manager)
        {
            _manager = manager;
        }
        public void ManageBookings(Passenger passenger)
        {
            if (!passenger.Bookings.Any())
            {
                Console.WriteLine("No bookings found");
                return;
            }
            while (true)
            {
                Console.WriteLine("Manage bookings\n1-View personal bookings\n2-Cancel a booking\n3-Modify a booking\n0-Go Back");
                try
                {
                    if (Enum.TryParse(Console.ReadLine(), out ManageBooking operation))
                    {
                        if (operation == ManageBooking.Exit)
                        {
                            break;
                        }
                        CheckManagingOption(passenger, operation);
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
        }

        private void CheckManagingOption(Passenger passenger, ManageBooking operation)
        {
            switch (operation)
            {
                case ManageBooking.ViewPersonalBooking:
                    ViewPersonalBookings(passenger);
                    break;
                case ManageBooking.CancelBooking:
                    ViewPersonalBookings(passenger);
                    Console.WriteLine("Enter the booking ID:");
                    _ = int.TryParse(Console.ReadLine(), out int bookingId);
                    CancelPersonalBooking(passenger, bookingId);
                    break;
                case ManageBooking.ModifyBooking:
                    ViewPersonalBookings(passenger);
                    Console.WriteLine("Enter the booking ID:");
                    _ = int.TryParse(Console.ReadLine(), out bookingId);
                    Console.WriteLine("1-Economy\n2-Business\n3-First Class");
                    _ = Enum.TryParse(Console.ReadLine(), out ClassType classType);
                    ModifyPersonalBooking(passenger, bookingId, classType);
                    break;
                default:
                    Console.WriteLine("Invalid Option");
                    break;
            }
        }

        public void ModifyPersonalBooking(Passenger passenger, int bookingId, ClassType classType)
        {
            Booking selectedBooking = passenger.Bookings.FirstOrDefault(b => b.BookingId == bookingId);
            if (selectedBooking == null)
            {
                Console.WriteLine("Invalid Booking ID");
                return;
            }
            else
            {
                selectedBooking.ClassType = classType;
                Console.WriteLine("Booking Modified");
            }
        }

        public void CancelPersonalBooking(Passenger passenger, int bookingId)
        {
            Booking selectedBooking = passenger.Bookings.FirstOrDefault(b => b.BookingId == bookingId);
            if (selectedBooking == null)
            {
                Console.WriteLine("Invalid Booking ID");
                return;
            }
            else
            {
                _manager.AllBookings!.Remove(selectedBooking);
                passenger.Bookings.Remove(selectedBooking);
                Console.WriteLine("Booking Cancelled");
            }
        }

        public void ViewPersonalBookings(Passenger passenger)
        {
            foreach (var booking in passenger.Bookings)
            {
                Console.WriteLine(booking);
            }
        }
    }
}
