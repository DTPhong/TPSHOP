using TPshop.Data.Infrastructure;
using TPshop.Model.Models;

namespace TPshop.Data.Respositories
{
    public interface ISupplierRepository : IRepository<Supplier>
    {
    }

    public class SupplierRepository : RepositoryBase<Supplier>, ISupplierRepository
    {
        public SupplierRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }
    }
}