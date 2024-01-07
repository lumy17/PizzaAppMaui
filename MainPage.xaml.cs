namespace PizzaAppMaui
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private async void OnNavigateToPizzaPageClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new PizzaPage());
        }
        private async void OnNavigateToIngredientPageClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new IngredientPage());
        }
        private async void OnNavigateToPizzasListPageClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new PizzasListPage());
        }

        private async void OnNavigateToIngredientsListPageClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new IngredientsListPage());
        }
    }
}
