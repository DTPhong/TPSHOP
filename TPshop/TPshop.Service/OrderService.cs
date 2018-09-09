using System;
using System.Collections.Generic;
using TPshop.Data.Infrastructure;
using TPshop.Data.Respositories;
using TPshop.Model.Models;

namespace TPshop.Service
{
    public interface IOrderService
    {
        bool Add(Order order, List<OrderDetail> orderDetails);

        Order Delete(int id);

        void Update(Order order);

        IEnumerable<Order> GetOrderByCustomerId(string id);

        IEnumerable<Order> GetAll(string[] includes);

        IEnumerable<Order> GetAll(string[] includes, string keyword);

        bool setShipping(int id);
        bool setCancel(int id);
    }

    public class OrderService : IOrderService
    {
        private IOrderRepository _orderRepository;
        private IOrderDetailRepository _orderDetailRepository;
        private IUnitOfWork _unitOfWork;

        public OrderService(IOrderRepository orderRepository, IUnitOfWork unitOfWork, IOrderDetailRepository orderDetailRepository)
        {
            this._orderRepository = orderRepository;
            this._orderDetailRepository = orderDetailRepository;
            this._unitOfWork = unitOfWork;
        }

        public bool Add(Order order, List<OrderDetail> orderDetails)
        {
            try
            {
                _orderRepository.Add(order);
                _unitOfWork.Commit();
                foreach (var item in orderDetails)
                {
                    item.OrderID = order.ID;
                    _orderDetailRepository.Add(item);
                }
                _unitOfWork.Commit();
                return true;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public Order Delete(int id)
        {
            return _orderRepository.Delete(id);
        }

        public IEnumerable<Order> GetAll(string[] includes)
        {
            return _orderRepository.GetAll(includes);
        }

        public IEnumerable<Order> GetAll(string[] includes, string keyword)
        {
            if (!string.IsNullOrEmpty(keyword))
            {
                return _orderRepository.GetMulti(x => x.CustomerName.Contains(keyword), includes);
            }
            else
            {
                return _orderRepository.GetAll(includes);
            }
        }

        public void Save()
        {
            _unitOfWork.Commit();
        }

        public bool setShipping(int id)
        {
            try
            {
                var order = _orderRepository.GetSingleById(id);
                order.Status = true;
                _orderRepository.Update(order);
                return true;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool setCancel(int id)
        {
            try
            {
                var order = _orderRepository.GetSingleById(id);
                order.Status = false;
                _orderRepository.Update(order);
                return true;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public void Update(Order order)
        {
            _orderRepository.Update(order);
        }

        public IEnumerable<Order> GetOrderByCustomerId(string id)
        {
            return _orderRepository.GetMulti(x => x.CustomerID==id);
        }
    }
}