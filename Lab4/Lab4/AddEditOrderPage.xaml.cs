using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Xamarin.Forms;

namespace Lab4
{
    public partial class AddEditOrderPage : ContentPage
    {
        private Order order;
        private CareDTO Care;
        private CareDTO targetCare;
        public AddEditOrderPage(CareDTO careDto, OrderDTO orderDto)
        {
            InitializeComponent();
            Care = careDto;
            workPicker.ItemsSource = Enum.GetNames(typeof(Work.TypeOfWork));
            if (orderDto == null)
            {
                Title = "Додати процедуру";
            }
            else
            {
                Title = "Редагувати процедуру";
                order = new Order(orderDto);
                LoadOrderData();
            }
        }

        private void LoadOrderData()
        {
            var animal = order.ToDTO().Animal;
            nameOfAnimalEntry.Text = animal?.NameOfAnimal;
            firstNameOfAnimalEntry.Text = animal?.FirstNameOfAnimal;
            yearOfBirthEntry.Text = animal?.YearOfBirth.ToString();
            genderSwitch.IsToggled = animal?.GenderOfAnimal ?? false;

            var workEnumValue = order.ToDTO().Work;
            var workNames = Enum.GetNames(typeof(Work.TypeOfWork));
            int selectedIndex = Array.IndexOf(workNames, workEnumValue.ToString());
            workPicker.SelectedIndex = selectedIndex;

            priceEntry.Text = order.ToDTO().Price.ToString();
        }

        private bool ValidateObject<T>(T obj, out List<ValidationResult> results)
        {
            var context = new ValidationContext(obj);
            results = new List<ValidationResult>();
            return Validator.TryValidateObject(obj, context, results, true);
        }

        private async void OnSaveClicked(object sender, EventArgs e)
        {
            string nameOfAnimal = nameOfAnimalEntry.Text;
            string firstNameOfAnimal = firstNameOfAnimalEntry.Text;
            if (!int.TryParse(yearOfBirthEntry.Text, out int yearOfBirth))
            {
                await DisplayAlert("Помилка", "Рік народження має бути числом.", "OK");
                return;
            }
            bool genderOfAnimal = genderSwitch.IsToggled;

            var animalDto = new AnimalDTO
            {
                NameOfAnimal = nameOfAnimal,
                FirstNameOfAnimal = firstNameOfAnimal,
                YearOfBirth = yearOfBirth,
                GenderOfAnimal = genderOfAnimal
            };

            if (!ValidateObject(animalDto, out var animalValidationResults))
            {
                string errors = string.Join("\n", animalValidationResults.Select(r => r.ErrorMessage));
                await DisplayAlert("Помилка валідації тварини", errors, "OK");
                return;
            }

            if (workPicker.SelectedIndex < 0)
            {
                await DisplayAlert("Помилка", "Оберіть тип роботи.", "OK");
                return;
            }
            var work = (Work.TypeOfWork)Enum.Parse(typeof(Work.TypeOfWork), workPicker.Items[workPicker.SelectedIndex]);

            if (!int.TryParse(priceEntry.Text, out int price))
            {
                await DisplayAlert("Помилка", "Ціна має бути числом.", "OK");
                return;
            }
            var orderDto = new OrderDTO
            {
                Animal = animalDto,
                Work = work,
                Price = price
            };

            if (!ValidateObject(orderDto, out var orderValidationResults))
            {
                string errors = string.Join("\n", orderValidationResults.Select(r => r.ErrorMessage));
                await DisplayAlert("Помилка валідації процедури", errors, "OK");
                return;
            }

            var allCares = Serializer.LoadFromXml<CareDTO>();
            targetCare = allCares.FirstOrDefault(c =>
                c.Surname == Care.Surname &&
                c.Date == Care.Date);

            if (targetCare != null)
            {
                if (order == null)
                {
                    targetCare.Orders.Add(orderDto);
                }
                else
                {
                    var existingOrder = targetCare.Orders.FirstOrDefault(o =>
                        o.Animal.NameOfAnimal == order.ToDTO().Animal.NameOfAnimal &&
                        o.Animal.FirstNameOfAnimal == order.ToDTO().Animal.FirstNameOfAnimal &&
                        o.Animal.YearOfBirth == order.ToDTO().Animal.YearOfBirth &&
                        o.Animal.GenderOfAnimal == order.ToDTO().Animal.GenderOfAnimal &&
                        o.Work == order.ToDTO().Work &&
                        o.Price == order.ToDTO().Price);

                    if (existingOrder != null)
                    {
                        existingOrder.Animal = orderDto.Animal;
                        existingOrder.Work = orderDto.Work;
                        existingOrder.Price = orderDto.Price;
                    }
                }

                Serializer.SaveToXml(allCares);
                await DisplayAlert("Успішно", "Процедуру збережено.", "OK");
            }

            Application.Current.MainPage = new NavigationPage(new ManageOrdersPage(targetCare));
        }

        private async void OnCloseClicked(object sender, EventArgs e)
        {
            string action = await DisplayActionSheet("Закрити сторінку?", "Скасувати", null,
                "Закрити і зберегти", "Закрити без збереження");

            switch (action)
            {
                case "Закрити і зберегти":
                    OnSaveClicked(sender, e);
                    break;

                case "Закрити без збереження":
                    Application.Current.MainPage = new NavigationPage(new ManageOrdersPage(targetCare));
                    break;

                case "Скасувати":
                default:
                    break;
            }
        }
    }
}