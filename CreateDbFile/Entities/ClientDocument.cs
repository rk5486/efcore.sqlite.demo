namespace DataAccess.Entities;

public class ClientDocument
{
    public int ClientId { get; init; }
    public int DocumentId { get; init; }
    public Client Client { get; init; }
    public Document Document { get; init; }
}
