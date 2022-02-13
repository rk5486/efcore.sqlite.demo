using DataAccess.Entities;

namespace DataAccess.Services;

public interface ISqliteFileService : IDisposable
{
    Task<int> CreateDatabase(
        IEnumerable<Client> clients,
        IEnumerable<Document> documents,
        IEnumerable<ClientDocument> clientDocuments);

    Task<byte[]> ReadFileAsByteArray();
}
