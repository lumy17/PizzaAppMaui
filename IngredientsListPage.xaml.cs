using PizzaAppMaui.Models;
using System;
using System.Collections.ObjectModel;


namespace PizzaAppMaui;

public partial class IngredientsListPage : ContentPage
{
    // ObservableCollection for ingredients
    private ObservableCollection<Ingredient> _ingredients = new ObservableCollection<Ingredient>();

    public IngredientsListPage()
    {
        InitializeComponent();
        ingredientsListView.ItemsSource = _ingredients; // Set the ObservableCollection as the ItemSource
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        var ingredients = await App.Database.GetIngredientsAsync();
        _ingredients.Clear();
        foreach (var ingredient in ingredients)
        {
            _ingredients.Add(ingredient); // Add each ingredient to the ObservableCollection
        }
    }

    private async void OnBackButtonClicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("//MainPage");
    }

    private async void OnDeleteAllIngredientsClicked(object sender, EventArgs e)
    {
        await App.Database.DeleteAllIngredientsAsync();
        _ingredients.Clear(); // Clear the ObservableCollection
        // Optionally, refresh the UI or navigate as needed
    }

    private async void OnDeleteIngredientClicked(object sender, EventArgs e)
    {
        try
        {
            if (sender is Button button && button.CommandParameter is Ingredient ingredient)
            {
                bool confirm = await DisplayAlert("Confirm Delete", $"Are you sure you want to delete {ingredient.Name}?", "Yes", "No");
                if (confirm)
                {
                    await App.Database.DeleteIngredientAsync(ingredient);

                    Device.BeginInvokeOnMainThread(async () =>
                    {
                        _ingredients.Remove(ingredient);
                    });
                }

            }
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error", $"An error occurred: {ex.Message}", "OK");
        }
    }

    // ... Any other methods or event handlers you may have ...
}
