using SQLite;
using SQLiteNetExtensions.Attributes;
using System.Collections.Generic;

namespace PizzaAppMaui.Models
{
    public class Pizza
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string PizzaName { get; set; }
        public float BasePrice { get; set; }
        //asa se modeleaza faptul ca o pizza poate fi inclusa in mai multe comenzi(pizzaorders). 
        //in acest caz atributul este optional deoarece o pizza poate sa nu sa fie inclusa intr-o comanda

        [OneToMany]
        public List<PizzaOrder>? PizzaOrders { get; set; }

        [OneToMany]
        public List<PizzaIngredient>? PizzaIngredients { get; set; }
    }
}
