using AirportTicketBookingSystem.Utilties.ManagerOptionsHandling.ErrorHandling;

namespace AirportTicketBookingSystem.Utilties.ManagerOptionsHandling.BookingHandling
{
    public class BookingManager : IBookingManager
    {
        private readonly IBookingFilter _bookingFilter;
        private readonly IErrorLogger _errorLogger;

        public BookingManager(IBookingFilter bookingFilter, IErrorLogger errorLogger)
        {
            _bookingFilter = bookingFilter;
            _errorLogger = errorLogger;
        }

        public void FilterBookings()
        {
            _bookingFilter.FilterBookings();
        }

        public void ViewErrorMessages()
        {
            foreach (var error in _errorLogger.ErrorMessages)
            {
                Console.WriteLine(error);
            }
        }
    }
}
