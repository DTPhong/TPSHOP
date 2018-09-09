namespace TPshop.Web.Models
{
    public class OrderDetailViewModel
    {
        public int OrderID { get; set; }
        
        public int ProductID { get; set; }

        public int? Quantity { get; set; }

        public decimal? Price { get; set; }

        public decimal? Promotion { get; set; }
        
        public virtual OrderViewModel Order { get; set; }
        
        public virtual ProductViewModel Product { get; set; }
    }
}