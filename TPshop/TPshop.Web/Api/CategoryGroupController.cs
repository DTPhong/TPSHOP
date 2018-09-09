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
    [RoutePrefix("api/categorygroup")]
    [Authorize]
    public class CategoryGroupController : ApiControllerBase
    {
        private ICategoryGroupService _categoryGroupService;

        public CategoryGroupController(IErrorService errorService, ICategoryGroupService categoryGroupService)
            : base(errorService)
        {
            this._categoryGroupService = categoryGroupService;
        }

        [Route("getbyid/{id:int}")]
        [HttpGet]
        public HttpResponseMessage GetById(HttpRequestMessage request, int id)
        {
            return CreateHttpResponse(request, () =>
            {
                var model = _categoryGroupService.GetById(id);
                var responData = Mapper.Map<CategoryGroup, CategoryGroupViewModel>(model);
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
                var model = _categoryGroupService.GetAll(keyword);

                totalRow = model.Count();
                var query = model.OrderByDescending(x => x.Name).Skip(page * pageSize).Take(pageSize);

                var responData = Mapper.Map<IEnumerable<CategoryGroup>, IEnumerable<CategoryGroupViewModel>>(query);

                var paginationSet = new PaginationSet<CategoryGroupViewModel>()
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
                var model = _categoryGroupService.GetAll();
                var responData = Mapper.Map<IEnumerable<CategoryGroup>, IEnumerable<CategoryGroupViewModel>>(model);
                HttpResponseMessage response = request.CreateResponse(HttpStatusCode.OK, responData);
                return response;
            });
        }

        [Route("create")]
        [HttpPost]
        [AllowAnonymous]
        public HttpResponseMessage Create(HttpRequestMessage request, CategoryGroupViewModel categoryGroupVm)
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
                    var newCategoryGroup = new CategoryGroup();
                    newCategoryGroup.UpdateCategoryGroup(categoryGroupVm);
                    _categoryGroupService.Add(newCategoryGroup);
                    _categoryGroupService.Save();

                    var responseData = Mapper.Map<CategoryGroup, CategoryGroupViewModel>(newCategoryGroup);
                    response = request.CreateResponse(HttpStatusCode.Created, responseData);
                }
                return response;
            });
        }

        [Route("update")]
        [HttpPut]
        [AllowAnonymous]
        public HttpResponseMessage Update(HttpRequestMessage request, CategoryGroupViewModel categoryGroupVm)
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
                    var dbCategoryGroup = _categoryGroupService.GetById(categoryGroupVm.ID);
                    dbCategoryGroup.UpdateCategoryGroup(categoryGroupVm);
                    _categoryGroupService.Update(dbCategoryGroup);
                    _categoryGroupService.Save();

                    var responseData = Mapper.Map<CategoryGroup, CategoryGroupViewModel>(dbCategoryGroup);
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
                    var oldCategoryGroup = _categoryGroupService.Delete(id);
                    _categoryGroupService.Save();

                    var responseData = Mapper.Map<CategoryGroup, CategoryGroupViewModel>(oldCategoryGroup);
                    response = request.CreateResponse(HttpStatusCode.Created, responseData);
                }
                return response;
            });
        }

        [Route("deletemulti")]
        [HttpDelete]
        [AllowAnonymous]
        public HttpResponseMessage DeleteMulti(HttpRequestMessage request, string checkedCategoryGroups)
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
                    var listCategoryGroup = new JavaScriptSerializer().Deserialize<List<int>>(checkedCategoryGroups);
                    foreach (var item in listCategoryGroup)
                    {
                        _categoryGroupService.Delete(item);
                    }
                    _categoryGroupService.Save();

                    response = request.CreateResponse(HttpStatusCode.OK, listCategoryGroup.Count);
                }
                return response;
            });
        }
    }
}