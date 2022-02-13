using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.UnitOfWork;

public sealed class ClientDocumentUnitOfWork : IUnitOfWork
{
    private readonly ClientDocumentContext _dbContext;
    private readonly SqliteConnection _connection;
    private readonly string _dbPath;
    
    public ClientDocumentUnitOfWork()
    {
        _dbPath = Path.GetTempFileName();
        _connection = new SqliteConnection($"Data Source={_dbPath}");

        var optionsBuilder = new DbContextOptionsBuilder<ClientDocumentContext>();
        optionsBuilder.UseSqlite(_connection);

        _dbContext = new ClientDocumentContext(optionsBuilder.Options);
        _dbContext.Database.EnsureCreated();
    }

    public void AddRange<T>(IEnumerable<T> data) where T : class
    {
        var set = _dbContext.Set<T>();
        set.AddRange(data);
    }

    public Task<byte[]> ReadDbAsByteArray() {
        SqliteConnection.ClearPool(_connection);
        return File.ReadAllBytesAsync(_dbPath);
    }

    public Task<int> SaveAsync()
    {
        return _dbContext.SaveChangesAsync();
    }
    
    private bool _disposed;

    private void Dispose(bool disposing)
    {
        if (!_disposed & disposing)
        {
            if (File.Exists(_dbPath))
            {
                File.Delete(_dbPath);
            }
            
            SqliteConnection.ClearPool(_connection);
            _dbContext.Dispose();
        }

        _disposed = true;
    }

    public void Dispose() => Dispose(true);
}
