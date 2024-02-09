namespace MyRestaurant.Api.Persistence
{
    public class OrderDbContext
    {
        //Kitchen area lists
        public Queue<string> Fries = new Queue<string>();
        public Queue<string> Grill = new Queue<string>();
        public Queue<string> Salad = new Queue<string>();
        public Queue<string> Drink = new Queue<string>();
        public Queue<string> Desert = new Queue<string>();
    }
}
