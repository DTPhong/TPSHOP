using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TPshop.Model.Models;
using TPshop.Service;
using TPshop.Web.App_Start;
using TPshop.Web.Infrastructure.Core;
using TPshop.Web.Models;
using Microsoft.AspNet.Identity;

namespace TPshop.Web.Controllers
{
    public class OrderController : Controller
    {
        private IOrderService _orderService;
        private IOrderDetailService _orderDetailService;
        private ApplicationUserManager _userManager;

        public OrderController(IErrorService errorService, IOrderService orderService, IOrderDetailService orderDetailService, ApplicationUserManager userManager)
        {
            this._orderService = orderService;
            this._orderDetailService = orderDetailService;
            this._userManager = userManager;
        }
        // GET: Order
        public ActionResult Index()
        {
            if (User.Identity.IsAuthenticated)
            {
                var model = _orderService.GetOrderByCustomerId(User.Identity.GetUserId());
                string[] includeProduct = { "Product" };
                var orderDetails = _orderDetailService.GetAll(includeProduct);
                foreach (var item in model)
                {
                    List<OrderDetail> listDetail = new List<OrderDetail>();
                    foreach (var itemDetail in orderDetails)
                    {
                        if (item.ID == itemDetail.OrderID)
                        {
                            listDetail.Add(itemDetail);
                        }
                    }
                    item.OrderDetails = listDetail;

                }
                var query = model.OrderByDescending(x => x.CreateData);

                var responData = Mapper.Map<IEnumerable<Order>, IEnumerable<OrderViewModel>>(query);

                return View(responData);
            }
            return View();
        }
    }
}