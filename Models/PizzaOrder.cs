using SQLite;
using SQLiteNetExtensions.Attributes;

namespace PizzaAppMaui.Models
{
    public class PizzaOrder
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        [ForeignKey(typeof(Order))]
        public int OrderId { get; set; }

        [ManyToOne]
        public Order Order { get; set; }

        [ForeignKey(typeof(Pizza))]
        public int PizzaId { get; set; }

        [ManyToOne]
        public Pizza Pizza { get; set; }

        public float FinalPrice { get; set; }
    }
}
