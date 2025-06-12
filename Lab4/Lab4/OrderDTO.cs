using System;
using System.ComponentModel.DataAnnotations;

namespace Lab4
{
	public class OrderDTO
	{
        public Work.TypeOfWork Work { get; set; }

        public AnimalDTO Animal { get; set; }

        [Range(100, 1500)]
        public int Price { get; set; }
    }
}