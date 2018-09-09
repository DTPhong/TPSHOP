using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using TPshop.Model.Models;
using TPshop.Service;
using TPshop.Web.Infrastructure.Core;
using TPshop.Web.Models;

namespace TPshop.Web.Api
{
    [RoutePrefix("api/order")]
    //[Authorize]
    public class OrderController : ApiControllerBase
    {
        private IOrderService _orderService;
        private IOrderDetailService _orderDetailService;

        public OrderController(IErrorService errorService, IOrderService orderService, IOrderDetailService orderDetailService)
            : base(errorService)
        {
            this._orderService = orderService;
            this._orderDetailService = orderDetailService;
        }
        [Route("getall")]
        [HttpGet]
        public HttpResponseMessage GetAll(HttpRequestMessage request, string keyword, int page, int pageSize = 20)
        {
            return CreateHttpResponse(request, () =>
            {
                string[] includes = null;
                int totalRow = 0;
                var model = _orderService.GetAll(includes, keyword);
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


                totalRow = model.Count();
                var query = model.OrderByDescending(x => x.CreateData).Skip(page * pageSize).Take(pageSize);

                var responData = Mapper.Map<IEnumerable<Order>, IEnumerable<OrderViewModel>>(query);

                var paginationSet = new PaginationSet<OrderViewModel>()
                {
                    Items = responData,
                    Page = page,
                    TotalCount = totalRow,
                    TotalPages = (int)Math.Ceiling((decimal)totalRow / pageSize)
                };

                HttpResponseMessage response = request.CreateResponse(HttpStatusCode.OK, paginationSet);

                return response;
            });
        }

        [Route("setShipping")]
        [HttpGet]
        public HttpResponseMessage setShipping(HttpRequestMessage request, int id)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                if (!ModelState.IsValid)
                {
                    response = request.CreateResponse(HttpStatusCode.BadRequest, ModelState);
                }
                else
                {
                    var order = _orderService.setShipping(id);
                    response = request.CreateResponse(HttpStatusCode.Created, order);
                }
                return response;
            });
        }

        [Route("setCancel")]
        [HttpGet]
        public HttpResponseMessage setCancel(HttpRequestMessage request, int id)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                if (!ModelState.IsValid)
                {
                    response = request.CreateResponse(HttpStatusCode.BadRequest, ModelState);
                }
                else
                {
                    var order = _orderService.setCancel(id);
                    response = request.CreateResponse(HttpStatusCode.Created, order);
                }
                return response;
            });
        }
    }
}
