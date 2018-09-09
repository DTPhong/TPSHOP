using TPshop.Data.Infrastructure;
using TPshop.Model.Models;

namespace TPshop.Data.Respositories
{
    public interface ISettingRepository : IRepository<Setting>
    {
    }

    public class SettingRepository : RepositoryBase<Setting>, ISettingRepository
    {
        public SettingRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }
    }
}