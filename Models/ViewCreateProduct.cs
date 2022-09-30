namespace TestNTT.Models
{
    public class ViewCreateProduct
    {
        public Product Product { get; set; }
        public Category Category { get; set; }
        public IEnumerable<Category> Categories { get; set; }
        public IEnumerable<Product> Products { get; set; }
    }
}
