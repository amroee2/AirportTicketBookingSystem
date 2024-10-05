namespace AirportTicketBookingSystem.Airport_Repository
{
    public interface IFlightImportRepository
    {
        public Task ImportFromCsvAsync(string directory, string file);
    }
}
