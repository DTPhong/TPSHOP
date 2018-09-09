using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using TPshop.Data.Infrastructure;
using TPshop.Data.Respositories;
using TPshop.Model.Models;

namespace TPshop.UnitTest.RepositoryTest
{
    [TestClass]
    public class ErrorRepositoryTest
    {
        private IDbFactory dbFactory;
        private IErrorRepository objRepository;
        private IUnitOfWork unitOfWork;
        

        [TestInitialize]
        public void Initialize()
        {
            dbFactory = new DbFactory();
            objRepository = new ErrorRepository(dbFactory);
            unitOfWork = new UnitOfWork(dbFactory);
        }

        [TestMethod]
        public void Error_Repository_Create()
        {
            Error error = new Error();
            error.Message = "a";
            error.StackTrace = "b";
            error.CreatedDate = DateTime.Now;

            var result = objRepository.Add(error);
            unitOfWork.Commit();

            Assert.IsNotNull(result);
            Assert.AreEqual(2, result.ID);
        }
    }
}