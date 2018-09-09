using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TPshop.Data.Infrastructure;
using TPshop.Data.Respositories;
using TPshop.Model.Models;
using TPshop.Service;

namespace TPshop.UnitTest.ServiceTest
{
    [TestClass]
    public class ErrorServiceTest
    {
        private Mock<IErrorRepository> _mockRepository;
        private Mock<IUnitOfWork> _mockUnitOfWork;
        private IErrorService _errorService;
        private List<Error> _listError;

        [TestInitialize]
        public void Initialize()
        {
            _mockRepository = new Mock<IErrorRepository>();
            _mockUnitOfWork = new Mock<IUnitOfWork>();
            _errorService = new ErrorService(_mockRepository.Object, _mockUnitOfWork.Object);
            _listError = new List<Error>()
            {
                new Error() {ID=1,Message="xxx",StackTrace="aaa",CreatedDate=DateTime.Now },
            };
        }

        [TestMethod]
        public void Error_Service_Create()
        {
            Error error = new Error();
            error.Message = "a";
            error.StackTrace = "b";
            error.CreatedDate = DateTime.Now;

            _mockRepository.Setup(m => m.Add(error)).Returns((Error e) =>
            {
                e.ID = 2;
                return e;
            });

            var result = _errorService.Create(error);

            Assert.IsNotNull(result);
            Assert.AreEqual(2, result.ID);
        }
    }
}

