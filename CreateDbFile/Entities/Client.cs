namespace DataAccess.Entities;

public class Client
{
    public int Id { get; init; }
    public string Name { get; init; }

    public virtual ICollection<ClientDocument> ClientDocuments { get; init; }
}
