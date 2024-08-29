using System;
using System.ComponentModel.DataAnnotations;

namespace AirportTicketBookingSystem.Models
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
