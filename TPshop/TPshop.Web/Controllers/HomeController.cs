using AutoMapper;
using System.Collections.Generic;
using System.Web.Mvc;
using TPshop.Model.Models;
using TPshop.Service;
using TPshop.Web.Models;

namespace TPshop.Web.Controllers
{
    public class HomeController : Controller
    {
        private ICategoryService _categoryService;
        private ICategoryGroupService _categoryGroupService;
        private IProductService _productService;

        public HomeController(ICategoryService categoryService, ICategoryGroupService categoryGroupService, IProductService productService)
        {
            _categoryService = categoryService;
            _categoryGroupService = categoryGroupService;
            _productService = productService;
        }

        [OutputCache(Duration = 60, Location = System.Web.UI.OutputCacheLocation.Client)]
        public ActionResult Index()
        {
            var includes = new string[] { "Category" };
            var homeViewModel = new HomeViewModel();

            var lastestProductModel = _productService.GetLastest(5, includes);
            var topSaleProductModel = _productService.GetTopSale(5, includes);
            var listHotCategoryModel = _categoryService.GetHottestCategory(3);

            var lastestProductViewModel = Mapper.Map<IEnumerable<Product>, IEnumerable<ProductViewModel>>(lastestProductModel);
            var topSaleProductViewModel = Mapper.Map<IEnumerable<Product>, IEnumerable<ProductViewModel>>(topSaleProductModel);
            var listHotCategoryViewModel = Mapper.Map<IEnumerable<Category>, IEnumerable<CategoryViewModel>>(listHotCategoryModel);

            homeViewModel.LastestProducts = lastestProductViewModel;
            homeViewModel.TopSaleProducts = topSaleProductViewModel;
            homeViewModel.ListCategory = listHotCategoryViewModel;

            return View(homeViewModel);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        [ChildActionOnly]
        public ActionResult Footer()
        {
            return PartialView();
        }

        [ChildActionOnly]
        public ActionResult Header()
        {
            return PartialView();
        }

        [ChildActionOnly]
        [OutputCache(Duration = 3600)]
        public ActionResult Navigation()
        {
            var listCategoryGroupModel = _categoryGroupService.GetAll();
            var listCategoryGroupViewModel = Mapper.Map<IEnumerable<CategoryGroup>, IEnumerable<CategoryGroupViewModel>>(listCategoryGroupModel);
            return PartialView(listCategoryGroupViewModel);
        }
    }
}