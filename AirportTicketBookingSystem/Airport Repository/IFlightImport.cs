namespace AirportTicketBookingSystem.Airport_Repository
{
    public interface IFlightImport
    {
        public Task ImportFromCsvAsync();
    }
}
