using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Lab4
{
    public partial class MainPage : ContentPage
    {
        private List<CareDTO> careDTOs;

        public MainPage()
        {
            InitializeComponent();
            careDTOs = new List<CareDTO>();
            LoadCareDTOs();
        }

        private void LoadCareDTOs()
        {
            careDTOs = Serializer.LoadFromXml<CareDTO>();
            var careList = careDTOs.Select(dto => new Care(dto)).ToList();
            careListView.ItemsSource = careList;
        }

        private async void OnAddCareClicked(object sender, EventArgs e)
        {
            Application.Current.MainPage = new NavigationPage(new AddEditCarePage(null));
        }

        private async void OnEditCareClicked(object sender, EventArgs e)
        {
            var selectedCare = careListView.SelectedItem as Care;
            if (selectedCare == null)
            {
                await DisplayAlert("Помилка", "Оберіть догляд зі списку для редагування.", "OK");
                return;
            }
            Application.Current.MainPage = new NavigationPage(new AddEditCarePage(selectedCare.ToDTO()));
        }

        private async void OnRemoveCareClicked(object sender, EventArgs e)
        {
            var selectedCare = careListView.SelectedItem as Care;
            if (selectedCare == null)
            {
                await DisplayAlert("Помилка", "Оберіть догляд зі списку для редагування.", "OK");
                return;
            }

            bool confirm = await DisplayAlert("Підтвердження", $"Ви дійсно хочете видалити догляд:\n{selectedCare.ToShortString()}?",
                "Видалити", "Скасувати");

            if (!confirm)
                return;
            var toRemove = careDTOs.FirstOrDefault(c => c.Surname == selectedCare.ToDTO().Surname && c.Date == selectedCare.ToDTO().Date);

            careDTOs.Remove(toRemove);
            Serializer.SaveToXml(careDTOs);
            LoadCareDTOs();
            await DisplayAlert("Успішно", "Догляд видалено.", "OK");
        }
    }
}