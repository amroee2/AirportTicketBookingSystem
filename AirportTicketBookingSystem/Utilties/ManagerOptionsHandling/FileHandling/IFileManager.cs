namespace AirportTicketBookingSystem.Utilties.ManagerOptionsHandling.FileHandling
{
    public interface IFileManager
    {
        Task ImportFromCsvAsync();
        Task ExportToCsvAsync();
    }
}
