namespace PizzaAppMaui;

public partial class IngredientsListPage : ContentPage
{
    public IngredientsListPage()
    {
        InitializeComponent();
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        ingredientsListView.ItemsSource = await App.Database.GetIngredientsAsync(); // Load and display ingredients
    }
    private async void OnBackButtonClicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync(".."); // Navigate back
    }

}
