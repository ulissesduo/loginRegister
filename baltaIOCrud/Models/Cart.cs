namespace baltaIOCrud.Models
{
    public class Cart
    {
        public Guid Id { get; set; }
        public List<Products> Items { get; set; }
        public int TotalQuantity { get; set; }
        public decimal TotalPrice { get; set; }
        public string? UserId { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        public Cart()
        {
            Items = new List<Products>();
        }

    }
}
