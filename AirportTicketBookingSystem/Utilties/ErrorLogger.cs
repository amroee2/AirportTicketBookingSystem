namespace AirportTicketBookingSystem.Utilties
{
    public class ErrorLogger : IErrorLogger
    {
        public List<string>? ErrorMessages { get; set; }
        public ErrorLogger()
        {
            ErrorMessages = new List<string>();
        }
    }
}
