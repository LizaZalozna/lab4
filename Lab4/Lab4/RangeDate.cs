using System;
using System.ComponentModel.DataAnnotations;
namespace Lab4
{
	public class RangeDate: ValidationAttribute
    {
        DateTime date = DateTime.Now;

        public override bool IsValid(object value)
        {
            return value != null && value is DateTime && (DateTime)value > new DateTime(2000, 1, 1) && (DateTime)value <= date;
        }
    }
}