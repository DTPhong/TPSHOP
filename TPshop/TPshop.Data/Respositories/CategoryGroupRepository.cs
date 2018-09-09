using TPshop.Data.Infrastructure;
using TPshop.Model.Models;

namespace TPshop.Data.Respositories
{
    public interface ICategoryGroupRepository : IRepository<CategoryGroup>
    {
    }

    public class CategoryGroupRepository : RepositoryBase<CategoryGroup>, ICategoryGroupRepository
    {
        public CategoryGroupRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }
    }
}