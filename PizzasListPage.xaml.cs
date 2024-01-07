namespace PizzaAppMaui;

public partial class PizzasListPage : ContentPage
{
    public PizzasListPage()
    {
        InitializeComponent();
        NavigationPage.SetHasNavigationBar(this, true);
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        pizzasListView.ItemsSource = await App.Database.GetPizzasWithIngredientsAsync(); // This method should fetch pizzas with their ingredients
    }
    private async void OnBackButtonClicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync(".."); // Navigate back
    }

}
