using System;
using PizzaAppMaui.Models;
using PizzaAppMaui.Data;
using System.Collections.ObjectModel;
using System.Windows.Input;
using System.Linq;

namespace PizzaAppMaui
{
    public partial class OrdersPage : ContentPage
    {
        public ObservableCollection<Pizza> Pizzas { get; set; }
        public ICommand CreateOrderCommand { get; private set; }

        public OrdersPage(Order order = null)
        {
            InitializeComponent();
            Pizzas = new ObservableCollection<Pizza>();
            LoadPizzas();
            CreateOrderCommand = new Command(CreateOrder);
            BindingContext = this;
        }

        private async void LoadPizzas()
        {
            var pizzasFromDb = await App.Database.GetPizzasAsync(); // Assuming you have this method in your database context
            foreach (var pizza in pizzasFromDb)
            {
                Pizzas.Add(pizza);
            }
        }

        private async void CreateOrder()
        {
            // Get the user input from the form
            var date = DateTime.Parse(DateEntry.Text); // You might want to validate and format this date
            var street = StreetEntry.Text;
            var number = int.Parse(NumberEntry.Text); // You should validate this input
            var apartment = string.IsNullOrWhiteSpace(ApartmentEntry.Text) ? (int?)null : int.Parse(ApartmentEntry.Text); // This field is optional, so check if it's empty
            var status = StatusEntry.Text;

            // Get the selected pizzas
            var selectedPizzas = Pizzas.Where(p => PizzaSelection.SelectedItems.Contains(p)).ToList();

            // Create a new Order object
            var newOrder = new Order
            {
                Date = date,
                Street = street,
                Number = number,
                Apartment = apartment,
                Status = status,
                PizzaOrders = selectedPizzas.Select(pizza => new PizzaOrder { Pizza = pizza, PizzaId = pizza.Id, FinalPrice = pizza.BasePrice }).ToList() // You might need to adjust this line depending on your PizzaOrder model
            };

            // Save the new Order to the database
            await App.Database.SaveOrderAsync(newOrder);

            await Shell.Current.GoToAsync("///PizzasListPage");
        }
    }
}
