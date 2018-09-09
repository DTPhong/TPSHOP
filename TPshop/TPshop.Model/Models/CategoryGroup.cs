namespace TPshop.Model.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("CategoryGroups")]
    public class CategoryGroup
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        [Required]
        [StringLength(256)]
        public string Name { get; set; }

        public virtual IEnumerable<Category> Categories { get; set; }
    }
}