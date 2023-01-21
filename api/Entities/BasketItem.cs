using System.ComponentModel.DataAnnotations.Schema;

namespace api.Entities
{
    [Table("BasketItems")]
    public class BasketItem
    {
        public int ID { get; set; }
        public int Quantity { get; set; }

        // Navigation properties
        public int ProductID { get; set; }
        public Product Product { get; set; }

        public int BasketID { get; set; }
        public Basket Basket { get; set; }
    }
}