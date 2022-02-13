namespace DataAccess.Entities;

public class Client
{
    public int Id { get; set; }
    public string Name { get; set; }
    public DateTime DateOfBirth { get; set; }
    
    public virtual ICollection<ClientDocument> ClientDocuments { get; set; }
}
