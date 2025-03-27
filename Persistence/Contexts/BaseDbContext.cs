using System.Reflection;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Persistence.Contexts;

public class BaseDbContext : DbContext
{
    protected IConfiguration Configuration;

    public DbSet<AdditionalService> AdditionalServices { get; set; }
    public Rental Rental { get; set; }
    public RentalBranch RentalBranch { get; set; }
    public RentalsAdditionalService RentalsAdditionalService { get; set; }

    public BaseDbContext(DbContextOptions contextOptions, IConfiguration configuration) : base(contextOptions)
    {
        Configuration = configuration;
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}