using System;
using System.ComponentModel.DataAnnotations;

namespace MaintenanceServiceMVC.Models.Validation
{
    public class FutureDateAttribute : ValidationAttribute
    {
        public override bool IsValid(object? value)
        {
            if (value is DateTime dt)
            {
                return dt > DateTime.Now;
            }
            return true; // allow null
        }

        public override string FormatErrorMessage(string name)
        {
            return $"{name} must be a future date and time.";
        }
    }
}