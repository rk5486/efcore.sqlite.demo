using Microsoft.EntityFrameworkCore;

namespace DataAccess.UnitOfWork;

public interface IRepository<T> where T : class
{
    void AddRange(IEnumerable<T> data);
}

public class Repository<T> : IRepository<T> where T : class
{
    private readonly DbSet<T> _set;

    public Repository(DbContext dbContext)
    {
        _set = dbContext.Set<T>();
    }
    
    public void AddRange(IEnumerable<T> data)
    {
        _set.AddRange(data);
    }
}
