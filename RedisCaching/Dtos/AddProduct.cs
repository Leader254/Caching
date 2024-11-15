namespace RedisCaching.Dtos
{
    public class AddProduct
    {
        public string? Name { get; set; }
        public string? Category { get; set; }
        public int Price { get; set; }
        public int Quantity { get; set; }
    }
}
