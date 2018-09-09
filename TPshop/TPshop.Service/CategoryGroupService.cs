using System.Collections.Generic;
using TPshop.Data.Infrastructure;
using TPshop.Data.Respositories;
using TPshop.Model.Models;

namespace TPshop.Service
{
    public interface ICategoryGroupService
    {
        CategoryGroup Add(CategoryGroup categoryGroup);

        CategoryGroup Delete(int id);

        CategoryGroup GetById(int id);

        void Update(CategoryGroup categoryGroup);

        IEnumerable<CategoryGroup> GetAll();

        IEnumerable<CategoryGroup> GetAll(string keyword);

        void Save();
    }

    public class CategoryGroupService : ICategoryGroupService
    {
        private ICategoryGroupRepository _categoryGroupRepository;
        private IUnitOfWork _unitOfWork;

        public CategoryGroupService(ICategoryGroupRepository categoryGroupRepository, IUnitOfWork unitOfWork)
        {
            this._categoryGroupRepository = categoryGroupRepository;
            this._unitOfWork = unitOfWork;
        }

        public CategoryGroup Add(CategoryGroup categoryGroup)
        {
            return _categoryGroupRepository.Add(categoryGroup);
        }

        public CategoryGroup Delete(int id)
        {
            return _categoryGroupRepository.Delete(id);
        }

        public IEnumerable<CategoryGroup> GetAll()
        {
            return _categoryGroupRepository.GetAll();
        }

        public IEnumerable<CategoryGroup> GetAll(string keyword)
        {
            if (!string.IsNullOrEmpty(keyword))
            {
                return _categoryGroupRepository.GetMulti(x => x.Name.Contains(keyword));
            }
            else
            {
                return _categoryGroupRepository.GetAll();
            }
        }

        public CategoryGroup GetById(int id)
        {
            return _categoryGroupRepository.GetSingleById(id);
        }

        public void Save()
        {
            _unitOfWork.Commit();
        }

        public void Update(CategoryGroup categoryGroup)
        {
            _categoryGroupRepository.Update(categoryGroup);
        }
    }
}