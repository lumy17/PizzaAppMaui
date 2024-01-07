using PizzaAppMaui.Models;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace PizzaAppMaui;

public partial class OrdersListPage : ContentPage
{
    private ObservableCollection<Order> _orders = new ObservableCollection<Order>();

    public OrdersListPage()
    {
        InitializeComponent();
        ordersListView.ItemsSource = _orders;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await LoadOrders();
    }

    private async Task LoadOrders()
    {
        var orders = await App.Database.GetOrdersAsync();
        _orders.Clear();
        foreach (var order in orders)
        {
            _orders.Add(order);
        }
    }

    private async void OnBackButtonClicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("//MainPage");
    }

    private async void OnDeleteOrderClicked(object sender, EventArgs e)
    {
        if (sender is Button button && button.CommandParameter is Order order)
        {
            bool confirm = await DisplayAlert("Confirm Delete", $"Are you sure you want to delete the order made on {order.Date}?", "Yes", "No");
            if (confirm)
            {
                await App.Database.DeleteOrderAsync(order);

                Device.BeginInvokeOnMainThread(() =>
                {
                    _orders.Remove(order);
                });
            }
        }
    }
}
