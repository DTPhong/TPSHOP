namespace TPshop.Model.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("Suppliers")]
    public partial class Supplier
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        [Required]
        [MaxLength(256)]
        public string Name { get; set; }

        [Required]
        [MaxLength(256)]
        public string SupplierAddress { get; set; }

        [Required]
        [MaxLength(256)]
        public string SupplierEmail { get; set; }

        [Required]
        [MaxLength(256)]
        public string SupplierPhone { get; set; }

        public virtual IEnumerable<Product> Products { get; set; }
    }
}