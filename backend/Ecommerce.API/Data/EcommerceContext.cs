using Ecommerce.API.Models;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.API.Data;

public class EcommerceContext : DbContext
{
    public DbSet<ClientModel> Clients { get; set; }
    public DbSet<OrderModel> Orders { get; set; }
    public DbSet<ProductModel> Products { get; set; }
    public DbSet<FilePathModel> FilePaths { get; set; }

    public EcommerceContext(DbContextOptions<EcommerceContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        
        modelBuilder.Entity<ClientModel>()
            .HasIndex(c => c.CPF_CNPJ);


        modelBuilder.Entity<OrderModel>()
            .HasOne(o => o.Client)
            .WithMany(c => c.Orders)
            .HasForeignKey(o => o.ClientCPF_CNPJ);
        
        modelBuilder.Entity<OrderModel>()
            .HasOne(o => o.Product)
            .WithMany(p => p.Orders)
            .HasForeignKey(o => o.ProductId);
    }

    public void AddSeedData()
    {
        try
        {
            if (!Products.Any())
            {
                Products.AddRange(
                    new ProductModel { Id = 1, Name = "Celular", Preco = 1000m },
                    new ProductModel { Id = 2, Name = "Notebook", Preco = 3000m },
                    new ProductModel { Id = 3, Name = "Televisão", Preco = 5000m }
                );

                SaveChanges();
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Erro ao adicionar dados de Seed: {ex.Message}");
        }
    }
}