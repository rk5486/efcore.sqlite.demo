namespace DataAccess.UnitOfWork;

public interface IUnitOfWork : IDisposable
{
    void AddRange<T>(IEnumerable<T> data) where T : class;
    Task<byte[]> ReadDbAsByteArray();
    Task<int> SaveAsync();
}
