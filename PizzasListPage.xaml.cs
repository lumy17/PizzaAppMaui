using PizzaAppMaui.Models;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace PizzaAppMaui;

public partial class PizzasListPage : ContentPage
{
    private ObservableCollection<Pizza> _pizzas = new ObservableCollection<Pizza>();

    public PizzasListPage()
    {
        InitializeComponent();
        pizzasListView.ItemsSource = _pizzas; // Set the ObservableCollection as the ItemSource
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await LoadPizzas();
    }

    private async void OnBackButtonClicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("//MainPage");
    }

    private async void OnDeletePizzaClicked(object sender, EventArgs e)
    {
        if (sender is Button button && button.CommandParameter is Pizza pizza)
        {
            bool confirm = await DisplayAlert("Confirm Delete", $"Are you sure you want to delete {pizza.PizzaName}?", "Yes", "No");
            if (confirm)
            {
                // Delete associated PizzaIngredient records and the pizza
                await App.Database.DeletePizzaAsync(pizza);

                // Update the UI on the main thread
                Device.BeginInvokeOnMainThread(() =>
                {
                    _pizzas.Remove(pizza);
                });
            }
        }
    }

    private async Task LoadPizzas()
    {
        var pizzas = await App.Database.GetPizzasWithIngredientsAsync();
        _pizzas.Clear();
        foreach (var pizza in pizzas)
        {
            _pizzas.Add(pizza);
        }
    }
}
