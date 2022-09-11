using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using MVP.VendingMachine.DataModel.Configurations;
using MVP.VendingMachine.DataModel.Models;

namespace MVP.VendingMachine.DataModel;

public class DataContext : IdentityDbContext<UserModel>
{
    private readonly IConfiguration _configuration;

    public DataContext() : base() { }

    public DataContext(DbContextOptions options, IConfiguration configuration)
        : base(options)
    {
        _configuration = configuration;
    }

    public DbSet<UserModel> Users { get; set; }
    public DbSet<ProductModel> Products { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder options)
    {
        if (!options.IsConfigured)     
            options.UseNpgsql(_configuration.GetSection("ConnectionString").Value);
        
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfiguration(new UserConfiguration());
        modelBuilder.ApplyConfiguration(new ProductConfiguration());
        modelBuilder.ApplyConfiguration(new RoleConfiguration());
    }
}



