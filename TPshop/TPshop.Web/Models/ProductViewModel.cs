using System;
using System.Collections.Generic;

namespace TPshop.Web.Models
{
    //[Serializable]
    public class ProductViewModel
    {
        public int ID { get; set; }

        public int CategoryID { get; set; }

        public int SupplierID { get; set; }

        public string Name { get; set; }

        public string Alias { get; set; }

        public string Image { get; set; }

        public string MoreImage { get; set; }

        public string Description { get; set; }

        public string Content { get; set; }

        public decimal? Price { get; set; }

        public decimal? Promotion { get; set; }

        public DateTime? CreatedDate { get; set; }

        public string CreatedBy { get; set; }

        public DateTime? UpdatedDate { get; set; }

        public string UpdatedBy { get; set; }

        public bool? Status { get; set; }

        public bool? HomeFlag { get; set; }

        public int? ViewCount { get; set; }

        public int Quantity { set; get; }

        public int ProductID { get; set; }

        public string CPU { get; set; }

        public string Ram { get; set; }

        public string Bus { get; set; }

        public string RamMax { get; set; }

        public string VGA { get; set; }

        public string Storage { get; set; }

        public string StorageType { get; set; }

        public string Monitor { get; set; }

        public string Resolution { get; set; }

        public string BatteryCapacity { get; set; }

        public string BatteryCell { get; set; }

        public string Webcam { get; set; }

        public string Size { get; set; }

        public string Weight { get; set; }

        public string OS { get; set; }

        public string Warranty { get; set; }

        public virtual CategoryViewModel Category { get; set; }

        public virtual IEnumerable<OrderDetailViewModel> OrderDetails { get; set; }
        

        public virtual SupplierViewModel Supplier { get; set; }
    }
}