using System.Collections.Generic;

namespace TPshop.Web.Models
{
    public class SupplierViewModel
    {
        public int ID { get; set; }

        public string Name { get; set; }
        public string SupplierAddress { get; set; }
        public string SupplierEmail { get; set; }
        public string SupplierPhone { get; set; }

        public virtual IEnumerable<ProductViewModel> Products { get; set; }
    }
}