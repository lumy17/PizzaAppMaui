using SQLite;
using SQLiteNetExtensions.Attributes;
using System.Collections.Generic;

namespace PizzaAppMaui.Models
{
    public class Cupon
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string Type { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public float Value { get; set; }
        public string Code { get; set; }

        //modelam relatia cu orders: un cupon poate sa fie inclus in una sau mai multe comenzi 
        [OneToMany]
        public List<Order>? Order { get; set; }  
    }
}
