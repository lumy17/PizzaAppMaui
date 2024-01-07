using PizzaAppMaui.Data;
using PizzaAppMaui.Models;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace PizzaAppMaui.ViewModels
{
    public class OrdersListViewModel : INotifyPropertyChanged
    {
        private PizzaAppMauiDatabase _database;
        public ObservableCollection<Order> Orders { get; private set; }

        public OrdersListViewModel()
        { 
            Orders = new ObservableCollection<Order>();
            LoadOrders();
        }

        private async void LoadOrders()
        {
            var ordersFromDb = await _database.GetOrdersAsync();
            Orders.Clear();
            foreach (var order in ordersFromDb)
            {
                Orders.Add(order);
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
