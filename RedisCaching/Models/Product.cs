
namespace RedisCaching.Models
{
    public class Product
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public string? Category { get; set; }
        public int Price { get; set; }
        public int Quantity { get; set; }
    }
}
