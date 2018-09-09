using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;
using TPshop.Data.Infrastructure;
using TPshop.Data.Respositories;
using TPshop.Model.Models;

namespace TPshop.UnitTest.RepositoryTest
{
    [TestClass]
    public class CategoryRepositoryTest
    {
        private IDbFactory dbFactory;
        private ICategoryRepository objRepository;
        private IUnitOfWork unitOfWork;

        private ICategoryGroupRepository categoryGroupObjRepository;
        private IApplicationGroupRepository applicationGroupRepository;
        private IOrderRepository orderRepository;
        private IOrderDetailRepository orderDetailRepository;
        [TestInitialize]
        public void Initialize()
        {
            dbFactory = new DbFactory();
            objRepository = new CategoryRepository(dbFactory);
            unitOfWork = new UnitOfWork(dbFactory);

            applicationGroupRepository = new ApplicationGroupRepository(dbFactory);

            orderRepository = new OrderRepository(dbFactory);
            orderDetailRepository = new OrderDetailRepository(dbFactory);
        }

        [TestMethod]
        public void Category_Repository_Create()
        {
            Category category = new Category();
            category.Name = "test";
            category.CategoryGroupID = 1;
            category.Alias = "test";
            category.Status = true;

            var result = objRepository.Add(category);
            unitOfWork.Commit();

            Assert.IsNotNull(result);
            Assert.AreEqual(8, result.ID);
        }

        [TestMethod]
        public void Category_Repository_GetAll()
        {
            Category category = new Category();
            string[] includes = new string[] { "CategoryGroup", "Product" };
            var result = objRepository.GetAll(includes);
            unitOfWork.Commit();

            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void ApplicationGroup_Repository_GetAll()
        {
            var result = applicationGroupRepository.GetAll();
            unitOfWork.Commit();
            foreach (var item in result)
            {
                Console.WriteLine(item);
            }
            Assert.IsNotNull(result);
            Assert.AreEqual(2, result.Count());
        }

        [TestMethod]
        public void Order_Repository_GetAll()
        {
            string[] includes = new string[] { "OrderDetails" };
            var result = orderRepository.GetAll(includes);
            unitOfWork.Commit();
            Assert.IsNotNull(result);
            Assert.AreEqual(1, result.Count());
        }
    }
}