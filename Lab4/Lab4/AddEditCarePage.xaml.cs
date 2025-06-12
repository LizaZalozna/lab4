using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Xamarin.Forms;

namespace Lab4
{
    public partial class AddEditCarePage : ContentPage
    {
        private Care care;

        public AddEditCarePage(CareDTO careDto)
        {
            InitializeComponent();

            if (careDto == null)
            {
                Title = "Додати догляд";
            }
            else
            {
                Title = "Редагувати догляд";
                this.care = new Care(careDto);
                LoadCareData();
            }
        }

        private void LoadCareData()
        {
            surnameEntry.Text = care.ToDTO().Surname;
            careDatePicker.Date = care.ToDTO().Date;
        }

        private bool ValidateCareDTO(CareDTO careDto, out List<ValidationResult> results)
        {
            var context = new ValidationContext(careDto);
            results = new List<ValidationResult>();
            bool isValid = Validator.TryValidateObject(careDto, context, results, true);
            return isValid;
        }

        private async void OnSaveClicked(object sender, EventArgs e)
        {
            string surname = surnameEntry.Text;
            DateTime date = careDatePicker.Date;

            if (string.IsNullOrWhiteSpace(surname))
            {
                await DisplayAlert("Помилка", "Поле прізвища не може бути порожнім.", "OK");
                return;
            }

            var newCare = new CareDTO
            {
                Surname = surname,
                Date = date,
                Orders = care?.ToDTO().Orders ?? new List<OrderDTO>() 
            };

            if (!ValidateCareDTO(newCare, out var validationResults))
            {
                string errors = string.Join("\n", validationResults.Select(r => r.ErrorMessage));
                await DisplayAlert("Помилка валідації", errors, "OK");
                return;
            }

            if (care != null)
            {
                var oldCareDto = care.ToDTO();

                bool isSame = oldCareDto.Surname == newCare.Surname
                              && oldCareDto.Date == newCare.Date;

                if (isSame)
                {
                    await DisplayAlert("Інформація не змінена", "Збереження не потрібне.", "OK");
                    return;
                }
            }

            var careList = Serializer.LoadFromXml<CareDTO>();

            if (care == null)
            {
                careList.Add(newCare);
            }
            else
            {
                var existing = careList.FirstOrDefault(c =>
                    c.Surname == care.ToDTO().Surname && c.Date == care.ToDTO().Date);
                if (existing != null)
                {
                    existing.Surname = newCare.Surname;
                    existing.Date = newCare.Date;
                    existing.Orders = newCare.Orders;
                }
            }

            Serializer.SaveToXml(careList);

            await DisplayAlert("Успішно", "Інформацію збережено.", "OK");
            Application.Current.MainPage = new NavigationPage(new MainPage());
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
                    Application.Current.MainPage = new NavigationPage(new MainPage());
                    break;

                case "Скасувати":
                default:
                    break;
            }
        }
    }
}