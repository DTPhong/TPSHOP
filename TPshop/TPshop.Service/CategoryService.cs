using System.Collections.Generic;
using System.Linq;
using TPshop.Data.Infrastructure;
using TPshop.Data.Respositories;
using TPshop.Model.Models;

namespace TPshop.Service
{
    public interface ICategoryService
    {
        Category Add(Category category);

        Category Delete(int id);

        void Update(Category category);

        IEnumerable<Category> GetAll();

        IEnumerable<Category> GetAll(string[] includes, string keyword);

        IEnumerable<Category> GetAll(string keyword);

        IEnumerable<Category> GetHottestCategory(int top);

        Category GetById(int id);

        Category GetByCategoryGroupId(int id);

        void Save();
    }

    public class CategoryService : ICategoryService
    {
        private ICategoryRepository _categoryRepository;

        private IUnitOfWork _unitOfWork;

        public CategoryService(ICategoryRepository categoryRepository, IUnitOfWork unitOfwork)
        {
            this._categoryRepository = categoryRepository;
            this._unitOfWork = unitOfwork;
        }

        public Category Add(Category category)
        {
            return _categoryRepository.Add(category);
        }

        public Category Delete(int id)
        {
            return _categoryRepository.Delete(id);
        }

        public IEnumerable<Category> GetAll()
        {
            return _categoryRepository.GetAll();
        }

        public IEnumerable<Category> GetAll(string keyword)
        {
            if (!string.IsNullOrEmpty(keyword))
            {
                return _categoryRepository.GetMulti(x => x.Name.Contains(keyword));
            }
            else
            {
                return _categoryRepository.GetAll();
            }
        }

        public IEnumerable<Category> GetAll(string[] includes, string keyword)
        {
            if (!string.IsNullOrEmpty(keyword))
            {
                return _categoryRepository.GetMulti(x => x.Name.Contains(keyword), includes);
            }
            else
            {
                return _categoryRepository.GetAll(includes);
            }
        }

        public Category GetByCategoryGroupId(int id)
        {
            return _categoryRepository.GetSingleByCondition(x => x.CategoryGroupID == id);
        }

        public Category GetById(int id)
        {
            return _categoryRepository.GetSingleById(id);
        }

        public IEnumerable<Category> GetHottestCategory(int top)
        {
            return _categoryRepository.GetMulti(x => x.Homeflag == true && x.Status == true).OrderByDescending(x => x.CreatedDate).Take(top);
        }

        public void Save()
        {
            _unitOfWork.Commit();
        }

        public void Update(Category category)
        {
            _categoryRepository.Update(category);
        }
    }
}