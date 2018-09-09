using System.Collections.Generic;
using System.Linq;
using TPshop.Data.Infrastructure;
using TPshop.Model.Models;

namespace TPshop.Data.Respositories
{
    public interface ICategoryRepository : IRepository<Category>
    {
        IEnumerable<Category> GetByAlias(string alias);
    }

    public class CategoryRepository : RepositoryBase<Category>, ICategoryRepository
    {
        public CategoryRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }

        public IEnumerable<Category> GetByAlias(string alias)
        {
            return this.DbContext.Categories.Where(x => x.Alias == alias);
        }
    }
}