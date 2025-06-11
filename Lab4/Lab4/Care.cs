using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Lab4
{
	public class Care
	{
		string surname;
		DateTime date;
		List<Order> orders;

        public Care(string surname, DateTime date, List<Order> orders)
        {
            this.surname = surname;
            this.date = date;
            this.orders = orders;
        }

        public Care(CareDTO dto)
        {
            this.surname = dto.Surname;
            this.date = dto.Date;
            this.orders = dto.Orders;
        }

        public CareDTO ToDTO()
        {
            return new CareDTO
            {
                Surname = surname,
                Date = date,
                Orders = orders
            };
        }

        public override string ToString()
        {
            string ordersInfo = string.Join("\n", orders.Select(o => o.ToString()));
            return $"Прізвище: {surname}, дата: {date.ToShortDateString()}\nЗамовлення:\n{ordersInfo}";
        }

        public string ToShortString()
        {
            int total = orders.Sum(o => o.price_);
            return $"Прізвище: {surname}, дата: {date.ToShortDateString()}, сума робіт: {total} грн.";
        }
    }
}

