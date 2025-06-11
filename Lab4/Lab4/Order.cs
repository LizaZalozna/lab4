using System;
namespace Lab4
{
	public class Order
	{
		Work.TypeOfWork work;
		Animal animal;
		int price;

        public Order(Work.TypeOfWork work, Animal animal, int price)
        {
            this.work = work;
            this.animal = animal;
            this.price = price;
        }

        public Order(OrderDTO dto)
        {
            this.work = dto.Work;
            this.animal = dto.Animal;
            this.price = dto.Price;
        }

        public OrderDTO ToDTO()
        {
            return new OrderDTO
            {
                Work = work,
                Animal = animal,
                Price = price
            };
        }

        public override string ToString()
        {
            return $"{animal}\n Тип виконаної роботи:{work}, ціна:{price}.";
        }
    }
}

