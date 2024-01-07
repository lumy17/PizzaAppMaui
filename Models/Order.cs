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

        [ForeignKey(typeof(Cupon))]
        public int? CuponId { get; set; }

        [ManyToOne]
        public Cupon Cupon { get; set; }

        [ForeignKey(typeof(Member))]
        public int MemberId { get; set; }

        [ManyToOne]
        public Member Member { get; set; }

        public float CalculateFinalPrice()
        {
            float finalPrice = 0;
            foreach(var pizzaOrder in PizzaOrders)
            {
                finalPrice += pizzaOrder.Pizza.BasePrice;
                if(pizzaOrder.Pizza.PizzaIngredients != null)
                {
                    foreach(var ingredient in pizzaOrder.Pizza.PizzaIngredients)
                    {
                        finalPrice += ingredient.Ingredient.Price;
                    }
                }
            }
            if (Cupon != null && Cupon.StartDate <= DateTime.Now && Cupon.EndDate >= DateTime.Now)
            {
                finalPrice = finalPrice - Cupon.Value * finalPrice;
            }

            return finalPrice;
        }
    }
}
