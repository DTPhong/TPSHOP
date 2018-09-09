namespace TPshop.Web.Models
{
    using System.Collections.Generic;

    public class CategoryGroupViewModel
    {
        public int ID { get; set; }

        public string Name { get; set; }

        public virtual IEnumerable<CategoryViewModel> Categories { get; set; }
    }
}