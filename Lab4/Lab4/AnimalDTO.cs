using System;
using System.ComponentModel.DataAnnotations;

namespace Lab4
{
	public class AnimalDTO
	{
        [RegularExpression("^[А-ЯЇІЄҐ][а-яїієґ]{2,19}$")]
        public string NameOfAnimal { get; set; }

        [RegularExpression("^[А-ЯЇІЄҐ][а-яїієґ]{2,19}$")]
        public string FirstNameOfAnimal { get; set; }

        [MaxYear()]
        public int YearOfBirth { get; set; }

        public bool GenderOfAnimal { get; set; }
    }
}

