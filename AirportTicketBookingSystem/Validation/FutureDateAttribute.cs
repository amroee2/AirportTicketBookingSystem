using System;
using System.ComponentModel.DataAnnotations;

namespace AirportTicketBookingSystem.Validation
{
    public class FutureDateAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            if (value is DateTime dateTime)
            {
                return dateTime > DateTime.Today;
            }
            return false;
        }

        public override string FormatErrorMessage(string date)
        {
            return $"The {date} must be a future date.";
        }
    }
}
