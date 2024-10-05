namespace AirportTicketBookingSystem.Utilties.ManagerOptionsHandling.FileHandling
{
    public interface IFileManager
    {
        Task ImportFromCsvAsync(string direcotry, string file);
        Task ExportToCsvAsync();
    }
}
