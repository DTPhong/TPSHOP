using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using TPshop.Common;
using TPshop.Model.Models;
using TPshop.Service;
using TPshop.Web.Infrastructure.Core;
using TPshop.Web.Models;

namespace TPshop.Web.Controllers
{
    public class ProductController : Controller
    {
        private ICategoryService _categoryService;
        private ICategoryGroupService _categoryGroupService;
        private IProductService _productService;
        private ISupplierService _supplierService;

        string[] includes = new string[] { "Category", "Supplier" };
        public ProductController(ICategoryService categoryService, IProductService productService, ICategoryGroupService categoryGroupService, ISupplierService supplierService)
        {
            this._categoryGroupService = categoryGroupService;
            this._productService = productService;
            this._categoryService = categoryService;
            this._supplierService = supplierService;
        }

        // GET: Product
        public ActionResult ProductByCategory(int id, int? brand, int page = 1, string sort = "")
        {
            int pageSize = int.Parse(ConfigHelper.GetByKey("PageSize"));
            int totalRow = 0;
            var productModel = _productService.GetListProductByCategoryIdPaging(id, page, pageSize, sort, out totalRow, includes);
            if (brand.HasValue)
            {
                productModel = productModel.Where(x=>x.SupplierID==brand);
            }

            var productViewModel = Mapper.Map<IEnumerable<Product>, IEnumerable<ProductViewModel>>(productModel);
            int totalPage = (int)Math.Ceiling((double)totalRow / pageSize);

            var category = _categoryService.GetById(id);
            ViewBag.Category = Mapper.Map<Category, CategoryViewModel>(category);
            var categoryGroup = _categoryGroupService.GetById(category.CategoryGroupID);
            ViewBag.CategoryGroup = Mapper.Map<CategoryGroup, CategoryGroupViewModel>(categoryGroup);
            ViewBag.relateProduct = _productService.GetTopSale(3, includes);
            ViewBag.Supplier = _supplierService.GetAll();
            var paginationSet = new PaginationSet<ProductViewModel>()
            {
                Items = productViewModel,
                MaxPage = int.Parse(ConfigHelper.GetByKey("MaxPage")),
                Page = page,
                TotalCount = totalRow,
                TotalPages = totalPage
            };

            return View(paginationSet);
        }

        public ActionResult Search(string keyword, int? brand, int? categoryId, int page = 1, string sort = "")
        {
            int pageSize = int.Parse(ConfigHelper.GetByKey("PageSize"));
            int totalRow = 0;
            var productModel = _productService.Search(keyword, page, pageSize, sort, out totalRow, includes);
            if (categoryId.HasValue)
            {
                productModel = productModel.Where(x => x.CategoryID == categoryId);
            }
            if (brand.HasValue)
            {
                productModel = productModel.Where(x => x.SupplierID == brand);
            }
            var productViewModel = Mapper.Map<IEnumerable<Product>, IEnumerable<ProductViewModel>>(productModel);
            int totalPage = (int)Math.Ceiling((double)totalRow / pageSize);
            ViewBag.relateProduct = _productService.GetTopSale(3, includes);
            ViewBag.Supplier = _supplierService.GetAll();
            ViewBag.Keyword = keyword;
            var paginationSet = new PaginationSet<ProductViewModel>()
            {
                Items = productViewModel,
                MaxPage = int.Parse(ConfigHelper.GetByKey("MaxPage")),
                Page = page,
                TotalCount = totalRow,
                TotalPages = totalPage
            };
            ViewBag.Category = _categoryService.GetAll();
            return View(paginationSet);
        }

        public ActionResult ProductByGroupCategory(int id, int? brand, int page = 1,string sort = "" )
        {
            int pageSize = int.Parse(ConfigHelper.GetByKey("PageSize"));
            int totalRow = 0;
            var productModel = _productService.GetListProductByGroupCategoryIdPaging(id, page, pageSize, sort, out totalRow, includes);
            if (brand.HasValue)
            {
                productModel = productModel.Where(x => x.SupplierID == brand);
            }
            var productViewModel = Mapper.Map<IEnumerable<Product>, IEnumerable<ProductViewModel>>(productModel);
            int totalPage = (int)Math.Ceiling((double)totalRow / pageSize);
            var categoryGroup = _categoryGroupService.GetById(id);
            ViewBag.CategoryGroup = Mapper.Map<CategoryGroup, CategoryGroupViewModel>(categoryGroup);
            var category = _categoryService.GetAll().Where(x => x.CategoryGroupID == id);
            ViewBag.Category = Mapper.Map<IEnumerable<Category>, IEnumerable<CategoryViewModel>>(category);

            ViewBag.Supplier = _supplierService.GetAll();

            ViewBag.relateProduct = _productService.GetTopSale(3, includes);
            var paginationSet = new PaginationSet<ProductViewModel>()
            {
                Items = productViewModel,
                MaxPage = int.Parse(ConfigHelper.GetByKey("MaxPage")),
                Page = page,
                TotalCount = totalRow,
                TotalPages = totalPage
            };

            return View(paginationSet);
        }

        public ActionResult Detail(int id)
        {
            var productModel = _productService.GetById(id);
            var viewModel = Mapper.Map<Product, ProductViewModel>(productModel);
            var relatedProduct = _productService.GetReatedProducts(id, productModel.CategoryID, 4);
            List<string> listImages = new JavaScriptSerializer().Deserialize<List<string>>(viewModel.MoreImage);
            var RelatedProduct = Mapper.Map<IEnumerable<Product>, IEnumerable<ProductViewModel>>(relatedProduct);
            ViewBag.RelatedProduct = RelatedProduct;
            ViewBag.MoreImages = listImages;
            ViewBag.Category = _categoryService.GetById(productModel.CategoryID).Name;
            ViewBag.CategoryGroup = _categoryGroupService.GetById(_categoryService.GetById(productModel.CategoryID).CategoryGroupID).Name;
            return View(viewModel);
        }

        public JsonResult GetListProductByName(string keyword)
        {
            var model = _productService.GetListProductByName(keyword);
            return Json(new
            {
                data = model
            }, JsonRequestBehavior.AllowGet);
        }
    }
}