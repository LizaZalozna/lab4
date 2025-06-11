using System;
using System.ComponentModel.DataAnnotations;

namespace Lab4
{
	public class AnimalDTO
	{
        [RegularExpression("^[А-ЯЇІЄҐ][а-яїієґ]{2,19}$")]
        public string NameOfAnimal;
        [RegularExpression("^[А-ЯЇІЄҐ][а-яїієґ]{2,19}$")]
        public string FirstNameOfAnimal;
        public int YearOfBirth;
        public bool GenderOfAnimal;
    }
}

