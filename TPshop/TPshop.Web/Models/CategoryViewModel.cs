using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TPshop.Web.Models
{
    public class CategoryViewModel
    {
        public int ID { get; set; }

        [Required]
        public int CategoryGroupID { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Alias { get; set; }

        public string Image { get; set; }

        public string Description { get; set; }

        public DateTime? CreatedDate { get; set; }

        [Required]
        public bool? Status { get; set; }

        public bool? Homeflag { get; set; }

        public virtual CategoryGroupViewModel CategoryGroup { get; set; }

        public virtual IEnumerable<ProductViewModel> Products { get; set; }
    }
}