using Microsoft.EntityFrameworkCore;
using mpc_DZ.Models;

public class CustomersContext : DbContext
{
    public CustomersContext(DbContextOptions<CustomersContext> options) : base(options) { }
    public DbSet<Customer> Customers { get; set; }
}
