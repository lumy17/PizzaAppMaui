using PizzaAppMaui.Models;
using System;

namespace PizzaAppMaui
{
    public partial class IngredientPage : ContentPage
    {
        public IngredientPage()
        {
            InitializeComponent();
        }

        private async void OnSaveButtonClicked(object sender, EventArgs e)
        {
            var ingredientName = ingredientNameEditor.Text;
            var ingredientPrice = float.Parse(ingredientPriceEntry.Text);
            var ingredientStock = int.Parse(ingredientStockEntry.Text);

            var newIngredient = new Ingredient
            {
                Name = ingredientName,
                Price = ingredientPrice,
                Stock = ingredientStock
            };

            await App.Database.SaveIngredientAsync(newIngredient);

            await Shell.Current.GoToAsync("///IngredientsListPage");  // Navigate to IngredientsListPage
        }
    }
}