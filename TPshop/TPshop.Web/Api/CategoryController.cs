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
    [RoutePrefix("api/category")]
    [Authorize]
    public class CategoryController : ApiControllerBase
    {
        private ICategoryService _categoryService;

        public CategoryController(IErrorService errorService, ICategoryService categoryService)
            : base(errorService)
        {
            this._categoryService = categoryService;
        }

        [Route("getbyid/{id:int}")]
        [HttpGet]
        public HttpResponseMessage GetById(HttpRequestMessage request, int id)
        {
            return CreateHttpResponse(request, () =>
            {
                var model = _categoryService.GetById(id);
                var responData = Mapper.Map<Category, CategoryViewModel>(model);
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
                string[] includes = { "CategoryGroup" };
                int totalRow = 0;
                var model = _categoryService.GetAll(includes, keyword);

                totalRow = model.Count();
                var query = model.OrderByDescending(x => x.CreatedDate).Skip(page * pageSize).Take(pageSize);

                var responData = Mapper.Map<IEnumerable<Category>, IEnumerable<CategoryViewModel>>(query);

                var paginationSet = new PaginationSet<CategoryViewModel>()
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
                var model = _categoryService.GetAll();
                var responData = Mapper.Map<IEnumerable<Category>, IEnumerable<CategoryViewModel>>(model);
                HttpResponseMessage response = request.CreateResponse(HttpStatusCode.OK, responData);
                return response;
            });
        }

        [Route("create")]
        [HttpPost]
        [AllowAnonymous]
        public HttpResponseMessage Create(HttpRequestMessage request, CategoryViewModel categoryVm)
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
                    var newCategory = new Category();
                    newCategory.UpdateCategory(categoryVm);
                    newCategory.CreatedDate = DateTime.Now;
                    _categoryService.Add(newCategory);
                    _categoryService.Save();

                    var responseData = Mapper.Map<Category, CategoryViewModel>(newCategory);
                    response = request.CreateResponse(HttpStatusCode.Created, responseData);
                }
                return response;
            });
        }

        [Route("update")]
        [HttpPut]
        [AllowAnonymous]
        public HttpResponseMessage Update(HttpRequestMessage request, CategoryViewModel categoryVm)
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
                    var dbCategory = _categoryService.GetById(categoryVm.ID);
                    dbCategory.UpdateCategory(categoryVm);
                    _categoryService.Update(dbCategory);
                    _categoryService.Save();

                    var responseData = Mapper.Map<Category, CategoryViewModel>(dbCategory);
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
                    var oldCategory = _categoryService.Delete(id);
                    _categoryService.Save();

                    var responseData = Mapper.Map<Category, CategoryViewModel>(oldCategory);
                    response = request.CreateResponse(HttpStatusCode.Created, responseData);
                }
                return response;
            });
        }

        [Route("deletemulti")]
        [HttpDelete]
        [AllowAnonymous]
        public HttpResponseMessage DeleteMulti(HttpRequestMessage request, string checkedCategories)
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
                    var listCategory = new JavaScriptSerializer().Deserialize<List<int>>(checkedCategories);
                    foreach (var item in listCategory)
                    {
                        _categoryService.Delete(item);
                    }
                    _categoryService.Save();

                    response = request.CreateResponse(HttpStatusCode.OK, listCategory.Count);
                }
                return response;
            });
        }
    }
}