namespace AirportTicketBookingSystem.Models
{
    public interface IBooking
    {
        public int BookingId { get; set; }
        public string? PassengerName { get; set; }
        public int PassengerId { get; set; }
        public IFlight? Flight { get; set; }
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
    }
}
