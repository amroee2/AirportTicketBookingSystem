using AirportTicketBookingSystem.Enums;
using AirportTicketBookingSystem.Utilties;

public class ManagerUtilities
{
    private readonly FileManager _fileManager;
    private readonly BookingManager _bookingManager;

    public ManagerUtilities(FileManager fileManager, BookingManager bookingManager)
    {
        _fileManager = fileManager;
        _bookingManager = bookingManager;
    }

    public void PrintMenu()
    {
        Console.WriteLine("Welcome Manager!");
        while (true)
        {
            try
            {
                Console.WriteLine("1-Filter Bookings\n2-Export to CSV\n3-Import from CSV\n4-View error messages from last import\n0-Go back");
                if (Enum.TryParse(Console.ReadLine(), out ManagerOption operation))
                {
                    HandleManagerRequest(operation);
                }
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.Message);
            }
        }
    }

    private void HandleManagerRequest(ManagerOption operation)
    {
        switch (operation)
        {
            case ManagerOption.Exit:
                return;
            case ManagerOption.FilterBookings:
                _bookingManager.FilterBookings();
                break;
            case ManagerOption.ExportBookingsToCsv:
                _fileManager.ExportToCsvAsync();
                break;
            case ManagerOption.ImportBookingsToCsv:
                _fileManager.ImportFromCsvAsync();
                break;
            case ManagerOption.ViewErrorMessages:
                _bookingManager.ViewErrorMessages();
                break;
            default:
                Console.WriteLine("Invalid Option");
                break;
        }
    }
}
