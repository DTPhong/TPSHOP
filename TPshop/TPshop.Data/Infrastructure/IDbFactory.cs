using System;

namespace TPshop.Data.Infrastructure
{
    public interface IDbFactory : IDisposable
    {
        TPshopDbContext Init();
    }
}