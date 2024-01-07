using SQLite;
using SQLiteNetExtensions.Attributes;
using System.Collections.Generic;

namespace PizzaAppMaui.Models
{
    public class Order
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public string Street { get; set; }
        public int Number { get; set; }
        public int? Apartment { get; set; }
        public string Status { get; set; }

        [OneToMany(CascadeOperations = CascadeOperation.All)]
        public List<PizzaOrder> PizzaOrders { get; set; }

        public float CalculateFinalPrice()
        {
            float finalPrice = 0;
            if (PizzaOrders != null)
            {
                foreach (var pizzaOrder in PizzaOrders)
                {
                    if (pizzaOrder?.Pizza != null)
                    {
                        finalPrice += pizzaOrder.Pizza.BasePrice;
                        if (pizzaOrder.Pizza.PizzaIngredients != null)
                        {
                            foreach (var pizzaIngredient in pizzaOrder.Pizza.PizzaIngredients)
                            {
                                if (pizzaIngredient?.Ingredient != null)
                                {
                                    finalPrice += pizzaIngredient.Ingredient.Price;
                                }
                            }
                        }
                    }
                }
            }
            return finalPrice;
        }

    }
}
