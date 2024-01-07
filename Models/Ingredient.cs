using SQLite;
using SQLiteNetExtensions.Attributes;
using System.Collections.Generic;

namespace PizzaAppMaui.Models
{
    public class Ingredient
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string Name { get; set; }
        public float Price { get; set; }
        public int Stock { get; set; }
        //Cascade Operations: CascadeOperations help define how changes
        //(like deletions) should propagate in related records.

        [OneToMany]
        public List<PizzaIngredient>? PizzaIngredients { get; set; }
    }
}
