using System;
using PizzaAppMaui.Models;
using PizzaAppMaui.Data;
using System.Collections.ObjectModel;

namespace PizzaAppMaui
{
    public partial class PizzaPage : ContentPage
    {
        public ObservableCollection<Ingredient> AvailableIngredients { get; set; }

        public PizzaPage()
        {
            InitializeComponent();
            AvailableIngredients = new ObservableCollection<Ingredient>();
            ingredientsCollectionView.ItemsSource = AvailableIngredients;
            LoadIngredients();
        }

        private async void LoadIngredients()
        {
            var ingredientsFromDb = await App.Database.GetIngredientsAsync(); // Assuming you have this method in your database context
            foreach (var ingredient in ingredientsFromDb)
            {
                AvailableIngredients.Add(ingredient);
            }
        }

        private async void OnSaveButtonClicked(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(pizzaNameEditor.Text) || string.IsNullOrWhiteSpace(basePriceEntry.Text))
                {
                    await DisplayAlert("Error", "Please enter a pizza name and base price.", "OK");
                    return;
                }

                var pizzaName = pizzaNameEditor.Text;
                if (!float.TryParse(basePriceEntry.Text, out var basePrice))
                {
                    await DisplayAlert("Error", "Invalid base price.", "OK");
                    return;
                }

                var selectedIngredients = ingredientsCollectionView.SelectedItems.Cast<Ingredient>().ToList();

                var newPizza = new Pizza
                {
                    PizzaName = pizzaName,
                    BasePrice = basePrice,
                    PizzaIngredients = selectedIngredients.Select(ingredient => new PizzaIngredient { IngredientId = ingredient.Id, Quantity = 1 }).ToList() // Adjust quantity as needed
                };

                await App.Database.SavePizzaAsync(newPizza); // Make sure this method is implemented correctly

                await Shell.Current.GoToAsync("///PizzasListPage");
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", $"An error occurred: {ex.Message}", "OK");
            }
        }

    }

}