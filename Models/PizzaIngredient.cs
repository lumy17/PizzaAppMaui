using SQLite;
using SQLiteNetExtensions.Attributes;

namespace PizzaAppMaui.Models
{
    public class PizzaIngredient
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        [ForeignKey(typeof(Pizza))]
        public int PizzaId { get; set; }

        [ManyToOne]
        public Pizza Pizza { get; set; }

        [ForeignKey(typeof(Ingredient))]
        public int IngredientId { get; set; }

        [ManyToOne]
        public Ingredient Ingredient { get; set; }

        public int Quantity { get; set; }
    }
}
