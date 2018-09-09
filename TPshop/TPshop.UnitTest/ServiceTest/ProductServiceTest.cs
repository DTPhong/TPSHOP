using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;
using TPshop.Data.Infrastructure;
using TPshop.Data.Respositories;
using TPshop.Model.Models;
using TPshop.Service;

namespace TPshop.UnitTest.ServiceTest
{
    [TestClass]
    public class ProductServiceTest
    {
        private Mock<IProductRepository> _mockRepository;
        private Mock<IUnitOfWork> _mockUnitOfWork;
        private IProductService _productService;
        private List<Product> _listProduct;

        [TestInitialize]
        public void Initialize()
        {
            _mockRepository = new Mock<IProductRepository>();
            _mockUnitOfWork = new Mock<IUnitOfWork>();
            _productService = new ProductService(_mockRepository.Object, _mockUnitOfWork.Object);
            _listProduct = new List<Product>()
            {
                new Product() {ID=1, Name="Product1", Status=true },
                new Product() {ID=2, Name="Product2", Status=true },
                new Product() {ID=3, Name="Product3", Status=true },
                new Product() {ID=4, Name="Product4", Status=true },
            };
        }

        [TestMethod]
        public void Product_Service_GetAll()
        {
            //setup method
            _mockRepository.Setup(m => m.GetAll(null)).Returns(_listProduct);

            //call action
            var result = _productService.GetAll() as List<Product>;

            //compare
            Assert.IsNotNull(result);
            Assert.AreEqual(4, result.Count);
        }

        //[TestMethod]
        //public void Product_Service_GetRelatedProduct()
        //{
        //    //setup method
        //    _mockRepository.Setup(m => m.GetAll(null)).Returns(_listProduct);

        //    //call action
        //    var result = _productService.GetReatedProducts(22,4) as List<Product>;

        //    //compare
        //    Assert.IsNotNull(result);
        //    Assert.AreEqual(4, result.Count);
        //}

        [TestMethod]
        public void Product_Service_Create()
        {
            Product product = new Product();
            product.Name = "Test";
            product.Alias = "test";
            product.Status = true;

            _mockRepository.Setup(m => m.Add(product)).Returns((Product p) =>
              {
                  p.ID = 1;
                  return p;
              });

            var result = _productService.Add(product);

            Assert.IsNotNull(result);
            Assert.AreEqual(1, result.ID);
        }
    }
}