using DataAccess.Entities;
using Microsoft.EntityFrameworkCore;

namespace DataAccess;

public class ClientDocumentContext : DbContext
{
    public ClientDocumentContext(DbContextOptions options) : base(options)
    {
    }

    public DbSet<Client> Clients { get; set; }
    public DbSet<Document> Documents { get; set; }
    public DbSet<ClientDocument> ClientDocuments { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<ClientDocument>()
                    .HasKey(cd => new { cd.ClientId, cd.DocumentId });

        modelBuilder.Entity<ClientDocument>()
                    .HasOne(cd => cd.Client)
                    .WithMany(c => c.ClientDocuments)
                    .HasForeignKey(cd => cd.ClientId);

        modelBuilder.Entity<ClientDocument>()
                    .HasOne(cd => cd.Document)
                    .WithMany(c => c.ClientDocuments)
                    .HasForeignKey(cd => cd.DocumentId);
    }
}
