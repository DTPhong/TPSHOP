using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using TPshop.Data.Infrastructure;
using TPshop.Data.Respositories;
using TPshop.Model.Models;

namespace TPshop.UnitTest.RepositoryTest
{
    [TestClass]
    public class ProductRepositoryTest
    {
        private IDbFactory dbFactory;
        private IProductRepository objRepository;
        private IUnitOfWork unitOfWork;

        [TestInitialize]
        public void Initialize()
        {
            dbFactory = new DbFactory();
            objRepository = new ProductRepository(dbFactory);
            unitOfWork = new UnitOfWork(dbFactory);
        }

        [TestMethod]
        public void Product_Repository_Create()
        {
            Product product = new Product();
            product.CategoryID = 4;
            product.SupplierID = 3;
            product.Name = "Test Product";
            product.Alias = "Test-Product";
            product.Status = true;

            var result = objRepository.Add(product);
            unitOfWork.Commit();

            Assert.IsNotNull(result);
            Assert.AreEqual(7, result.ID);
        }

        [TestMethod]
        public void Product_Repository_GetAll()
        {
            var list = objRepository.GetAll().ToList();
            Assert.AreEqual(1, list.Count);
        }

        [TestMethod]
        public void Product_Repository_Delete()
        {
            var id = 4;
            var result = objRepository.Delete(id);
            unitOfWork.Commit();
            Assert.IsNotNull(result);
            Assert.AreEqual(4, result.ID);
        }

        [TestMethod]
        public void Product_Repository_Update()
        {
            Product product = new Product();
            product.ID = 5;
            product.CategoryID = 3;
            product.SupplierID = 3;
            product.Name = "Test Product5";
            product.Alias = "Test-Product";
            product.Status = true;

            objRepository.Update(product);
            unitOfWork.Commit();

            Assert.AreEqual(5, product.ID);
        }
    }
}