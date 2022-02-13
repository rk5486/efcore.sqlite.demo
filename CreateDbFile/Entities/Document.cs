namespace DataAccess.Entities;

public class Document
{
    public int Id { get; init; }
    public string Number { get; init; }
    public DateTime DocumentDate { get; init; }
    
    public virtual ICollection<ClientDocument> ClientDocuments { get; init; }
}
