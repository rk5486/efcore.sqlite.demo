using DataAccess.Entities;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Services;

public sealed class SqliteFileService : ISqliteFileService
{
    private readonly ClientDocumentContext _context;
    private readonly string _dbPath;
    private readonly SqliteConnection _connection;

    public SqliteFileService()
    {
        _dbPath = Path.GetTempFileName();
        _connection = new SqliteConnection($"Data Source={_dbPath}");

        var optionsBuilder = new DbContextOptionsBuilder<ClientDocumentContext>();
        optionsBuilder.UseSqlite(_connection);

        _context = new ClientDocumentContext(optionsBuilder.Options);
        _context.Database.EnsureCreated();
    }

    public async Task<int> CreateDatabase(
        IEnumerable<Client> clients,
        IEnumerable<Document> documents,
        IEnumerable<ClientDocument> clientDocuments)
    {
        _context.Clients.AddRange(clients);
        _context.Documents.AddRange(documents);
        _context.ClientDocuments.AddRange(clientDocuments);
        
        return await _context.SaveChangesAsync();
    }

    public async Task<byte[]> ReadFileAsByteArray()
    {
        SqliteConnection.ClearPool(_connection);
        return await File.ReadAllBytesAsync(_dbPath);
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
            _context.Dispose();
        }

        _disposed = true;
    }

    public void Dispose() => Dispose(true);
}
