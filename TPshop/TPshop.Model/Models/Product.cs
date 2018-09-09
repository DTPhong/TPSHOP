namespace TPshop.Model.Models
{
    using Abstract;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("Products")]
    public class Product : Auditable
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        [Required]
        public int CategoryID { get; set; }

        [Required]
        public int SupplierID { get; set; }

        [Required]
        [MaxLength(256)]
        public string Name { get; set; }

        [Required]
        [MaxLength(256)]
        public string Alias { get; set; }

        [MaxLength(256)]
        public string Image { get; set; }

        [Column(TypeName = "xml")]
        public string MoreImage { get; set; }

        [MaxLength(500)]
        public string Description { get; set; }
        
        public string Content { get; set; }

        public decimal? Price { get; set; }

        public decimal? Promotion { get; set; }

        public bool? Status { get; set; }

        public bool? HomeFlag { get; set; }

        public int? ViewCount { get; set; }

        public int Quantity { set; get; }

        [StringLength(100)]
        public string CPU { get; set; }

        [StringLength(100)]
        public string Ram { get; set; }

        [StringLength(100)]
        public string Bus { get; set; }

        [StringLength(100)]
        public string RamMax { get; set; }

        [StringLength(100)]
        public string VGA { get; set; }

        [StringLength(100)]
        public string Storage { get; set; }

        [StringLength(100)]
        public string StorageType { get; set; }

        [StringLength(100)]
        public string Monitor { get; set; }

        [StringLength(100)]
        public string Resolution { get; set; }

        [StringLength(100)]
        public string BatteryCapacity { get; set; }

        [StringLength(100)]
        public string BatteryCell { get; set; }

        [StringLength(100)]
        public string Webcam { get; set; }

        [StringLength(100)]
        public string Size { get; set; }

        [StringLength(100)]
        public string Weight { get; set; }

        [StringLength(100)]
        public string OS { get; set; }

        [StringLength(100)]
        public string Warranty { get; set; }

        [ForeignKey("CategoryID")]
        public virtual Category Category { get; set; }

        public virtual IEnumerable<OrderDetail> OrderDetails { get; set; }

        [ForeignKey("SupplierID")]
        public virtual Supplier Supplier { get; set; }
    }
}