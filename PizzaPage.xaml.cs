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
            var pizzaName = pizzaNameEditor.Text;
            var basePrice = float.Parse(basePriceEntry.Text);
            var selectedIngredients = ingredientsCollectionView.SelectedItems.Cast<Ingredient>().ToList();

            var newPizza = new Pizza
            {
                PizzaName = pizzaName,
                BasePrice = basePrice,
                PizzaIngredients = selectedIngredients.Select(ingredient => new PizzaIngredient { IngredientId = ingredient.Id, Quantity = 1 }).ToList() // Set quantity as needed
            };

            await App.Database.SavePizzaAsync(newPizza); // Ensure this method handles saving pizza with ingredients

            await Shell.Current.GoToAsync("///PizzasListPage");
        }
    }

}