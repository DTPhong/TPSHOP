namespace TPshop.Data.Infrastructure
{
    public class DbFactory : Disposable, IDbFactory
    {
        private TPshopDbContext dbContext;

        public TPshopDbContext Init()
        {
            return dbContext ?? (dbContext = new TPshopDbContext());
        }

        protected override void DisposeCore()
        {
            if (dbContext != null)
            {
                dbContext.Dispose();
            }
        }
    }
}