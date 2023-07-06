namespace baltaIOCrud.Models
{
    public class Purchase
    {
        public Guid Id { get; set; }
        public string CustomerId { get; set; }
        public Guid ProductId { get; set; }//talvez tenha dado problema aqui mas nao consegui fazer migration
        public string ProductName { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }


    }
}
