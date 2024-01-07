using SQLite;
using System.Collections.Generic;
using System.Threading.Tasks;
using PizzaAppMaui.Models; // Ensure this namespace matches your models' namespace
namespace PizzaAppMaui.Data
{
    public class PizzaAppMauiDatabase
    {
        readonly SQLiteAsyncConnection _database;

        public PizzaAppMauiDatabase(string dbPath)
        {
            _database = new SQLiteAsyncConnection(dbPath);
            _database.CreateTableAsync<Cupon>().Wait();
            _database.CreateTableAsync<Ingredient>().Wait();
            _database.CreateTableAsync<Member>().Wait();
            _database.CreateTableAsync<Order>().Wait();
            _database.CreateTableAsync<Pizza>().Wait();
            _database.CreateTableAsync<PizzaIngredient>().Wait();
            _database.CreateTableAsync<PizzaOrder>().Wait();
        }

        // Cupon methods
        public Task<List<Cupon>> GetCuponsAsync() => _database.Table<Cupon>().ToListAsync();
        public Task<Cupon> GetCuponAsync(int id) => _database.Table<Cupon>().Where(i => i.Id == id).FirstOrDefaultAsync();
        public Task<int> SaveCuponAsync(Cupon cupon) => cupon.Id != 0 ? _database.UpdateAsync(cupon) : _database.InsertAsync(cupon);
        public Task<int> DeleteCuponAsync(Cupon cupon) => _database.DeleteAsync(cupon);

        // Ingredient methods
        public Task<List<Ingredient>> GetIngredientsAsync() => _database.Table<Ingredient>().ToListAsync();
        public Task<Ingredient> GetIngredientAsync(int id) => _database.Table<Ingredient>().Where(i => i.Id == id).FirstOrDefaultAsync();
        public Task<int> SaveIngredientAsync(Ingredient ingredient) => ingredient.Id != 0 ? _database.UpdateAsync(ingredient) : _database.InsertAsync(ingredient);
        public Task<int> DeleteIngredientAsync(Ingredient ingredient) => _database.DeleteAsync(ingredient);

        // Member methods
        public Task<List<Member>> GetMembersAsync() => _database.Table<Member>().ToListAsync();
        public Task<Member> GetMemberAsync(int id) => _database.Table<Member>().Where(i => i.Id == id).FirstOrDefaultAsync();
        public Task<int> SaveMemberAsync(Member member) => member.Id != 0 ? _database.UpdateAsync(member) : _database.InsertAsync(member);
        public Task<int> DeleteMemberAsync(Member member) => _database.DeleteAsync(member);

        // Order methods
        public Task<List<Order>> GetOrdersAsync() => _database.Table<Order>().ToListAsync();
        public Task<Order> GetOrderAsync(int id) => _database.Table<Order>().Where(i => i.Id == id).FirstOrDefaultAsync();
        public Task<int> SaveOrderAsync(Order order) => order.Id != 0 ? _database.UpdateAsync(order) : _database.InsertAsync(order);
        public Task<int> DeleteOrderAsync(Order order) => _database.DeleteAsync(order);

        // Pizza methods
        public Task<List<Pizza>> GetPizzasAsync() => _database.Table<Pizza>().ToListAsync();
        public Task<Pizza> GetPizzaAsync(int id) => _database.Table<Pizza>().Where(i => i.Id == id).FirstOrDefaultAsync();
        public Task<int> SavePizzaAsync(Pizza pizza) => pizza.Id != 0 ? _database.UpdateAsync(pizza) : _database.InsertAsync(pizza);
        public Task<int> DeletePizzaAsync(Pizza pizza) => _database.DeleteAsync(pizza);

        // PizzaIngredient methods
        public Task<List<PizzaIngredient>> GetPizzaIngredientsAsync() => _database.Table<PizzaIngredient>().ToListAsync();
        public Task<PizzaIngredient> GetPizzaIngredientAsync(int id) => _database.Table<PizzaIngredient>().Where(i => i.Id == id).FirstOrDefaultAsync();
        public Task<int> SavePizzaIngredientAsync(PizzaIngredient pizzaIngredient) => pizzaIngredient.Id != 0 ? _database.UpdateAsync(pizzaIngredient) : _database.InsertAsync(pizzaIngredient);
        public Task<int> DeletePizzaIngredientAsync(PizzaIngredient pizzaIngredient) => _database.DeleteAsync(pizzaIngredient);

        // PizzaOrder methods
        public Task<List<PizzaOrder>> GetPizzaOrdersAsync() => _database.Table<PizzaOrder>().ToListAsync();
        public Task<PizzaOrder> GetPizzaOrderAsync(int id) => _database.Table<PizzaOrder>().Where(i => i.Id == id).FirstOrDefaultAsync();
        public Task<int> SavePizzaOrderAsync(PizzaOrder pizzaOrder) => pizzaOrder.Id != 0 ? _database.UpdateAsync(pizzaOrder) : _database.InsertAsync(pizzaOrder);
        public Task<int> DeletePizzaOrderAsync(PizzaOrder pizzaOrder) => _database.DeleteAsync(pizzaOrder);
        public async Task<List<Pizza>> GetPizzasWithIngredientsAsync()
        {
            var pizzas = await _database.Table<Pizza>().ToListAsync();
            foreach (var pizza in pizzas)
            {
                pizza.PizzaIngredients = await _database.Table<PizzaIngredient>()
                                                       .Where(pi => pi.PizzaId == pizza.Id)
                                                       .ToListAsync();
            }
            return pizzas;
        }

    }
}
