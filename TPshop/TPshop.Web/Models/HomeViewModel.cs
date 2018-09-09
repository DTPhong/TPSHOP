using System.Collections.Generic;

namespace TPshop.Web.Models
{
    public class HomeViewModel
    {
        public IEnumerable<ProductViewModel> LastestProducts { set; get; }
        public IEnumerable<ProductViewModel> TopSaleProducts { set; get; }
        public IEnumerable<ProductViewModel> HotProducts { set; get; }
        public IEnumerable<CategoryViewModel> ListCategory { set; get; }
        public IEnumerable<CategoryGroupViewModel> ListCategoryGroup { set; get; }

        public string Title { set; get; }
    }
}