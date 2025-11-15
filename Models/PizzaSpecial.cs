namespace BlazingPizza.Models
{
    public class PizzaSpecial
    {
        public int PizzaSpecialId { get; set; }

        // compatibility alias used by pages/controllers that expect .Id
        public int Id
        {
            get => PizzaSpecialId;
            set => PizzaSpecialId = value;
        }

        public string Name { get; set; } = "";
        public string Description { get; set; } = "";
        public decimal Price { get; set; }
    }
}
