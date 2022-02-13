namespace DataAccess.Entities;

public class Document
{
    public int Id { get; set; }
    public string Number { get; set; }
    public DateTime DocumentDate { get; set; }
    
    public virtual ICollection<ClientDocument> ClientDocuments { get; set; }
}
