namespace DataAccess.Entities;

public class ClientDocument
{
    public int ClientId { get; set; }
    public int DocumentId { get; set; }
    public Client Client { get; set; }
    public Document Document { get; set; }
}
