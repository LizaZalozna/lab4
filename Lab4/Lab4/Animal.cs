using System;
namespace Lab4
{
	public class Animal
	{
		string nameOfAnimal;
		string firstNameOfAnimal;
		int yearOfBirth;
		bool genderOfAnimal;

        public Animal(string nameOfAnimal, string firstNameOfAnimal, int yearOfBirth, bool genderOfAnimal)
        {
            this.nameOfAnimal = nameOfAnimal;
            this.firstNameOfAnimal = firstNameOfAnimal;
            this.yearOfBirth = yearOfBirth;
            this.genderOfAnimal = genderOfAnimal;
        }

        public Animal(AnimalDTO dto)
        {
            this.nameOfAnimal = dto.NameOfAnimal;
            this.firstNameOfAnimal = dto.FirstNameOfAnimal;
            this.yearOfBirth = dto.YearOfBirth;
            this.genderOfAnimal = dto.GenderOfAnimal;
        }

        public AnimalDTO ToDTO()
        {
            return new AnimalDTO
            {
                NameOfAnimal = nameOfAnimal,
                FirstNameOfAnimal = firstNameOfAnimal,
                YearOfBirth = yearOfBirth,
                GenderOfAnimal = genderOfAnimal
            };
        }

        public string GetGender()
        {
            if (genderOfAnimal) return "жіноча";
            else return "чоловіча";
        }
    }
}

