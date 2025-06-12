using System;
using System.Collections.Generic;
using System.Linq;
using Xamarin.Forms;

namespace Lab4
{	
	public partial class ManageOrdersPage : ContentPage
	{
        private CareDTO careDto;
        private List<CareDTO> allCares;

        public ManageOrdersPage(CareDTO careDto)
        {
            InitializeComponent();
            this.careDto = careDto;
            allCares = Serializer.LoadFromXml<CareDTO>();
            LoadOrders();
        }

        private void LoadOrders()
        {
            var orderModels = careDto.Orders.Select(dto => new Order(dto)).ToList();
            orderListView.ItemsSource = orderModels;
        }

        private async void OnAddOrderClicked(object sender, EventArgs e)
        {
            Application.Current.MainPage = new NavigationPage(new AddEditOrderPage(careDto, null));
        }

        private async void OnEditOrderClicked(object sender, EventArgs e)
        {
            var selectedOrder = orderListView.SelectedItem as Order;
            if (selectedOrder == null)
            {
                await DisplayAlert("Помилка", "Оберіть наряд для редагування.", "OK");
                return;
            }

            Application.Current.MainPage = new NavigationPage(new AddEditOrderPage(careDto, selectedOrder.ToDTO()));
        }

        private async void OnRemoveOrderClicked(object sender, EventArgs e)
        {
            var selectedOrder = orderListView.SelectedItem as Order;
            if (selectedOrder == null)
            {
                await DisplayAlert("Помилка", "Оберіть наряд для видалення.", "OK");
                return;
            }

            bool confirm = await DisplayAlert("Підтвердження", $"Видалити {selectedOrder}?", "Так", "Ні");
            if (!confirm) return;

            var toRemove = careDto.Orders.FirstOrDefault(o =>
                o.Animal == selectedOrder.ToDTO().Animal &&
                o.Work == selectedOrder.ToDTO().Work &&
                o.Price == selectedOrder.ToDTO().Price);

            if (toRemove != null)
            {
                careDto.Orders.Remove(toRemove);
                var careToUpdate = allCares.FirstOrDefault(c =>
                    c.Surname == careDto.Surname && c.Date == careDto.Date);

                if (careToUpdate != null)
                {
                    careToUpdate.Orders = careDto.Orders;
                    Serializer.SaveToXml(allCares);
                }

                LoadOrders();
                await DisplayAlert("Успішно", "Наряд видалено.", "OK");
            }
        }

        private async void OnCloseClicked(object sender, EventArgs e)
        {
            Application.Current.MainPage = new NavigationPage(new MainPage());
        }
    }
}