using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Lab4
{
	public class CareDTO
	{
        [RegularExpression("^[А-ЯЇІЄҐ][а-яїієґ]{2,14}$")]
        public string Surname { get; set; }

        [RangeDate()]
        public DateTime Date { get; set; }

        public List<Order> Orders { get; set; }
    }
}

