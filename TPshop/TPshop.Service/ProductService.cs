using System;
using System.Collections.Generic;
using System.Linq;
using TPshop.Data.Infrastructure;
using TPshop.Data.Respositories;
using TPshop.Model.Models;

namespace TPshop.Service
{
    public interface IProductService
    {
        Product Add(Product Product);

        void Update(Product Product);

        Product Delete(int id);

        IEnumerable<Product> GetAll();

        IEnumerable<Product> GetAll(string keyword, string[] includes);

        IEnumerable<Product> GetLastest(int top, string[] includes);

        IEnumerable<Product> GetHotProduct(int top);

        IEnumerable<Product> GetTopSale(int top, string[] includes);

        IEnumerable<Product> GetListProductByCategoryIdPaging(int categoryId, int page, int pageSize, string sort, out int totalRow, string[] includes);

        IEnumerable<Product> GetListProductByGroupCategoryIdPaging(int categoryGroupId, int page, int pageSize, string sort, out int totalRow, string[] includes);

        IEnumerable<Product> Search(string keyword, int page, int pageSize, string sort, out int totalRow, string[] includes);

        IEnumerable<Product> GetReatedProducts(int id, int categoryId, int top);

        IEnumerable<string> GetListProductByName(string name);

        Product GetById(int id);

        void Save();

        void IncreaseView(int id);

        bool SellProduct(int productId, int quantity);
    }

    public class ProductService : IProductService
    {
        private IProductRepository _productRepository;

        private IUnitOfWork _unitOfWork;

        public ProductService(IProductRepository productRepository, IUnitOfWork unitOfWork)
        {
            this._productRepository = productRepository;
            this._unitOfWork = unitOfWork;
        }

        public Product Add(Product product)
        {
            return _productRepository.Add(product);
        }

        public Product Delete(int id)
        {
            return _productRepository.Delete(id);
        }

        public IEnumerable<Product> GetAll()
        {
            return _productRepository.GetAll();
        }

        public IEnumerable<Product> GetAll(string keyword, string[] includes)
        {
            if (!string.IsNullOrEmpty(keyword))
                return _productRepository.GetMulti(x => x.Name.Contains(keyword) || x.Description.Contains(keyword), includes);
            else
                return _productRepository.GetAll(includes);
        }

        public Product GetById(int id)
        {
            return _productRepository.GetSingleById(id);
        }

        public IEnumerable<Product> GetHotProduct(int top)
        {
            return _productRepository.GetMulti(x => x.HomeFlag == true && x.Status == true).OrderByDescending(x => x.CreatedDate).Take(top);
        }

        public IEnumerable<Product> GetLastest(int top, string[] includes)
        {
            return _productRepository.GetMulti(x => x.Status == true, includes).OrderByDescending(x => x.CreatedDate).Take(top);
        }

        public IEnumerable<Product> GetListProductByCategoryIdPaging(int categoryId, int page, int pageSize, string sort, out int totalRow, string[] includes)
        {
            var query = _productRepository.GetMulti(x => x.Status == true && x.CategoryID == categoryId, includes);

            switch (sort)
            {
                case "popular":
                    query = query.OrderByDescending(x => x.ViewCount);
                    break;

                case "discount":
                    query = query.OrderByDescending(x => x.Promotion.HasValue);
                    break;

                case "price":
                    query = query.OrderBy(x => x.Price);
                    break;

                default:
                    query = query.OrderByDescending(x => x.CreatedDate);
                    break;
            }

            totalRow = query.Count();

            return query.Skip((page - 1) * pageSize).Take(pageSize);
        }

        public IEnumerable<Product> GetListProductByGroupCategoryIdPaging(int categoryGroupId, int page, int pageSize, string sort, out int totalRow, string[] includes)
        {
            var query = _productRepository.GetMulti(x => x.Status == true && x.Category.CategoryGroupID == categoryGroupId, includes);

            switch (sort)
            {
                case "popular":
                    query = query.OrderByDescending(x => x.ViewCount);
                    break;

                case "discount":
                    query = query.OrderByDescending(x => x.Promotion.HasValue);
                    break;

                case "price":
                    query = query.OrderBy(x => x.Price);
                    break;

                default:
                    query = query.OrderByDescending(x => x.CreatedDate);
                    break;
            }

            totalRow = query.Count();

            return query.Skip((page - 1) * pageSize).Take(pageSize);
        }

        public IEnumerable<string> GetListProductByName(string name)
        {
            return _productRepository.GetMulti(x => x.Status == true && x.Name.Contains(name)).Select(y => y.Name);
        }

        public IEnumerable<Product> GetReatedProducts(int id, int categoryId, int top)
        {
            return _productRepository.GetMulti(x => x.Status == true && x.ID != id && x.CategoryID == categoryId).OrderByDescending(x => x.CreatedDate).Take(top);
        }

        public IEnumerable<Product> GetTopSale(int top, string[] includes)
        {
            return _productRepository.GetMulti(x => x.Status == true && x.Promotion.HasValue, includes).OrderByDescending(x => x.Promotion / x.Price).Take(top);
        }

        public void IncreaseView(int id)
        {
            var product = _productRepository.GetSingleById(id);
            if (product.ViewCount.HasValue)
                product.ViewCount += 1;
            else
                product.ViewCount = 1;
        }

        public void Save()
        {
            _unitOfWork.Commit();
        }

        public IEnumerable<Product> Search(string keyword, int page, int pageSize, string sort, out int totalRow, string[] includes)
        {
            var query = _productRepository.GetMulti(x => x.Status == true && x.Name.Contains(keyword), includes);

            switch (sort)
            {
                case "popular":
                    query = query.OrderByDescending(x => x.ViewCount);
                    break;

                case "discount":
                    query = query.OrderByDescending(x => x.Promotion.HasValue);
                    break;

                case "price":
                    query = query.OrderBy(x => x.Price);
                    break;

                default:
                    query = query.OrderByDescending(x => x.CreatedDate);
                    break;
            }

            totalRow = query.Count();

            return query.Skip((page - 1) * pageSize).Take(pageSize);
        }

        public bool SellProduct(int productId, int quantity)
        {
            throw new NotImplementedException();
        }

        public void Update(Product product)
        {
            _productRepository.Update(product);
        }
    }
}