using System.ComponentModel.DataAnnotations;

namespace TestNTT.Models
{
    public class Category
    {
        [Key]
        public Guid CategoryId { get; set; }
        public string CategoryName { get; set; }

        public virtual IEnumerable<Product> Products { get; set; }
    }
}
