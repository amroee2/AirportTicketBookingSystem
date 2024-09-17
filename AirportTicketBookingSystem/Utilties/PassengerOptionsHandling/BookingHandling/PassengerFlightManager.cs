using AirportTicketBookingSystem.Enums;
using AirportTicketBookingSystem.Models;

namespace AirportTicketBookingSystem.Utilties.PassengerOptionsHandling.BookingHandling
{
    public class PassengerFlightManager : IFlightManager
    {
        public IManager _manager;

        public PassengerFlightManager(IManager manager)
        {
            _manager = manager;
        }
        public void ManageBookings(IPassenger passenger)
        {

            while (true)
            {
                Console.WriteLine("Manage bookings\n1-View personal bookings\n2-Cancel a booking\n3-Modify a booking\n0-Go Back");
                try
                {
                    if (Enum.TryParse(Console.ReadLine(), out ManageBooking operation))
                    {
                        CheckManagingOption(passenger, operation);
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
        }

        private void CheckManagingOption(IPassenger passenger, ManageBooking operation)
        {
            switch (operation)
            {
                case ManageBooking.Exit:
                    Console.WriteLine("Exiting");
                    return;
                case ManageBooking.ViewPersonalBooking:
                    ViewPersonalBookings(passenger);
                    break;
                case ManageBooking.CancelBooking:
                    CancelPersonalBooking(passenger);
                    break;
                case ManageBooking.ModifyBooking:
                    ModifyPersonalBooking(passenger);
                    break;
                default:
                    Console.WriteLine("Invalid Option");
                    break;
            }
        }

        private void ModifyPersonalBooking(IPassenger passenger)
        {
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
            _ = int.TryParse(Console.ReadLine(), out int bookingId);
            IBooking selectedBooking = passenger.Bookings.FirstOrDefault(b => b.BookingId == bookingId);
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
        }

        private void CancelPersonalBooking(IPassenger passenger)
        {
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
            IBooking selectedBooking = passenger.Bookings.FirstOrDefault(b => b.BookingId == bookingId);
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

        private static void ViewPersonalBookings(IPassenger passenger)
        {
            foreach (var booking in passenger.Bookings)
            {
                Console.WriteLine(booking);
            }
        }
    }
}
