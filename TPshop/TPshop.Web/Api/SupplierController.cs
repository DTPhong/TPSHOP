using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Script.Serialization;
using TPshop.Model.Models;
using TPshop.Service;
using TPshop.Web.Infrastructure.Core;
using TPshop.Web.Infrastructure.Extensions;
using TPshop.Web.Models;

namespace TPshop.Web.Api
{
    [RoutePrefix("api/supplier")]
    [Authorize]
    public class SupplierController : ApiControllerBase
    {
        private ISupplierService _supplierService;
        public SupplierController(IErrorService errorService, ISupplierService supplierService)
            : base(errorService)
        {
            this._supplierService = supplierService;
        }

        [Route("getbyid/{id:int}")]
        [HttpGet]
        public HttpResponseMessage GetById(HttpRequestMessage request, int id)
        {
            return CreateHttpResponse(request, () =>
            {
                var model = _supplierService.GetById(id);
                var responData = Mapper.Map<Supplier, SupplierViewModel>(model);
                HttpResponseMessage response = request.CreateResponse(HttpStatusCode.OK, responData);
                return response;
            });
        }

        [Route("getall")]
        [HttpGet]
        public HttpResponseMessage GetAll(HttpRequestMessage request, string keyword, int page, int pageSize = 20)
        {
            return CreateHttpResponse(request, () =>
            {
                int totalRow = 0;
                var model = _supplierService.GetAll(keyword);

                totalRow = model.Count();
                var query = model.OrderByDescending(x => x.Name).Skip(page * pageSize).Take(pageSize);

                var responData = Mapper.Map<IEnumerable<Supplier>, IEnumerable<SupplierViewModel>>(query);

                var paginationSet = new PaginationSet<SupplierViewModel>()
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

        [Route("getallnopaging")]
        [HttpGet]
        public HttpResponseMessage GetAll(HttpRequestMessage request)
        {
            return CreateHttpResponse(request, () =>
            {
                var model = _supplierService.GetAll();
                var responData = Mapper.Map<IEnumerable<Supplier>, IEnumerable<SupplierViewModel>>(model);
                HttpResponseMessage response = request.CreateResponse(HttpStatusCode.OK, responData);
                return response;
            });
        }

        [Route("create")]
        [HttpPost]
        [AllowAnonymous]
        public HttpResponseMessage Create(HttpRequestMessage request, SupplierViewModel supplierVm)
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
                    var newSupplier = new Supplier();
                    newSupplier.UpdateSupplier(supplierVm);
                    _supplierService.Add(newSupplier);
                    _supplierService.Save();

                    var responseData = Mapper.Map<Supplier, SupplierViewModel>(newSupplier);
                    response = request.CreateResponse(HttpStatusCode.Created, responseData);
                }
                return response;
            });
        }

        [Route("update")]
        [HttpPut]
        [AllowAnonymous]
        public HttpResponseMessage Update(HttpRequestMessage request, SupplierViewModel supplierVm)
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
                    var dbSupplier = _supplierService.GetById(supplierVm.ID);
                    dbSupplier.UpdateSupplier(supplierVm);
                    _supplierService.Update(dbSupplier);
                    _supplierService.Save();

                    var responseData = Mapper.Map<Supplier, SupplierViewModel>(dbSupplier);
                    response = request.CreateResponse(HttpStatusCode.Created, responseData);
                }
                return response;
            });
        }

        [Route("delete")]
        [HttpDelete]
        [AllowAnonymous]
        public HttpResponseMessage Delete(HttpRequestMessage request, int id)
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
                    var oldSupplier = _supplierService.Delete(id);
                    _supplierService.Save();

                    var responseData = Mapper.Map<Supplier, SupplierViewModel>(oldSupplier);
                    response = request.CreateResponse(HttpStatusCode.Created, responseData);
                }
                return response;
            });
        }

        [Route("deletemulti")]
        [HttpDelete]
        [AllowAnonymous]
        public HttpResponseMessage DeleteMulti(HttpRequestMessage request, string checkedSuppliers)
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
                    var listSupplier = new JavaScriptSerializer().Deserialize<List<int>>(checkedSuppliers);
                    foreach (var item in listSupplier)
                    {
                        _supplierService.Delete(item);
                    }
                    _supplierService.Save();

                    response = request.CreateResponse(HttpStatusCode.OK, listSupplier.Count);
                }
                return response;
            });
        }
    }
}