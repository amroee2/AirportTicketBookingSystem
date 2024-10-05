namespace AirportTicketBookingSystem.Models
{
    public class Booking
    {
        public int BookingId { get; set; }
        public string? PassengerName { get; set; }
        public int PassengerId { get; set; }
        public Flight? Flight { get; set; }
        public ClassType ClassType { get; set; }
        public int Price
        {
            get
            {
                switch (ClassType)
                {
                    case ClassType.Economy:
                        return 100;
                    case ClassType.Business:
                        return 200;
                    case ClassType.FirstClass:
                        return 300;
                }
                return 0;
            }
        }

        public Booking(int bookingId, string passengerName, int passengerId, Flight flight, ClassType classType)
        {
            BookingId = bookingId;
            PassengerName = passengerName;
            PassengerId = passengerId;
            Flight = flight;
            ClassType = classType;
        }
        public override string ToString()
        {
            return $"Booking ID: {BookingId}, Passenger: {PassengerName} (ID: {PassengerId}), Flight: {Flight?.ToString() ?? "N/A"}, Class: {ClassType}, Price: {Price}";
        }
    }
}
