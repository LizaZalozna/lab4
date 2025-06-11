using System;
using System.ComponentModel.DataAnnotations;

namespace Lab4
{
	public class MaxYear: ValidationAttribute
    {
		int yearNow = DateTime.Now.Year;

        public override bool IsValid(object? value)
        {
            return value != null && value is int && (int)value > 2000 && (int)value <= yearNow;
        }
    }
}

