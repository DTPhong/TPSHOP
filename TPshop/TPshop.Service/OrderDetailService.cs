using System.Collections.Generic;
using TPshop.Data.Infrastructure;
using TPshop.Data.Respositories;
using TPshop.Model.Models;

namespace TPshop.Service
{
    public interface IOrderDetailService
    {
        OrderDetail Add(OrderDetail orderDetail);

        OrderDetail Detele(int id);

        OrderDetail GetById(int id);

        IEnumerable<OrderDetail> GetAll();
        IEnumerable<OrderDetail> GetAll(string[] includes);

        void Save();
    }

    public class OrderDetailService : IOrderDetailService
    {
        private IOrderDetailRepository _orderDetailRepository;
        private IUnitOfWork _unitOfWork;

        public OrderDetailService(IOrderDetailRepository orderDetailRepository, IUnitOfWork unitOfWork)
        {
            this._orderDetailRepository = orderDetailRepository;
            this._unitOfWork = unitOfWork;
        }

        public OrderDetail Add(OrderDetail orderDetail)
        {
            return _orderDetailRepository.Add(orderDetail);
        }

        public OrderDetail Detele(int id)
        {
            return _orderDetailRepository.Delete(id);
        }

        public IEnumerable<OrderDetail> GetAll()
        {
            return _orderDetailRepository.GetAll();
        }

        public IEnumerable<OrderDetail> GetAll(string[] includes)
        {
            return _orderDetailRepository.GetAll(includes);
        }

        public OrderDetail GetById(int id)
        {
            return _orderDetailRepository.GetSingleById(id);
        }

        public void Save()
        {
            _unitOfWork.Commit();
        }
    }
}