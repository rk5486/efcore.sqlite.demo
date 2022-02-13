using System.Collections.Generic;
using System.Threading.Tasks;
using AutoFixture;
using DataAccess.Entities;
using DataAccess.Services;
using DataAccess.UnitOfWork;
using Xunit;
using Xunit.Abstractions;

namespace DataAccess.UnitTests;

public class DemoContextTests
{
    private readonly IFixture _fixture = new Fixture();
    private readonly ITestOutputHelper _output;

    private readonly IEnumerable<Client> _clients;
    private readonly IEnumerable<Document> _documents;
    private readonly IEnumerable<ClientDocument> _clientDocuments;

    public DemoContextTests(ITestOutputHelper output)
    {
        _output = output;
        
        var client1 = _fixture.Build<Client>().Without(client => client.ClientDocuments).Create();
        var client2 = _fixture.Build<Client>().Without(client => client.ClientDocuments).Create();
        var client3 = _fixture.Build<Client>().Without(client => client.ClientDocuments).Create();

        var document1 = _fixture.Build<Document>().Without(document => document.ClientDocuments).Create();
        var document2 = _fixture.Build<Document>().Without(document => document.ClientDocuments).Create();
        var document3 = _fixture.Build<Document>().Without(document => document.ClientDocuments).Create();

        _clients = new List<Client> { client1, client2, client3 };
        _documents = new List<Document> { document1, document2, document3 };
        _clientDocuments = new List<ClientDocument>
        {
            new() { ClientId = client1.Id, DocumentId = document1.Id },
            new() { ClientId = client2.Id, DocumentId = document2.Id },
            new() { ClientId = client3.Id, DocumentId = document3.Id },
            new() { ClientId = client3.Id, DocumentId = document1.Id },
        };
    }

    [Fact]
    public async Task SqliteFileService()
    {
        byte[]? content;

        using (ISqliteFileService fileService = new SqliteFileService())
        {
            await fileService.CreateDatabase(_clients, _documents, _clientDocuments);
            content = await fileService.ReadFileAsByteArray();
        }

        Assert.NotEmpty(content);
    }
    
    [Fact]
    public async Task ClientDocumentUnitOfWork()
    {
        int numberOfRows;
        byte[]? content;

        using (IUnitOfWork unitOfWork = new ClientDocumentUnitOfWork())
        {
            unitOfWork.AddRange(_clients);
            unitOfWork.AddRange(_documents);
            unitOfWork.AddRange(_clientDocuments);

            numberOfRows = await unitOfWork.SaveAsync();
            content = await unitOfWork.ReadDbAsByteArray();
        }

        Assert.NotEmpty(content);
        Assert.True(numberOfRows == 10);
        _output.WriteLine($"{numberOfRows}");
    }
}
